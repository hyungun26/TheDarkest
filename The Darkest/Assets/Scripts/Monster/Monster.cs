using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float attackRange = 2f; // ���� ���� ����
    public float attackDelay = 1f; // ���� ������

    private Transform target; // ��ǥ (�÷��̾�)
    private bool isAttacking = false; // ���� ������ ����
    private Animator animator; // �ִϸ�����

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ��ǥ���� �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(transform.position, target.position);

        // ��ǥ���� �Ÿ��� ���� ���� ���� ������ ���, �����մϴ�.
        if (distance <= attackRange && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        // �׷��� ���� ���, �̵��մϴ�.
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
