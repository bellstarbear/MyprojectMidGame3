using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorrorEventTrigger : MonoBehaviour
{
    public GameObject ghostShadow;
    public Transform player;

    public float visibleDuration = 3f;
    public float rushSpeed = 15f;

    private bool hasTriggered = false;
    private bool isRushing = false;

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

        yield return new WaitForSeconds(visibleDuration);

        isRushing = true;
    }

    private void Update()
    {
        if (!isRushing || ghostShadow == null || player == null)
            return;

        Vector3 direction =
            (player.position - ghostShadow.transform.position).normalized;

        ghostShadow.transform.position +=
            direction * rushSpeed * Time.deltaTime;
    }

    // เรียกเมื่อผีชนผู้เล่น
    public void HitPlayer()
    {
        if (!isRushing)
            return;

        isRushing = false;

        // สั่งเปลี่ยน Scene ไปหน้า GameOver ทันที (ใส่เครื่องหมายคำพูดครอบชื่อ Scene)
        SceneManager.LoadScene("GameOver");
    }
}