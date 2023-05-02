using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimatorController : MonoBehaviour
{
    [SerializeField] private GameObject _coinObject;
    [SerializeField] private GameObject _textObject;
    [SerializeField] private GameObject _playerMoverObject;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Transform _pickUpAnimEnd;

    [SerializeField] private float _animOffsetZ;
    [SerializeField] private float _textOffsetY;
    [SerializeField] private float _textOffsetX;
    [SerializeField] private float _time;
    private IEnumerator coroutine;
    private void Awake()
    {
        EventManager.AddListener<CoinPickUpEvent>(OnCoinPickUp);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<CoinPickUpEvent>(OnCoinPickUp);
    }
    private void OnCoinPickUp(CoinPickUpEvent obj)
    {
        GameObject go = Instantiate(_coinObject, _playerMoverObject.transform);
        go.transform.localPosition = new Vector3(_playerObject.transform.position.x, 0, _animOffsetZ);
        //GameObject textGO = Instantiate(_textObject, go.transform);
        //textGO.transform.localPosition = new Vector3(-_textOffsetX, 0, -_textOffsetY);
        //textGO.transform.localScale = 1/go.transform.localScale;
        coroutine = PickUpAnimation(go, Camera.main.ScreenToWorldPoint(new Vector3(_pickUpAnimEnd.position.x, _pickUpAnimEnd.position.y, 50)));
        StartCoroutine(coroutine);
    }
    private IEnumerator PickUpAnimation(GameObject go, Vector3 endPos)
    {
        Vector3 startPos = go.transform.localPosition;
        Vector3 startScale = go.transform.localScale;
        Vector3 finishPos = _playerMoverObject.transform.InverseTransformPoint(endPos);
        float elapsedTime = 0;

        while (elapsedTime < _time)
        {
            go.transform.localPosition = Vector3.Lerp(startPos, finishPos, (elapsedTime / _time));
            go.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, (elapsedTime / _time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        EventManager.Broadcast(GameEventsHandler.CoinPickUpAnimationEndEvent);
        Destroy(go);
    }
}
