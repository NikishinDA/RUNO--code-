using System.Collections;
using UnityEngine;

public class CardText : MonoBehaviour
{
    [SerializeField] private string[] _resaultTexts;
    private void OnEnable()
    {
        GetComponent<TextMesh>().text = _resaultTexts[Random.Range(0, _resaultTexts.Length)];

        StartCoroutine("LifeTime");
    }
    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
