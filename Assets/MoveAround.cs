using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0, 0, 0); // Vị trí ban đầu
    public float radius = 5.0f; // Bán kính di chuyển
    public float speed = 1.0f; // Tốc độ di chuyển

    private float angle = 0.0f;

    void Start()
    {
        // Đặt vị trí ban đầu của đối tượng
        transform.position = initialPosition;
    }

    void Update()
    {
        // Cập nhật vị trí của đối tượng theo thời gian
        float x = initialPosition.x + radius * Mathf.Cos(angle);
        float y = initialPosition.y + radius * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, initialPosition.z);

        // Cập nhật góc
        angle += speed * Time.deltaTime;
    }
}
