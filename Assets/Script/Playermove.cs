using UnityEngine;

public class Playermove : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float speed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    Vector3 velocity;
    float jumpCooldown = 1.1f;
    float nextJumpTime = 0f;

    public Animator animator;

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Xoay theo hướng di chuyển
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            animator.SetFloat("speed", 0f);

            // 🔹 FLIP theo hướng camera khi đứng yên 🔹
            float cameraY = cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, cameraY, 0f);
        }

        if (Input.GetButtonDown("Jump") && Time.time >= nextJumpTime)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            nextJumpTime = Time.time + jumpCooldown;
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}