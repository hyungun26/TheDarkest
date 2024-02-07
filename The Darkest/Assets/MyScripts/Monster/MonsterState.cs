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
    public enum MonsterStates
    {
        Idle, Walk, Fight, Chase, Attack, Dead, Scream, Sleep
    }
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
