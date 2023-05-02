using System.Collections;
using UnityEngine;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SkyboxSettings[] _skyboxes;
    private float _playTime;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<CoinPickUpEvent>(OnCoinPickUp);
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChanged);
        VarSaver.Skin = Random.Range(0, 3);
        SetSkybox(Random.Range(0, 3));
        GameAnalytics.Initialize();
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<CoinPickUpEvent>(OnCoinPickUp);
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("CompleteTutorial", 0);
        }
    }
    private void SetSkybox(int skin)
    {
        RenderSettings.skybox = _skyboxes[skin].Material;
        RenderSettings.fogColor = _skyboxes[skin].FogColor;
    }
    private void OnGameStart(GameStartEvent obj)
    {
        Debug.Log("Game Started");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + PlayerPrefs.GetInt("level"));
        _playTime = 0;
        StartCoroutine("Timer");
    }
    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
        {
            Debug.Log("Game Won");
        }
        else
        {
            Debug.Log("Game Lost");
        }
        var status = (obj.IsWin) ? GAProgressionStatus.Complete : GAProgressionStatus.Fail;
        GameAnalytics.NewProgressionEvent(
            status,
            "Level_" + PlayerPrefs.GetInt("level",1),
            "PlayTime_" + Mathf.RoundToInt(_playTime));
    }
    public void OnCoinPickUp(CoinPickUpEvent obj)
    {
        Debug.Log("Picked up coin");
    }
    public void OnCardNumberChanged(CardNumberChangeEvent obj)
    {
        Debug.Log("Changed number on " + obj.Number);
    }
    private IEnumerator Timer()
    {
        for (; ; ){
            _playTime += Time.deltaTime;
            yield return null;
        }
    }
}
