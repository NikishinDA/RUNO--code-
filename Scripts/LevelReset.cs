using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelReset : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    private void Awake()
    {
        _resetButton.onClick.AddListener(OnResetButtonPress);
    }
    private void OnResetButtonPress()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

