using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementScale;
    [SerializeField] private float _movementConstraint;
    [SerializeField] private float _touchDeltaScale;
    private bool _stopped = true;

    [SerializeField] private ParticleSystem[] _effects;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GamePauseEvent>(OnGamePause);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GamePauseEvent>(OnGamePause);
    }

    private void Update()
    {
        if (!_stopped && Input.GetMouseButton(0))
        {
            float newPosX = Input.GetAxis("Mouse X");
            if (Input.touchCount > 0)
            {
                newPosX = (Input.touches[0].deltaPosition.x / Screen.width) * _touchDeltaScale;
            }
            if (!((transform.position.x + newPosX * _movementScale > _movementConstraint)
                || (transform.position.x + newPosX * _movementScale < -_movementConstraint)))
            {
                transform.Translate(newPosX * _movementScale, 0, 0);
            }
        }
    }
    public void OnGameOver(GameOverEvent obj)
    {
        _stopped = true;
        if (obj.IsWin)
        {
            foreach (ParticleSystem go in _effects)
            {
                go.Play();
            }
        }
    }

    public void OnGameStart(GameStartEvent obj)
    {
        _stopped = false;
    }

    public void OnGamePause(GamePauseEvent obj)
    {
        _stopped = obj.SetPause;
    }

}
