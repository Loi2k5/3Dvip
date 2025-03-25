using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public Collider damageCollider;
    public int damageAmount = 20;
    public string targetTag;

    public List<Collider> colliderTargets = new List<Collider>();

    private bool hasAttacked = false; // biến để xác định đã tấn công hay chưa

    void Start()
    {
        damageCollider.enabled = false;
    }

    void Update()
    {
        // Reset trạng thái tấn công nếu điều kiện đã được thiết lập
        if (!damageCollider.enabled)
        {
            hasAttacked = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag) && !colliderTargets.Contains(other) && !hasAttacked)
        {
            colliderTargets.Add(other);
            var player = other.GetComponent<Playermove>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                Debug.Log($"Damaging player: {player.name}");
                hasAttacked = true; // đặt trạng thái tấn công thành true
            }
        }
    }

    public void BeginAttack()
    {
        colliderTargets.Clear();
        damageCollider.enabled = true;
    }

    public void EndAttack()
    {
        colliderTargets.Clear();
        damageCollider.enabled = false;
    }

    // Phương thức này sẽ được gọi bởi sự kiện animation
    public void DoDamage()
    {
        foreach (var target in colliderTargets)
        {
            var player = target.GetComponent<Playermove>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                Debug.Log($"Damaging player: {player.name}");
            }
        }
        hasAttacked = true; // Đặt trạng thái tấn công thành true
    }
}
