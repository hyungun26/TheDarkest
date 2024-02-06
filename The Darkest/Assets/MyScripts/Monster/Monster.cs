using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class Monster : AnimatorAll
{
    private float hp = 500.0f;
    public Transform PlayerTransform;
    PlayerController player;
    public float moveSpeed = 3f;
    public float rotationSpeed = 1f;
    public float attackRange = 4f;
    public float attackDelay = 1f;
    private float walkDelay = 5.0f;
    private string attackType = "WeekAttack";
    public float Exp = 0.0f;

    float dis;
    private Transform target;
    private bool isAttacking = false;
    private Animator animator;
    float rot = 0.0f;
    public Transform MonsterArea;
    public bool outOfRange = false;

    public Transform arm1;
    public Transform arm2;
    public LayerMask enemyMask;

    private float deadDelay = 3.0f;
    public Rigidbody gravity;
    public CapsuleCollider capColl;
    Vector3 vec;

    //hp bar
    public Transform Head;
    public Slider slid;
    private new Camera camera;
    public float failChase = 0.0f;

    //spawner
    public Spawner spawner;

    //drop table
    DropTable drop;
    private void Start()
    {
        drop = this.transform.GetComponent<DropTable>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        GameObject ob = GameObject.Find("MainCamera");
        camera = ob.GetComponent<Camera>();
        slid.gameObject.SetActive(false);
        vec = transform.position;
        slid.maxValue = hp;
        slid.value = hp;
    }
    private void LateUpdate()
    {
        slid.transform.position = camera.WorldToScreenPoint(Head.position);

        //만들어야할것 거리가 멀면 bar를 비활성화하고 맞았을때 활성화 할것
        //몬스터 위에 체력바가 다른곳을 봤을때 보이는것을 방지하기 위한 if문
        if (slid.transform.position.z < 0.0f)
        {
            slid.transform.position *= -1.0f;
        }
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
            slid.gameObject.SetActive(true);
            break;
            case MonsterState.Attack:
            break;
            case MonsterState.Dead:
            drop.DropItem();
            player.PlayerExp(Exp);
            slid.gameObject.SetActive(false);
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
            //player와 거리가 멀고 더이상 공격하지 않는다 판단하면 hpSlider 끄는 코드
            if (slid.gameObject.activeSelf)
            {
                failChase += 1.0f * Time.deltaTime;
                if (failChase > 10.0f)
                {
                    slid.gameObject.SetActive(false);
                    failChase = 0.0f;
                }
            }
            walkDelay -= 1.0f * Time.deltaTime;
            if (walkDelay < 0.0f)
            {
            ChangeState(MonsterState.Walk);
            walkDelay = Random.RandomRange(5, 10);
            rot = Random.RandomRange(0, 361);
            }
            break;
            case MonsterState.Walk:
            walkDelay -= 1.0f * Time.deltaTime;
            if(!outOfRange)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(rot, Vector3.up), 2.0f * Time.deltaTime);
            }
            else
            {
                LookPlayer(MonsterArea);
            }
            if (walkDelay < 0.0f)
            {
                ChangeState(MonsterState.Idle);
                walkDelay = 5.0f;
            }
            break;
            case MonsterState.Chase:
            LookPlayer(PlayerTransform);
            dis = Vector3.Distance(this.transform.position, PlayerTransform.position);
            if (dis > 10.0f) // 많이 멀어지면 쫓지 않기
            {
                ChangeState(MonsterState.Idle);
            }
            if(dis < attackRange)
            {
                ChangeState(MonsterState.Attack);
                myAnim.SetBool("IsChasing", false);
            }
            break;
            case MonsterState.Attack:
            dis = Vector3.Distance(this.transform.position, PlayerTransform.position);
            Attack();
            LookPlayer(PlayerTransform);
            if(dis > attackRange)
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
                    if(!transform.gameObject.activeSelf)
                        spawner.num--;
                }
            }
            break;
        }
    }

    public void Search()
    {
        if (State == MonsterState.Chase || State == MonsterState.Dead) return;
        myAnim.SetBool("IsChasing", true);
        ChangeState(MonsterState.Chase);
    }

    void LookPlayer(Transform pos)
    {
        Vector3 dir = pos.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
    }
        
    void GiveUpChase() //현재 애니메이션 상태가 무엇인지알 수 있는 코드
    {
        //if(myAnim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Idle"))
        //{
        //    ChangeState(MonsterState.Idle);
        //}
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
        failChase = 0.0f;
        if(!slid.gameObject.activeSelf)
        {
            slid.gameObject.SetActive(true);
        }
        hp -= dam;
        slid.value = hp;
        if(hp <= 0.0f)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
        myAnim.SetTrigger("IsHit");
    }

    private void OnEnable()
    {
        hp = 500.0f;
        deadDelay = 3.0f;
        ChangeState(MonsterState.Idle);
        gravity.useGravity = true;
        capColl.enabled = true;
        slid.maxValue = hp;
        slid.value = hp;
    }
}