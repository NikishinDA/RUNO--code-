using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedScale;
    private bool _stopped = true;

    private float _desiredSpeed;
    [SerializeField] private int _affectSpeedAfterLevel;
    [SerializeField] AnimationCurve _speedToCardNumber;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChanged);

    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void Start()
    {
        _desiredSpeed = _speedScale;
    }
    void Update()
    {
        _speedScale += (_desiredSpeed - _speedScale) * Time.deltaTime;
        if (!_stopped)
        {
            transform.Translate(Vector3.forward * _speedScale * Time.deltaTime);
        }
    }
    public void OnGameOver(GameOverEvent obj)
    {
        _stopped = true;
    }

    public void OnGameStart(GameStartEvent obj)
    {
        _stopped = false;
        /*_speedScale = obj.SetSpeedZ;
        _desiredSpeed = _speedScale;
        Keyframe[] keys = _speedToCardNumber.keys;
        keys[0].value = obj.SetSpeedZMax;
        keys[1].value = _speedScale;
        _speedToCardNumber.keys = keys;*/
    }
    private void OnCardNumberChanged(CardNumberChangeEvent obj)
    {
        if (PlayerPrefs.GetInt("level") > _affectSpeedAfterLevel)
        {
            _desiredSpeed = _speedToCardNumber.Evaluate(obj.TotalNumber);
        }
    }
}
