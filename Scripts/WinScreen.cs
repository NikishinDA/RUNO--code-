using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Text _cardText;
    [SerializeField] private Text _coinText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject _goodResult;
    [SerializeField] private GameObject _perfectResult;
    [SerializeField] private int _lessCardsForPerfect;
    [SerializeField] private int _lessCardsForTwoStars;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonPress);
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonPress);
    }
    private void Start()
    {
        _stars[0].SetActive(true);
        if (VarSaver.NumberOfCards < _lessCardsForTwoStars)
        {
            _stars[1].SetActive(true);
        }
        if(VarSaver.NumberOfCards < _lessCardsForPerfect)
        {
            _stars[2].SetActive(true);
            _perfectResult.SetActive(true);
        }
        else
        {
            _goodResult.SetActive(true);
        }
        _cardText.text = "x" + VarSaver.NumberOfCards;
        _coinText.text = VarSaver.CurrentNumberOfCoins.ToString();
    }
    private void OnRestartButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnNextLevelButtonPress()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level",1) + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
