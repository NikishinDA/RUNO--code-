using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIControllerFloat : MonoBehaviour
{
    [SerializeField] InputField _inputField;
    [SerializeField] Button _plusButton;
    [SerializeField] Button _minusButton;
    [SerializeField] private float _value;
    [SerializeField] private float _minValue;
    private void Awake()
    {
        _plusButton.onClick.AddListener(OnPlusButtonPress);
        _minusButton.onClick.AddListener(OnMinusButtonPress);
        _inputField.text = _value.ToString();
    }

    private void OnPlusButtonPress()
    {
        _inputField.text = (++_value).ToString();
    }
    private void OnMinusButtonPress()
    {
        if (_value > _minValue)
        {
            _inputField.text = (--_value).ToString();
        }
    }
}

