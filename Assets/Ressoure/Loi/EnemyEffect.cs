using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    public GameObject deathEffect;

    // Phương thức này được gọi khi quái bị giết
    public void Die()
    {
        // Tạo hiệu ứng chết
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Hủy đối tượng quái
        Destroy(gameObject);
    }
}
