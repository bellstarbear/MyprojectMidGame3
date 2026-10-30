using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // ใส่ชื่อ Scene ของเกมหลักที่คุณต้องการให้รีสตาร์ทกลับไปเล่น (เช่น ชื่อ Scene "Game" หรือชื่อฉากเกมของคุณ)
    [SerializeField] private string gameplaySceneName = "Game";

    void Update()
    {
        // ถ้าผู้เล่นกดปุ่ม R บนคีย์บอร์ดในหน้า Game Over
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้เดินปกติ
        SceneManager.LoadScene(gameplaySceneName);
    }
}