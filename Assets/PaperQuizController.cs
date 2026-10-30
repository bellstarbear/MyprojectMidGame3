using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PaperQuizController : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject minigameCanvas;
    public GameObject interactionPromptText;

    [Header("Quiz Content")]
    [TextArea(2, 5)]
    public string questionText;
    public TMP_Text uiQuestionText;

    [Header("Answers & Button")]
    public Toggle[] answerToggles;
    public int correctToggleIndex;
    public Button submitButton;

    [Header("Scene Transition")]
    public string winSceneName = "WinScene";

    private bool isPlayerInRange = false;
    private bool isMinigameOpen = false;

    private void Start()
    {
        if (minigameCanvas != null)
            minigameCanvas.SetActive(false);

        if (interactionPromptText != null)
            interactionPromptText.SetActive(false);

        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitAnswer);
    }

    private void Update()
    {
        if (isPlayerInRange && !isMinigameOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenMinigame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter กับ: " + other.name + " | Tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (interactionPromptText != null && !isMinigameOpen)
            {
                interactionPromptText.SetActive(true);
                Debug.Log("เปิดข้อความ Press E สำเร็จ!");
            }
            else
            {
                Debug.LogWarning("interactionPromptText เป็นค่า Null หรือมินิเกมเปิดอยู่!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (interactionPromptText != null)
            {
                interactionPromptText.SetActive(false);
            }
        }
    }

    public void OpenMinigame()
    {
        isMinigameOpen = true;

        if (interactionPromptText != null)
            interactionPromptText.SetActive(false);

        if (minigameCanvas != null)
            minigameCanvas.SetActive(true);

        if (uiQuestionText != null)
        {
            uiQuestionText.text = questionText;
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (gameObject.GetComponent<Renderer>() != null)
            gameObject.GetComponent<Renderer>().enabled = false;
        if (gameObject.GetComponent<Collider>() != null)
            gameObject.GetComponent<Collider>().enabled = false;
    }

    public void OnSubmitAnswer()
    {
        bool isCorrect = false;

        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (answerToggles[i].isOn)
            {
                if (i == correctToggleIndex)
                {
                    isCorrect = true;
                }
                break;
            }
        }

        if (isCorrect)
        {
            LoadWinScene();
        }
    }

    void LoadWinScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(winSceneName);
    }
}