using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class Monster : MonsterState
{
    public MonsterSound monsterSound;
    public float rotationSpeed = 1f;
    private float walkDelay = 5.0f;
    float rot;

    public Transform MonsterArea;
    public bool outOfRange = false;

    private float deadDelay = 3.0f;
    Rigidbody gravity;
    CapsuleCollider capColl;
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

    public float CrashArrwoDistance;
    private void Start()
    {
        AttackRange = 0.5f;
        Hp = 500.0f;
        drop = this.transform.GetComponent<DropTable>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        slid.gameObject.SetActive(false);
        vec = transform.position;
        slid.maxValue = Hp;
        slid.value = Hp;
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

    public MonsterStates State = MonsterStates.Idle;

    public void ChangeState(MonsterStates s) //행동이 바뀔때 최초 한번 실행 하는곳
    {
        if (State == s) return;
        State = s;
        switch(State)
        {
            case MonsterStates.Idle:
            monsterSound.Idle.enabled = true;
            myAnim.SetBool("IsChasing", false);
            myAnim.SetBool("IsWalk", false);
            break;
            case MonsterStates.Walk:
            myAnim.SetBool("IsWalk", true);
            break;
            case MonsterStates.Chase:
            monsterSound.Idle.enabled = false;
            slid.gameObject.SetActive(true);
            break;
            case MonsterStates.Attack:
            break;
            case MonsterStates.Dead:
            drop.DropItem();
            playerController.PlayerExp(Exp);
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
            case MonsterStates.Idle:
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
            ChangeState(MonsterStates.Walk);
            walkDelay = Random.Range(5, 10);
            rot = Random.Range(0, 361);
            }
            break;
            case MonsterStates.Walk:
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
                ChangeState(MonsterStates.Idle);
                walkDelay = 5.0f;
            }
            break;
            case MonsterStates.Chase:
            LookPlayer(PlayerTransform);
            float dis = Vector3.Distance(this.transform.position, PlayerTransform.position);
            if (dis > 10.0f) // 많이 멀어지면 쫓지 않기
            {
                ChangeState(MonsterStates.Idle);
            }
            if(dis < AttackLength)
            {
                ChangeState(MonsterStates.Attack);
                myAnim.SetBool("IsChasing", false);
            }
            break;
            case MonsterStates.Attack:
            dis = Vector3.Distance(this.transform.position, PlayerTransform.position);
            
            LookPlayer(PlayerTransform);
            if(dis > AttackLength)
            {
                ChangeState(MonsterStates.Chase);
                myAnim.SetBool("IsChasing", true);
            }
            StartCoroutine(Attack());
            if (attackDelay > 3.0f)
            {
                int n = Random.Range(0, 2);                
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
            case MonsterStates.Dead:
            deadDelay -= 1.0f * Time.deltaTime;
            if(deadDelay < 0.0f)
            {
                this.transform.position += Vector3.down * Time.deltaTime;
                if(vec.y > this.transform.position.y+3)
                {
                    monsterSound.StopSound();
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
        if (State == MonsterStates.Chase || State == MonsterStates.Dead) return;
        myAnim.SetBool("IsChasing", true);
        ChangeState(MonsterStates.Chase);
    }

    void LookPlayer(Transform pos)
    {
        Vector3 dir = pos.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
    }

    private void OnEnable()
    {
        
        gravity = this.transform.GetComponent<Rigidbody>();
        capColl = this.transform.GetComponent<CapsuleCollider>();
        Hp = 500.0f;
        deadDelay = 3.0f;
        ChangeState(MonsterStates.Idle);
        gravity.useGravity = true;
        capColl.enabled = true;
        slid.maxValue = Hp;
        slid.value = Hp;
    }

    public override void monsterHit(float dam)
    {
        failChase = 0.0f;
        if (!slid.gameObject.activeSelf)
        {
            slid.gameObject.SetActive(true);
        }

        Hp -= dam;
        slid.value = Hp;
        if (Hp <= 0.0f)
        {            
            ChangeState(MonsterStates.Dead);
            return;
        }
        myAnim.SetTrigger("IsHit");
    }
}