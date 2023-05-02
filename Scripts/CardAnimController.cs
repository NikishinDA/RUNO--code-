using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimController : MonoBehaviour
{
    [SerializeField] private GameObject _cardObject;
    [SerializeField] private GameObject _textObject;
    [SerializeField] private GameObject _playerMoverObject;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Transform _pickUpAnimEnd;

    [SerializeField] private float _animOffsetZ = 1f;
    [SerializeField] private float _textOffsetY = 4f;
    [SerializeField] private float _textOffsetX = 10f;
    [SerializeField] private float _time;
    private IEnumerator coroutine;
    private void Awake()
    {
        EventManager.AddListener<GateInteractedEvent>(OnGateInteracted);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GateInteractedEvent>(OnGateInteracted);
    }
    
    private void OnGateInteracted(GateInteractedEvent obj)
    {
        if (obj.IsDouble)
        {
            if (obj.IsPickUp)
            {
                coroutine = PickUpSeveral(obj.MoreCards);
                StartCoroutine(coroutine);
            }
            else
            {
                coroutine = DiscardSeveral(obj.MoreCards);
                StartCoroutine(coroutine);

            }
        }
        else
        {
            if (obj.IsPickUp)
            {
                SetOffCardPickUpAnimation(obj.AffectedCard);
            }
            else
            {
                SetOffCardDiscardAnimation(obj.AffectedCard);
            }
        }
    }

    private void ChangeSkin(int skinNum, Renderer renderer, CardClass card)
    {
        switch (skinNum)
        {
            case 0:
                {
                    renderer.material = card.SkinClassic;
                }
                break;
            case 1:
                {
                    renderer.material = card.SkinFood;
                }
                break;
            case 2:
                {
                    renderer.material = card.SkinShape;
                }
                break;
        }
    }
    private void SetOffCardPickUpAnimation(CardClass card)
    {
        GameObject go = Instantiate(_cardObject, _playerMoverObject.transform);
        go.transform.localPosition = new Vector3(_playerObject.transform.position.x, 0, _animOffsetZ);
        ChangeSkin(VarSaver.Skin, go.GetComponentInChildren<Renderer>(), card);
       // go.GetComponent<Animator>().SetBool("PickUp", true);
        GameObject textGO = Instantiate(_textObject, go.transform.GetChild(0));
        textGO.transform.localPosition = new Vector3(-_textOffsetX, 0, -_textOffsetY);
        textGO.transform.localRotation = Quaternion.LookRotation(Vector3.down, Vector3.back);
        coroutine = PickUpAnimation(go, Camera.main.ScreenToWorldPoint(new Vector3(_pickUpAnimEnd.position.x, _pickUpAnimEnd.position.y, 50)));
        StartCoroutine(coroutine);
    }
    private void SetOffCardDiscardAnimation(CardClass card)//, CardClass second)
    {
        GameObject go = Instantiate(_cardObject, _playerMoverObject.transform);
        go.transform.localPosition = new Vector3(_playerObject.transform.position.x, 0, 1f);
        ChangeSkin(VarSaver.Skin, go.GetComponentInChildren<Renderer>(), card);
        if (_playerObject.transform.position.x > 0)
        {
            go.GetComponent<Animator>().SetBool("DiscardRight", true);
        }
        else
        {
            go.GetComponent<Animator>().SetBool("DiscardLeft", true);
        }
    }
    private IEnumerator PickUpSeveral(List<CardClass> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            SetOffCardPickUpAnimation(cards[i]);
            yield return new WaitForSeconds(.2f);
        }
    }
    private IEnumerator DiscardSeveral(List<CardClass> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            SetOffCardDiscardAnimation(cards[i]);
            yield return new WaitForSeconds(.2f);
        }
    }
    private IEnumerator PickUpAnimation(GameObject go, Vector3 endPos)
    {
        Vector3 startPos = go.transform.localPosition;
        Vector3 finishPos = _playerMoverObject.transform.InverseTransformPoint(endPos);
        float elapsedTime = 0;

        while (elapsedTime < _time)
        {
            go.transform.localPosition = Vector3.Lerp(startPos, finishPos, (elapsedTime/_time));
            go.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, (elapsedTime / _time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        EventManager.Broadcast(GameEventsHandler.CardPickUpEvent);
    }
}