
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorrorEventTrigger : MonoBehaviour
{
    public GameObject ghostShadow;
    public Transform player;

    public float visibleDuration = 3f;
    public float rushSpeed = 15f;
    public float rushDuration = 0.5f;

    public string gameOverSceneName = "GameOver";

    private bool hasTriggered = false;

    private void Start()
    {
        if (ghostShadow != null)
            ghostShadow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player"))
            return;

        hasTriggered = true;
        player = other.transform;

        StartCoroutine(ShowGhost());
    }

    private IEnumerator ShowGhost()
    {
        if (ghostShadow == null || player == null)
            yield break;

        ghostShadow.SetActive(true);

        // แสดงเงา 3 วินาที
        yield return new WaitForSeconds(visibleDuration);

        // พุ่งเข้าหาผู้เล่น
        Vector3 direction =
            (player.position - ghostShadow.transform.position).normalized;

        float elapsed = 0f;

        while (elapsed < rushDuration)
        {
            ghostShadow.transform.position +=
                direction * rushSpeed * Time.deltaTime;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // จบเกมด้วยการเปลี่ยน Scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}
