using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _playerAnimator;
    private const string WinAnimatorBool = "IsWin";
    private const string GameStartAnimatorBool = "GameStart";

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        _playerAnimator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }
    public void OnGameOver(GameOverEvent obj)
    {
        _playerAnimator.SetBool(WinAnimatorBool, obj.IsWin);
    }
    public void OnGameStart(GameStartEvent obj)
    {
        _playerAnimator.SetBool(GameStartAnimatorBool, true);
    }
}
