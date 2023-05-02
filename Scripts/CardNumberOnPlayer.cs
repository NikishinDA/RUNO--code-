using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardNumberOnPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _cardNumberText;
    private int _currentCardNumber = 7;

    private void Awake()
    {
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void Start()
    {
        _cardNumberText.text =  _currentCardNumber.ToString();
    }
    private void OnCardNumberChanged(CardNumberChangeEvent obj)
    {
        _currentCardNumber += obj.Number;
        VarSaver.NumberOfCards = _currentCardNumber;
        _cardNumberText.text =  _currentCardNumber.ToString();
    }
}
