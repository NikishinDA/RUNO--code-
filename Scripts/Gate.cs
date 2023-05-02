using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private CardClass[] _commonCardPool;
    [SerializeField] private CardClass[] _specialCardPool;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _cardAnimController;
    [SerializeField] private CardClass _card;

    [SerializeField] private AnimationCurve _probabilityCurve;
    [SerializeField] private AnimationCurve _cardPoolBasedOnLevel;
    [SerializeField] private AnimationCurve _specialCardPoolBasedOnLevel;


    private void Start()
    {
        /*float prob = _probabilityCurve.Evaluate(PlayerPrefs.GetInt("level"));
        if (Random.value > prob)
        {
            int randomNumber = Random.Range(0, 5 * (int)_cardPoolBasedOnLevel.Evaluate(PlayerPrefs.GetInt("level")));
            _card = _commonCardPool[randomNumber];
        }
        else*/
            int randomNumber = Random.Range(0, (int)_specialCardPoolBasedOnLevel.Evaluate(PlayerPrefs.GetInt("level")));
            _card = _specialCardPool[randomNumber];
 
        GameObject go = Instantiate(_cardPrefab, transform);
        ChangeSkin(VarSaver.Skin, go.GetComponentInChildren<Renderer>(), _card);
    }
    private void CompareCards(Deck deck)
    {
        if (_card.Compare(deck.GetTopCard()))
        {
            
            deck.RemoveTopCard();
        }
        else
        {
            deck.AddCardToDeck(_card);
        }
    }
    private void ChangeSkin(int skinNum, Renderer renderer, CardClass card)
    {
        switch (skinNum)
        {
            case 0:
                {
                    renderer.material = card.SkinClassic;
                }
                break;
            case 1:
                {
                    renderer.material = card.SkinFood;
                }
                break;
            case 2:
                {
                    renderer.material = card.SkinShape;
                }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        SelectGate(other.gameObject.GetComponentInChildren<Deck>());
    }
    public void SelectGate(Deck deck)
    {
        switch (_card.GetCardType)
        {
            case CardType.Common:
                {
                    CompareCards(deck);
                }
                break;
            case CardType.Wild:
                {
                    deck.ChangeTopCardColor();
                }
                break;
            case CardType.Draw:
                {
                    deck.DrawRandomCards(_card.GetDrawNumber);
                }
                break;
            case CardType.Discard:
                {
                    deck.DiscardCards(_card.GetDrawNumber);
                }
                break;
            case CardType.GameOver:
                {
                    var evt = GameEventsHandler.GameOverEvent;
                    evt.IsWin = false;
                    EventManager.Broadcast(evt);
                }
                break;
        }
        Destroy(gameObject);
    }
}
