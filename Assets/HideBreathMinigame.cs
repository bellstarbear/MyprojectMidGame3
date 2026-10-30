using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HideBreathMinigame : MonoBehaviour
{
    [Header("UI")]
    public GameObject minigamePanel;
    public Slider suspicionSlider;
    public TMP_Text timerText;
    public TMP_Text instructionText;

    [Header("Ghost")]
    public GameObject ghostShadow;
    public bool IsPlaying => isPlaying;

    [Header("Settings")]
    public float surviveTime = 10f;
    public float startingSuspicion = 50f;
    public float increaseSpeed = 20f;
    public float decreaseSpeed = 15f;

    public string gameOverSceneName = "GameOver";

    private float suspicion;
    private float timer;
    private bool isPlaying = false;
    private bool isFinished = false;

    private void Start()
    {
        if (minigamePanel != null)
            minigamePanel.SetActive(false);
    }

    public void StartMinigame()
    {
        if (isPlaying || isFinished)
            return;

        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);

        if (ghostShadow != null)
            ghostShadow.SetActive(true);

        suspicion = startingSuspicion;
        timer = surviveTime;

        isPlaying = true;

        if (minigamePanel != null)
            minigamePanel.SetActive(true);

        if (instructionText != null)
            instructionText.text = "HOLD SPACE TO HOLD YOUR BREATH";

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                suspicion -= decreaseSpeed * Time.deltaTime;
            }
            else
            {
                suspicion += increaseSpeed * Time.deltaTime;
            }

            suspicion = Mathf.Clamp(suspicion, 0f, 100f);

            if (suspicionSlider != null)
                suspicionSlider.value = suspicion;

            if (timerText != null)
                timerText.text = Mathf.Max(0f, timer).ToString("F1");

            if (suspicion >= 100f)
            {
                GameOver();
                yield break;
            }

            yield return null;
        }

        Win();
    }

    private void Win()
    {
        isPlaying = false;
        isFinished = true;

        if (minigamePanel != null)
            minigamePanel.SetActive(false);

        // ซ่อนตัวผีเงา (Ghost Shadow) ทันที
        if (ghostShadow != null)
        {
            ghostShadow.SetActive(false);
        }

        // ค้นหาผีผ่าน Tag "Ghost" (แนะนำให้ไปตั้ง Tag ที่ตัวผีว่า Ghost)
        GameObject ghostObj = GameObject.FindWithTag("Ghost");
        if (ghostObj != null)
        {
            ghostObj.SetActive(false);
        }

        // คืนค่าเวลาและปลดล็อกเมาส์/การเคลื่อนไหว
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("You survived the ghost!");
    }

    private void GameOver()
    {
        isPlaying = false;
        isFinished = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }
}