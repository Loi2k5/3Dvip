using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player; // Gán đối tượng Player vào đây trong Inspector
    public Vector3 offset = new Vector3(0, 5, -10); // Điều chỉnh vị trí camera so với player

    void LateUpdate()
    {
        if (player != null)
        {
            // Cập nhật vị trí của camera theo vị trí của player
            transform.position = player.position + offset;

            // Camera luôn nhìn vào player
            transform.LookAt(player);
        }
    }
}