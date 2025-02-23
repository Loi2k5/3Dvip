using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target; // Mục tiêu

    public float searchRadius = 10f; // Bán kính tìm kiếm mục tiêu
    public Vector3 originalPosition; // Vị trí ban đầu
    public float maxDistance = 50f; // Khoảng cách tối đa từ vị trí ban đầu
    public float maxHP = 100f;
    public float currentHP;

    public Animator animator; // Khai báo component Animator

    // State machine
    public enum CharacterState
    {
        Idle,
        Chase,
        Attack,
        Die
    }
    public CharacterState currentState; // Trạng thái hiện tại

    public float attackRange = 2f; // Phạm vi tấn công
    public float attackCooldown = 1f; // Thời gian chờ giữa các lần tấn công
    private float lastAttackTime;

    void Start()
    {
        originalPosition = transform.position;
        currentHP = maxHP;
        lastAttackTime = -attackCooldown; // Cho phép tấn công ngay khi bắt đầu
        ChangeState(CharacterState.Idle);
    }

    void Update()
    {
        if (currentState == CharacterState.Die)
        {
            return;
        }

        float distanceToOriginal = Vector3.Distance(originalPosition, transform.position);
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        switch (currentState)
        {
            case CharacterState.Idle:
                if (distanceToTarget <= searchRadius && distanceToOriginal <= maxDistance)
                {
                    ChangeState(CharacterState.Chase);
                }
                break;

            case CharacterState.Chase:
                navMeshAgent.SetDestination(target.position);
                animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

                if (distanceToTarget <= attackRange)
                {
                    ChangeState(CharacterState.Attack);
                }

                if (distanceToTarget > searchRadius || distanceToOriginal > maxDistance)
                {
                    ChangeState(CharacterState.Idle);
                }
                break;

            case CharacterState.Attack:
                animator.SetFloat("Speed", 0); // Dừng di chuyển khi tấn công
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                    // Gọi hàm tấn công ở đây (ví dụ: gây sát thương cho mục tiêu)
                    Attack();
                }

                if (distanceToTarget > attackRange)
                {
                    ChangeState(CharacterState.Chase);
                }
                break;
        }

        if (currentState == CharacterState.Idle)
        {
            navMeshAgent.SetDestination(originalPosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distanceToOriginal < 1f)
            {
                animator.SetFloat("Speed", 0);
            }
        }

        // Cập nhật giá trị Current HP nếu bị tấn công
        if (currentHP < maxHP && currentState != CharacterState.Die)
        {
            currentHP = Mathf.Min(maxHP, currentHP + Time.deltaTime * 5); // Tự hồi máu chậm
        }
    }

    private void ChangeState(CharacterState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case CharacterState.Idle:
                break;

            case CharacterState.Chase:
                break;

            case CharacterState.Attack:
                break;

            case CharacterState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 6f);
                GetComponent<Collider>().enabled = false;
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        if (currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
        }
    }

    private void Attack()
    {
        // Thực hiện logic tấn công ở đây (ví dụ: gây sát thương cho mục tiêu)
        Debug.Log("Enemy attacked!");
        // Ví dụ: target.GetComponent<Health>().TakeDamage(damage);
    }

    // Optional: Draw search radius and attack range in the Scene view for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}