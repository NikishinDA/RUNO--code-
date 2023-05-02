using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLengthDebug : MonoBehaviour
{
    [SerializeField] InputField _inputField;
    [SerializeField] Button _plusButton;
    [SerializeField] Button _minusButton;
    [SerializeField] private int _length;
    private void Awake()
    {
        _plusButton.onClick.AddListener(OnPlusButtonPress);
        _minusButton.onClick.AddListener(OnMinusButtonPress);
        _inputField.text = _length.ToString();
    }

    private void OnPlusButtonPress()
    {
        _inputField.text = (++_length).ToString();
    }
    private void OnMinusButtonPress()
    {
        if (_length > 5)
        {
            _inputField.text = (--_length).ToString();
        }
    }
}
