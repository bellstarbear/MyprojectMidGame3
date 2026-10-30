using UnityEngine;

public class GhostHitPlayer : MonoBehaviour
{
    // ลาก GameObject ที่มีสคริปต์ HorrorEventTrigger มาใส่ในช่องนี้ผ่านหน้าต่าง Inspector
    [SerializeField] private HorrorEventTrigger horrorEvent;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าชนกับ Player และตั้งค่าสคริปต์ HorrorEvent ไว้แล้ว
        if (other.CompareTag("Player") && horrorEvent != null)
        {
            horrorEvent.HitPlayer();
        }
    }
}