using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardNumber : MonoBehaviour
{
    [SerializeField] Text _cardNumberText;
    private int _currentCardNumber = 7;

    private void Awake()
    {
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChanged);
        EventManager.AddListener<CardPickUpEvent>(OnCardPickUp);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChanged);
        EventManager.RemoveListener<CardPickUpEvent>(OnCardPickUp);
    }
    private void Start()
    {
        _cardNumberText.text = "x" + _currentCardNumber;
    }
    private void OnCardNumberChanged(CardNumberChangeEvent obj)
    {
        _currentCardNumber = obj.TotalNumber; 
        if (obj.Number < 0)
        {
            VarSaver.NumberOfCards = _currentCardNumber;
            _cardNumberText.text = "x" + _currentCardNumber;
            _cardNumberText.GetComponent<Animator>().SetTrigger("Discard");
        }
    }
    private void OnCardPickUp(CardPickUpEvent obj)
    {
            VarSaver.NumberOfCards = _currentCardNumber;
            _cardNumberText.text = "x" + _currentCardNumber;
        _cardNumberText.GetComponent<Animator>().SetTrigger("PickUp");
    }
}
