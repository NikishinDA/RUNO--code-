using UnityEngine;
using UnityEngine.UI;

public class CoinMeter : MonoBehaviour
{
    [SerializeField] GameObject _resultSaver;
    [SerializeField] Text _coinText;
    private int _coinNumber;

    private void Awake()
    {
        EventManager.AddListener<CoinPickUpEvent>(OnCoinNumberChange);
        EventManager.AddListener<CoinPickUpAnimationEndEvent>(OnCoinPickUpAnimationEnd);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<CoinPickUpEvent>(OnCoinNumberChange);
        EventManager.RemoveListener<CoinPickUpAnimationEndEvent>(OnCoinPickUpAnimationEnd);
    }
    private void OnCoinNumberChange(CoinPickUpEvent obj)
    {
        _coinNumber++;
        VarSaver.CurrentNumberOfCoins = _coinNumber;
        PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins") + 1);
        PlayerPrefs.Save();
    }
    private void OnCoinPickUpAnimationEnd(CoinPickUpAnimationEndEvent obj)
    {
        _coinText.GetComponent<Animator>().SetTrigger("PickUp");
        _coinText.text = _coinNumber.ToString();
    }
}
