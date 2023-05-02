using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLevelTutorial : MonoBehaviour
{
    [SerializeField] private Button _screenTap;

    private void Awake()
    {
        _screenTap.onClick.AddListener(OnScreenTap);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnScreenTap()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        var evt = GameEventsHandler.GamePauseEvent;
        evt.SetPause = false;
        EventManager.Broadcast(evt);
    }
}
