
using UnityEngine;

public class HideSystem : MonoBehaviour
{
    public Transform hidePoint;
    public MonoBehaviour playerMovement;

    private Transform player;
    private CharacterController characterController;
    private Collider[] playerColliders;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isNearby;
    private bool isHidden;

    void Update()
    {
        if (isNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHidden)
                HidePlayer();
            else
                ExitHide();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isHidden)
            return;

        player = other.transform.root;

        characterController =
            player.GetComponent<CharacterController>();

        playerColliders =
            player.GetComponentsInChildren<Collider>();

        isNearby = true;
        Debug.Log("Press E to Hide");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isHidden)
        {
            isNearby = false;
            Debug.Log("Hide area exited");
        }
    }

    void HidePlayer()
    {
        if (player == null || hidePoint == null)
            return;

        originalPosition = player.position;
        originalRotation = player.rotation;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (characterController != null)
            characterController.enabled = false;

        player.position = hidePoint.position;
        player.rotation = hidePoint.rotation;

        foreach (Collider col in playerColliders)
            col.enabled = false;

        isHidden = true;
        Debug.Log("You are hiding");
    }

    void ExitHide()
    {
        if (player == null)
            return;

        player.position = originalPosition;
        player.rotation = originalRotation;

        foreach (Collider col in playerColliders)
            col.enabled = true;

        if (characterController != null)
            characterController.enabled = true;

        if (playerMovement != null)
            playerMovement.enabled = true;

        isHidden = false;
        isNearby = false;

        Debug.Log("You left the hiding spot");
    }
}
