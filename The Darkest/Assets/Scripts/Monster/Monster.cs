using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f; // 이동 속도
    public float attackRange = 2f; // 공격 가능 범위
    public float attackDelay = 1f; // 공격 딜레이

    private Transform target; // 목표 (플레이어)
    private bool isAttacking = false; // 공격 중인지 여부
    private Animator animator; // 애니메이터

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 목표와의 거리를 계산합니다.
        float distance = Vector3.Distance(transform.position, target.position);

        // 목표와의 거리가 공격 가능 범위 이하인 경우, 공격합니다.
        if (distance <= attackRange && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        // 그렇지 않은 경우, 이동합니다.
        else
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        //target.GetComponent<Character>().TakeDamage(10);
        isAttacking = false;
    }
}
