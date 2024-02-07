using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState : AnimatorAll
{
    public float Hp;
    public float attackDelay;
    public float moveSpeed;
    public string attackType;
    public float dis;
    public float Exp = 0.0f;
    public LayerMask enemyMask;
    public Transform PlayerTransform;
    //���� ����
    public float AttackRange;
    //���� ��Ÿ�
    public float AttackLength;
    public static PlayerController playerController;
    public Transform[] attackPos;
    public enum MonsterStates
    {
        Idle, Walk, Fight, Chase, Attack, Dead, Scream, Sleep
    }
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public IEnumerator Attack()
    {
        for (int i = 0; i < attackPos.Length; i++)
        {
            Collider[] attack = Physics.OverlapSphere(attackPos[i].position, AttackRange, enemyMask);
            foreach (Collider coll in attack)
            {
                PlayerController playerController = coll.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.Attacked(20.0f, attackType);
                    playerController.hit = true;
                }
            }
        }
        yield return null;
    }
    public abstract void monsterHit(float dam);
}
