using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _startScreen;
    [SerializeField] GameObject _overlay;

    private void Awake()
    {
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);


    }
    private void Start()
    {
        _startScreen.SetActive(true);
    }
    private void OnGameOver(GameOverEvent obj)
    {
        _overlay.SetActive(false);
        if (obj.IsWin)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }
    
    private void OnGameStart(GameStartEvent obj)
    {
        _overlay.SetActive(true);
        
    }

}
