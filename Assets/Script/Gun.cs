using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;  // Đối tượng đạn
    public Transform bulletSpawn;    // Vị trí bắn đạn
    public float bulletSpeed = 60f;  // Tốc độ đạn
    public float maxDistance = 100f; // Tầm xa tối đa của đạn

    public Transform player;        // Nhân vật
    public AudioSource audioSource; // Nguồn phát âm thanh
    public AudioClip shootSound;    // Âm thanh bắn súng

    void Start()
    {
        player = transform; // Lấy Transform của nhân vật

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Thêm AudioSource nếu chưa có
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Kiểm tra nếu nhấp chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Tạo đạn tại vị trí bulletSpawn và theo hướng quay của nhân vật
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, player.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Thiết lập vận tốc đạn theo hướng quay của nhân vật
        rb.linearVelocity = player.forward * bulletSpeed;

        // Phát âm thanh bắn súng
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Tự hủy đạn sau khi vượt quá tầm xa
        Destroy(bullet, maxDistance / bulletSpeed);
    }
}
