using UnityEngine;

public class GhostHitPlayer : MonoBehaviour
{
    [SerializeField] private HorrorEventTrigger horrorEvent;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && horrorEvent != null)
        {
            horrorEvent.HitPlayer();
        }
    }
}
