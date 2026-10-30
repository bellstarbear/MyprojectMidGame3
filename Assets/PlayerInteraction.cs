using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject promptUIBottomLeft; // ลาก PromptUI_BottomLeft มาใส่ที่นี่

    private PaperQuizController currentPaper; // เก็บข้อมูลแผ่นกระดาษที่ผู้เล่นกำลังยืนใกล้
    private bool isInRange = false;

    private void Start()
    {
        if (promptUIBottomLeft != null)
            promptUIBottomLeft.SetActive(false);
    }

    private void Update()
    {
        // ถ้าอยู่ในระยะของกระดาษ และกด "E"
        if (isInRange && currentPaper != null && Input.GetKeyDown(KeyCode.E))
        {
            currentPaper.OpenMinigame(); // สั่งให้กระดาษแผ่นนั้นเปิดมินิเกม

            // ซ่อนข้อความแจ้งเตือนเมื่อเปิดมินิเกมแล้ว
            if (promptUIBottomLeft != null)
                promptUIBottomLeft.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ถ้าชนกับวัตถุที่มี Tag เป็น "Paper"
        if (other.CompareTag("Paper"))
        {
            currentPaper = other.GetComponent<PaperQuizController>();
            if (currentPaper != null)
            {
                isInRange = true;
                if (promptUIBottomLeft != null)
                    promptUIBottomLeft.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ถ้าเดินออกจากระยะแผ่นกระดาษ
        if (other.CompareTag("Paper"))
        {
            if (other.GetComponent<PaperQuizController>() == currentPaper)
            {
                isInRange = false;
                currentPaper = null;
                if (promptUIBottomLeft != null)
                    promptUIBottomLeft.SetActive(false);
            }
        }
    }
}