using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _runoText;
    private GameObject go;
    private bool runoed = false;
    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonPress);
        EventManager.AddListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<CardNumberChangeEvent>(OnCardNumberChanged);
    }
    private void OnCardNumberChanged(CardNumberChangeEvent obj)
    {
        if (obj.TotalNumber == 1)
        {
            go = _runoText;//[Random.Range(0, _runoText.Length)].gameObject;
            _runoText.SetActive(true);
            runoed = true;
            //IEnumerator coroutine = RunoAnim(go);
            //StartCoroutine(coroutine);
        }
        if(runoed && obj.TotalNumber > 1)
        {
            _runoText.SetActive(false);
            runoed = false;
        }
    }

    private void OnRestartButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator RunoAnim(GameObject go)
    {
        yield return new WaitForSeconds(1.5f);
        go.SetActive(false);
    }
}
