using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackDelay = 1f;

    private Transform target;
    private bool isAttacking = false;
    private Animator animator;

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    
}
