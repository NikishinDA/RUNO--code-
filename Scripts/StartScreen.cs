using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Text _coinText;
    [SerializeField] InputField _levelLengthDebug;
    [SerializeField] InputField _speedXDebug;
    [SerializeField] InputField _speedZDebug;
    [SerializeField] InputField _speedZMaxDebug;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonPress);
    }
    private void Start()
    {
        _coinText.text = PlayerPrefs.GetInt("totalCoins").ToString();
    }
    private void OnStartButtonPress()
    {
        /*var evt = GameEventsHandler.GameStartEvent;
        evt.LevelSetLength = Convert.ToInt32(_levelLengthDebug.text);
        evt.SetSpeedX = Convert.ToSingle(_speedXDebug.text);
        evt.SetSpeedZ = Convert.ToSingle(_speedZDebug.text);
        evt.SetSpeedZMax = Convert.ToSingle(_speedZMaxDebug.text);*/
        EventManager.Broadcast(GameEventsHandler.GameStartEvent);
        gameObject.SetActive(false);
    }
}
