using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private CardClass[] _cardPool;
    [SerializeField] private int _deckInitialCount = 7;
    private List<CardClass> _deck;
    [SerializeField] private CardClass _topCard;
    [SerializeField] private GameObject _playerModel;

    [SerializeField] private AnimationCurve _cardPoolBasedOnLevel;

    [SerializeField] private ParticleSystem _effectChangeCard;
    [SerializeField] private GameObject _textCardRight;
    [SerializeField] private GameObject _textCardWrong;
    //[SerializeField] private ParticleSystem _effectRightCard;
    //[SerializeField] private ParticleSystem _effectWrongCard;


    private int _skinNum = 0;

    public CardClass GetCardAt(int n)
    {
        if (n >= _deck.Count)
        {
            return null;
        }
        else
        {
            return _deck[n];
        }
    }
    private void Awake()
    {
        _deck = new List<CardClass>();
        for (int i = 0; i < _deckInitialCount; i++)
        {
            _deck.Add(NewRandomCard());
        }
    }
    private void Start()
    {
        
        _skinNum = VarSaver.Skin;
        _topCard = _deck[0];
        ChangeSkin(_skinNum);
    }
    private void ChangeSkin(int skinNum)
    {
        switch (skinNum)
        {
            case 0:
                {
                    _playerModel.GetComponentInChildren<Renderer>().material = _topCard.SkinClassic;
                }
                break;
            case 1:
                {
                    _playerModel.GetComponentInChildren<Renderer>().material = _topCard.SkinFood;
                }
                break;
            case 2:
                {
                    _playerModel.GetComponentInChildren<Renderer>().material = _topCard.SkinShape;
                }
                break;
        }
    }
    public CardClass NewRandomCard()
    {
        int randomNumber = Random.Range(0, 5 * (int)_cardPoolBasedOnLevel.Evaluate(PlayerPrefs.GetInt("level")));
        return _cardPool[randomNumber];
    }

    public void AddCardToDeck(CardClass card)
    {
        _deck.Add(card);
        ChangedCardNumberEventBrodcast(1);
        var evt = GameEventsHandler.GateInteractedEvent;
        evt.IsPickUp = true;
        evt.IsDouble = false;
        evt.AffectedCard = card;
        EventManager.Broadcast(evt);
        //_effectWrongCard.Play();
    }
    private void ChangedCardNumberEventBrodcast(int num)
    {
        var evt = GameEventsHandler.CardNumberChangeEvent;
        evt.TotalNumber = _deck.Count;
        evt.Number = num;
        EventManager.Broadcast(evt);
    }
    public CardClass GetTopCard()
    {
        return _topCard;
    }
    public void RemoveTopCard()
    {
        if (_deck.Count > 1)
        {
            var evt = GameEventsHandler.GateInteractedEvent;
            evt.AffectedCard = _topCard;
            evt.IsPickUp = false;
            evt.IsDouble = false;
            EventManager.Broadcast(evt);
            _deck.RemoveAt(0);
            _topCard = _deck[0];
            ChangeSkin(_skinNum);
            _effectChangeCard.Play();
            _textCardRight.SetActive(true);
            ChangedCardNumberEventBrodcast(-1);
        }
        else
        {
            _deck.RemoveAt(0);
            ChangedCardNumberEventBrodcast(-1);
            WinCheck();
        }
    }

    private void WinCheck()
    {
        if (_deck.Count == 0)
        {
            var goEvt = GameEventsHandler.GameOverEvent;
            goEvt.IsWin = true;
            EventManager.Broadcast(goEvt);
        }
    }

    public void ChangeTopCardColor()
    {
        int randomNumber = Random.Range(0, (int)_cardPoolBasedOnLevel.Evaluate(PlayerPrefs.GetInt("level")));
        _topCard = _cardPool[_topCard.GetValue - 1 + 5 * randomNumber];
        ChangeSkin(_skinNum);
        _effectChangeCard.Play();
    }
    public void DrawRandomCards(int number)
    {
        var evt = GameEventsHandler.GateInteractedEvent;
        evt.IsPickUp = true;
        evt.IsDouble = true;
        evt.MoreCards = new List<CardClass>();
        _textCardWrong.SetActive(true);
        for (int i = 0; i < number; i++)
        {
            CardClass card = NewRandomCard();
            evt.MoreCards.Add(card);
            _deck.Add(card);
        }
        EventManager.Broadcast(evt);
        ChangedCardNumberEventBrodcast(number);
        _textCardWrong.SetActive(true);
    }
    public void DiscardCards(int number)
    {
        var evt = GameEventsHandler.GateInteractedEvent;
        evt.IsPickUp = false;
        evt.IsDouble = true;
        evt.MoreCards = new List<CardClass>();
        for (int i = 0; (i < number) && (_deck.Count > 0); i++)
        {
            evt.MoreCards.Add(_deck[_deck.Count - 1]);
            _deck.RemoveAt(_deck.Count - 1);
            ChangedCardNumberEventBrodcast(-1);

        }
        WinCheck();
        EventManager.Broadcast(evt);
        _textCardRight.SetActive(true);
    }
}
