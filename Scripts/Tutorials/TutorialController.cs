using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject[] _tutorials;
    private bool _cardPickedUp = false;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChange);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChange);
    }
    private void OnCardNumberChange(CardNumberChangeEvent obj)
    {
        if (!_cardPickedUp && (PlayerPrefs.GetInt("level", 1) == 1) && (PlayerPrefs.GetInt("CompleteTutorial", 0) != 1))
        {
            _tutorials[1].SetActive(true);
            BrodcastPauseSetEvent();
            _cardPickedUp = true;
        }
    }
    private void OnGameStart(GameStartEvent obj)
    {
        if (PlayerPrefs.GetInt("CompleteTutorial", 0) != 1)
        {
            switch (PlayerPrefs.GetInt("level", 1))
            {
                case 1:
                    {
                        _tutorials[0].SetActive(true);
                        BrodcastPauseSetEvent();
                    }
                    break;
                case 4:
                    {
                        _tutorials[2].SetActive(true);
                        BrodcastPauseSetEvent();
                    }
                    break;
                case 5:
                    {
                        _tutorials[3].SetActive(true);
                        BrodcastPauseSetEvent();
                    }
                    break;
                case 6:
                    {
                        _tutorials[4].SetActive(true);
                        BrodcastPauseSetEvent();
                    }
                    break;
                case 7:
                    {
                        _tutorials[5].SetActive(true);
                        BrodcastPauseSetEvent();
                    }
                    break;
                case 8:
                    {
                        PlayerPrefs.SetInt("CompleteTutorial", 1);
                    }
                    break;
            }
        }
    }

    private static void BrodcastPauseSetEvent()
    {
        var evt = GameEventsHandler.GamePauseEvent;
        evt.SetPause = true;
        EventManager.Broadcast(evt);
    }
}
