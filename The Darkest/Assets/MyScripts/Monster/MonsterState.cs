using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterState : AnimatorAll
{
    public float Hp;
    public float moveSpeed;
    public string attackType;
    public float Exp = 0.0f;
    public LayerMask enemyMask;
    public Transform PlayerTransform;
    //���� ������
    public static float attackDelay;
    //���� ����
    public static float AttackRange;
    //���� ��Ÿ�
    public float AttackLength;
    public static PlayerController playerController;
    public Transform[] attackPos;
    public enum MonsterStates
    {
        Idle, Walk, Fight, Chase, Attack, Dead, Scream, Sleep, Delay
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
            Debug.Log(attackPos[i]);
            foreach (Collider coll in attack)
            {
                PlayerController playerController = coll.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.Attacked(20.0f, attackType, this.transform);
                    playerController.hit = true;
                    Debug.Log(attackPos[i]);
                }
            }
        }
        yield return new WaitForSeconds(1.0f);
    }
    public abstract void monsterHit(float dam);
}
