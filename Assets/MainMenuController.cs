using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
        exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit Game";
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
        #endif
    }
}
