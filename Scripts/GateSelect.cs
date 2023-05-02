using UnityEngine;

public class GateSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] _gates;
    [SerializeField] private float _gateWaySize;

    [SerializeField] private Deck _playerDeck;
    [SerializeField] private GameObject _cardObject;
    private CardClass[] _cardsInGates;
    private GameObject[] _cardsGOsInGates;
    [SerializeField] private CardClass[] _commonCardPool;

    [SerializeField] private AnimationCurve _cardPoolBasedOnLevel;

    private void Awake()
    {
        _playerDeck = FindObjectOfType<Deck>();
        _cardsInGates = new CardClass[_gates.Length];
        _cardsGOsInGates = new GameObject[_gates.Length];
        
    }
    private void Start()
    {
        int[] positions = new int[_gates.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i]=i;
        }
        for (int i =0; i<positions.Length-1; i++)
        {
            int rnd = Random.Range(i, positions.Length);
            int temp = positions[rnd];
            positions[rnd] = positions[i];
            positions[i] = temp;
        }
        for (int i = 0; i<_gates.Length; i++)
        {
            CardClass card;
            if (_playerDeck.GetCardAt(i) != null)
            {
                CardColor cardColor = _playerDeck.GetCardAt(i).GetCardColor;
                int randomNumber = Random.Range(0, 5);
                card = _commonCardPool[5 * (int) cardColor + randomNumber];
            }
            else
            {
                int randomNumber = Random.Range(0, 5 * (int)_cardPoolBasedOnLevel.Evaluate(PlayerPrefs.GetInt("level")));
                card = _commonCardPool[randomNumber];
            }
            GameObject go = Instantiate(_cardObject, _gates[positions[i]].transform);
            _cardsInGates[positions[i]] = card;
            _cardsGOsInGates[positions[i]] = go;
            ChangeSkin(VarSaver.Skin, go.GetComponentInChildren<Renderer>(), card);

        }
    }
    /*
    private int[] RandomUniqueIntegers(int count)
    {
        int[] ints = new int[count];
        int i = 0;
        while (i<count)
        {
            int n = Random.Range(1, 5);
            if (!ints.Contains(n))
            {
                ints[i] = n;
                i++;
            }
        }
        return ints;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChooseGateBasedOnPositionX(other.gameObject);
        }
    }
    private void ChooseGateBasedOnPositionX(GameObject playerObject)
    {
        if (playerObject.transform.position.x < -_gateWaySize)
        {
            SelectGate(0, playerObject.GetComponentInChildren<Deck>());
            //_gates[0].SelectGate(playerObject.GetComponentInChildren<Deck>());
        }
        else if (playerObject.transform.position.x < 0)
        {
            SelectGate(1, playerObject.GetComponentInChildren<Deck>());
            //_gates[1].SelectGate(playerObject.GetComponentInChildren<Deck>());
        }
        else if (playerObject.transform.position.x > _gateWaySize)
        {
            SelectGate(3, playerObject.GetComponentInChildren<Deck>());
            //_gates[3].SelectGate(playerObject.GetComponentInChildren<Deck>());
        }
        else
        {

            SelectGate(2, playerObject.GetComponentInChildren<Deck>());
            //_gates[2].SelectGate(playerObject.GetComponentInChildren<Deck>());
        }
    }
    public void SelectGate(int number, Deck deck)
    {
        switch (_cardsInGates[number].GetCardType)
        {
            case CardType.Common:
                {
                    CompareCards(_cardsInGates[number], deck);
                }
                break;
            case CardType.Wild:
                {
                    deck.ChangeTopCardColor();
                }
                break;
            case CardType.Draw:
                {
                    deck.DrawRandomCards(_cardsInGates[number].GetDrawNumber);
                }
                break;
            case CardType.Discard:
                {
                    deck.DiscardCards(_cardsInGates[number].GetDrawNumber);
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
        Destroy(_cardsGOsInGates[number]);
    }
    private void CompareCards(CardClass card, Deck deck)
    {
        if (card.Compare(deck.GetTopCard()))
        {

            deck.RemoveTopCard();
        }
        else
        {
            deck.AddCardToDeck(card);
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
}
