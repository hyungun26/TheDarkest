using UnityEngine;
using UnityEngine.TextCore.Text;

public class Monster : AnimatorAll
{
    public float hp = 500.0f;
    public Transform PlayerTransform;
    public float moveSpeed = 3f;
    public float rotationSpeed = 1f;
    public float attackRange = 4f;
    public float attackDelay = 1f;
    private float walkDelay = 5.0f;
    private string attackType = "WeekAttack";

    private Transform target;
    private bool isAttacking = false;
    private Animator animator;
    float rot = 0.0f;

    public Transform arm1;
    public Transform arm2;
    public LayerMask enemyMask;

    private float deadDelay = 3.0f;
    public Rigidbody gravity;
    public CapsuleCollider capColl;
    Vector3 vec;
    private void Start()
    {
        vec = transform.position;
    }

    private void Update()
    {
        if(attackDelay < 3.0f)
        {
            attackDelay += 1.0f * Time.deltaTime;
        }

        StateProcess();        
    }

    public enum MonsterState
    {
        Idle, Walk, Chase, Attack, Dead
    }

    public MonsterState State = MonsterState.Idle;

    public void ChangeState(MonsterState s) //행동이 바뀔때 최초 한번 실행 하는곳
    {
        if (State == s) return;
        State = s;
        switch(State)
        {
            case MonsterState.Idle:
            myAnim.SetBool("IsChasing", false);
            myAnim.SetBool("IsWalk", false);
            break;
            case MonsterState.Walk:
            myAnim.SetBool("IsWalk", true);
            break;
            case MonsterState.Chase:
            break;
            case MonsterState.Attack:
            break;
            case MonsterState.Dead:
            myAnim.SetTrigger("IsDead");
            gravity.useGravity = false;
            capColl.enabled = false;
            break;
        }
    }
    public void StateProcess()
    {
        switch(State)
        {
            case MonsterState.Idle:
            walkDelay -= 1.0f * Time.deltaTime;
            if(walkDelay < 0.0f)
            {
                ChangeState(MonsterState.Walk);
                walkDelay = Random.RandomRange(5, 10);
                rot = Random.RandomRange(0, 361); 
            }
            break;
            case MonsterState.Walk:
            walkDelay -= 1.0f * Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(rot, Vector3.up), 2.0f * Time.deltaTime);
            if(walkDelay < 0.0f)
            {
                ChangeState(MonsterState.Idle);
                walkDelay = 5.0f;
            }
            break;
            case MonsterState.Chase:
            LookPlayer(PlayerTransform);
            if(Vector3.Distance(this.transform.position, PlayerTransform.position) > 10.0f) // 많이 멀어지면 쫓지 않기
            {
                ChangeState(MonsterState.Idle);
            }
            if(Vector3.Distance(this.transform.position, PlayerTransform.position) < attackRange)
            {
                ChangeState(MonsterState.Attack);
                myAnim.SetBool("IsChasing", false);
            }
            break;
            case MonsterState.Attack:
            Attack();
            LookPlayer(PlayerTransform);
            if(Vector3.Distance(this.transform.position, PlayerTransform.position) > attackRange)
            {
                ChangeState(MonsterState.Chase);
                myAnim.SetBool("IsChasing", true);
            }
            if(attackDelay > 3.0f)
            {
                int n = Random.Range(0,2);
                attackDelay = 0.0f;
                switch(n)
                {
                    case 0:
                    myAnim.SetTrigger("IsAttack1");
                    break;
                    case 1:
                    myAnim.SetTrigger("IsAttack2");
                    break;
                }
            }
            break;
            case MonsterState.Dead:
            deadDelay -= 1.0f * Time.deltaTime;
            if(deadDelay < 0.0f)
            {
                this.transform.position += Vector3.down * Time.deltaTime;
                if(vec.y > this.transform.position.y+3)
                {
                    this.gameObject.SetActive(false);
                }
            }
            break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(State == MonsterState.Chase) return;
            myAnim.SetBool("IsChasing", true);
            ChangeState(MonsterState.Chase);
        }
    }

    void LookPlayer(Transform player)
    {
        Vector3 dir = player.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
    }

    void GiveUpChase() //현재 애니메이션 상태가 무엇인지알 수 있는 코드
    {
        if(myAnim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Idle"))
        {
            ChangeState(MonsterState.Idle);
        }
    }

    void Attack()
    {
        Collider[] list1 = Physics.OverlapSphere(arm1.position, 0.5f, enemyMask);
        Collider[] list2 = Physics.OverlapSphere(arm2.position, 0.5f, enemyMask);
        foreach(Collider coll in list1)
        {
            PlayerController playerController = coll.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.Attacked(20.0f, attackType);
                playerController.hit = true;
            }
        }
        foreach(Collider coll in list2)
        {
            PlayerController playerController = coll.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.Attacked(20.0f, attackType);
                playerController.hit = true;
            }
        }
    }

    public void monsterHit(float dam)
    {
        hp -= dam;
        if(hp <= 0.0f)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
        myAnim.SetTrigger("IsHit");
    }
}