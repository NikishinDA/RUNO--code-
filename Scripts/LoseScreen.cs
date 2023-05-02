using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] private Text _coinText;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonPress);
    }
    private void Start()
    {
        _coinText.text = "x" + VarSaver.CurrentNumberOfCoins;
    }
    private void OnRestartButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
