using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;  // Đối tượng đạn
    public Transform bulletSpawn;    // Vị trí bắn đạn
    public float bulletSpeed = 60f;  // Tốc độ đạn
    public float maxDistance = 100f; // Tầm xa tối đa của đạn

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Kiểm tra nếu nhấp chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawn.forward * bulletSpeed;  // Đẩy đạn về phía trước
        Destroy(bullet, maxDistance / bulletSpeed);  // Tự hủy đạn sau khi vượt quá tầm xa
    }
}
