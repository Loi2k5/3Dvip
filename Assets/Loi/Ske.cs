using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ske : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public string targetTag = "Player"; // tag của mục tiêu

    public float radius = 10f; // bán kính tìm kiếm mục tiêu
    public Vector3 originalePosition; // vị trí ban đầu
    public float maxDistance = 50f; // khoảng cách tối đa

    public Animator animator; // khai báo component
    public DamageZone damageZone;
    public Heath heath;

    private Transform target; // mục tiêu

    // state machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState; // trạng thái hiện tại

    void Start()
    {
        originalePosition = transform.position;
        FindTarget();
    }

    void Update()
    {
        if (heath.currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
        }

        if (currentState == CharacterState.Die)
        {
            return;
        }

        if (target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);

            // khoảng cách từ vị trí hiện tại đến vị trí ban đầu
            var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
            // khoảng cách từ vị trí hiện tại đến mục tiêu
            var distance = Vector3.Distance(target.position, transform.position);
            if (distance <= radius && distanceToOriginal <= maxDistance)
            {
                // di chuyển đến mục tiêu
                navMeshAgent.SetDestination(target.position);
                animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

                distance = Vector3.Distance(target.position, transform.position);
                if (distance < 2f)
                {
                    // tấn công
                    ChangeState(CharacterState.Attack);
                }
                else if (currentState == CharacterState.Attack && distance >= 2f)
                {
                    // kết thúc tấn công khi mục tiêu rời khỏi phạm vi
                    ChangeState(CharacterState.Normal);
                }
            }
            else
            {
                // quay về vị trí ban đầu
                navMeshAgent.SetDestination(originalePosition);
                animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

                // chuyển sang trạng thái đứng yên
                distance = Vector3.Distance(originalePosition, transform.position);
                if (distance < 1f)
                {
                    animator.SetFloat("Speed", 0);
                    ChangeState(CharacterState.Normal);
                }
            }
        }
    }

    // tìm mục tiêu theo tag
    private void FindTarget()
    {
        GameObject targetObject = GameObject.FindWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target with tag " + targetTag + " not found!");
        }
    }

    // chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {
        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                break;
        }

        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 1f);
                break;
        }

        // update current state
        currentState = newState;
    }
}
