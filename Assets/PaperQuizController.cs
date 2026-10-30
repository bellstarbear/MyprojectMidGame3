using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // เพิ่มไลบรารีสำหรับจัดการ Scene

public class PaperQuizController : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject minigameCanvas; // หน้าต่างมินิเกมคำถาม

    [Header("Quiz Content")]
    [TextArea(2, 5)]
    public string questionText;
    public TMP_Text uiQuestionText;

    [Header("Answers & Button")]
    public Toggle[] answerToggles;
    public int correctToggleIndex;
    public Button submitButton;

    [Header("Scene Transition")]
    public string winSceneName = "WinScene"; // พิมพ์ชื่อ Scene จบเกมของคุณตรงนี้

    private bool isPlayerInRange = false;
    private bool isMinigameOpen = false;

    private void Start()
    {
        if (minigameCanvas != null)
            minigameCanvas.SetActive(false);

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
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void OpenMinigame()
    {
        isMinigameOpen = true;

        if (minigameCanvas != null)
            minigameCanvas.SetActive(true);

        if (uiQuestionText != null)
        {
            uiQuestionText.text = questionText;
        }

        // หยุดเวลาและปลดล็อกเมาส์
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ซ่อนแผ่นกระดาษ
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
            Debug.Log("ตอบถูก! เปลี่ยนไปหน้า WinScene");
            LoadWinScene();
        }
        else
        {
            Debug.Log("ตอบผิด! ลองใหม่อีกครั้ง");
        }
    }

    void LoadWinScene()
    {
        // คืนค่าเวลาก่อนเปลี่ยน Scene เสมอ ไม่งั้น Scene ใหม่เวลาจะหยุดนิ่ง
        Time.timeScale = 1f;

        // โหลด Scene จบเกมตามชื่อที่กำหนด
        SceneManager.LoadScene(winSceneName);
    }
}