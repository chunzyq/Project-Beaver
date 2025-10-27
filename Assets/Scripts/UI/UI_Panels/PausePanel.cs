using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backToMainButton;
    [SerializeField] private GameObject pausePanelUI;

    private bool isPaused = false;

    private void Awake()
    {
        pausePanelUI.SetActive(false);

        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        backToMainButton.onClick.AddListener(OnBackToMainButtonClicked);

        resumeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
        backToMainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back to Main Menu";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnResumeButtonClicked()
    {
        TogglePause();
    }

    private void OnBackToMainButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void TogglePause()
    {
        Debug.Log("Toggling Pause");
        isPaused = !isPaused;
        pausePanelUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
