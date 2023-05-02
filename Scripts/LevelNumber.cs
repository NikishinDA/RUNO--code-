using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    [SerializeField] Text _levelNumberText;

    private void Start()
    {
        _levelNumberText.text = "Level " + PlayerPrefs.GetInt("level", 1).ToString();
    }
}
