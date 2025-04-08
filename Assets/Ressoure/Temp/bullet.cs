using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 20; // Lượng sát thương gây ra

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Enemy"
        if (other.CompareTag("Ske"))
        {
            // Lấy thành phần "Heath" từ đối tượng va chạm
            Heath enemyHealth = other.GetComponent<Heath>();
            if (enemyHealth != null)
            {
                // Gây sát thương cho đối tượng
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log($"Damaged enemy: {other.name}, remaining health: {enemyHealth.currentHP}");

                // Hủy viên đạn sau khi gây sát thương
                Destroy(gameObject);
            }
        }
    }
}
