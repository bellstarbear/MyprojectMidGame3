using System.Collections;
using UnityEngine;

public class HorrorEventTrigger : MonoBehaviour
{
    public GameObject ghostShadow;
    public float visibleDuration = 3f;

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
        StartCoroutine(ShowGhost());
    }

    private IEnumerator ShowGhost()
    {
        if (ghostShadow != null)
        {
            ghostShadow.SetActive(true);
            yield return new WaitForSeconds(visibleDuration);
            ghostShadow.SetActive(false);
        }
    }
}