using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonsterState
{
    //드래곤에 문제점 움직임이 이상함 고쳐야함
    public GameObject DragonUI;
    public NavMeshAgent Agent;
    public AnimationEvent animEvent;
    bool wakeUp = false;
    bool scream = false;
    bool run = false;
    [SerializeField]
    bool attackPossible = false;
    float runORwalk = 10.0f;
    
    float DHP;

    //공격 부위
    public Transform AttackPoint;
    public Transform AttackPoint1;
    public Transform AttackPoint2;
    public Transform Head;

    int rnd;
    Vector3 dir = Vector3.zero;

    //effect on off & dead State Production
    public GameObject[] smokeEffect;
    public Material changeMaterial;
    public SkinnedMeshRenderer dragonMaterial;
    float changeTime = 3.0f;
    float decreaseMaterial = 0f;

    //dragon hp effect
    float lerpTime = 50f;
    float currentTime = 0.0f;
    bool once = false;

    public PlayerStat PlayerStat;
    
    MonsterStates State = MonsterStates.Sleep;
  
    void Start()
    {
        Agent.speed = moveSpeed;
        DragonUI = GameObject.Find("Dragon");
        Hp = 5000.0f;
        DHP = Hp;
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

        if (Hp <= 0.0f)
        {
            ChangeState(MonsterStates.Dead);
        }
        else if (Hp != DragonUI.GetComponent<UIController>().DecreaseHP.value && !wakeUp)
        {
            ChangeState(MonsterStates.Walk);
            wakeUp = true;
        }

        //테스트 어택 딜레이
        attackDelay -= Time.deltaTime;
        if(DHP != Hp)
        {
            DragonUI.GetComponent<UIController>().DecreaseHP.value = Hp;
            DHP = Hp;
        }

        if (DragonUI.GetComponent<UIController>().DecreaseHP2.value != DragonUI.GetComponent<UIController>().DecreaseHP.value)
        {
            if (!once)
            {
                once = true;
                StartCoroutine(HpEffect());
            }
        }
    }

    public void ChangeState(MonsterStates s) //행동이 바뀔때 최초 한번 실행 하는곳
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case MonsterStates.Sleep:
                break;
            case MonsterStates.Scream:
                playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                PlayerTransform = playerController.gameObject.transform;
                DragonUI.GetComponent<UIController>().dragonState = true; // UI생성
                break;
            case MonsterStates.Walk:
                break;
            case MonsterStates.Idle:
                break;
            case MonsterStates.Fight:
                break;
            case MonsterStates.Dead:
                playerController.PlayerExp(Exp);
                DragonUI.GetComponent<UIController>().dragonDead = true; // 값변경
                DragonUI.GetComponent<UIController>().dragonState = true; // UI삭제
                myAnim.SetTrigger("IsDead");
                break;
        }
    }

    public void StateProcess() //행동중 계속 해야하는 곳
    {
        switch (State)
        {
            case MonsterStates.Scream:
                myAnim.SetTrigger("IsScream");
                ChangeState(MonsterStates.Walk);
                break;
            case MonsterStates.Walk:
                AttackLength = 5.0f;
                if (!scream) // 1회성 코드
                {
                    myAnim.SetBool("IsWalk", true);
                    scream = true;
                    ChangeState(MonsterStates.Scream);
                }
                if (animEvent.Scream)
                {
                    if(playerController.MyState == PlayerController.PlayerState.Play) //플레이어가 살아있다면 쫓아가라
                    {
                        dis = Vector3.Distance(this.transform.position, PlayerTransform.position);
                        if (dis < AttackLength) // player 공격사거리
                        {
                            animEvent.Fight = false;
                            ChangeState(MonsterStates.Fight);
                        }
                        dir = PlayerTransform.transform.position - this.transform.position;
                        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
                        if (dis > runORwalk && !run && !attackPossible) //거리가 멀면 뛰어가라
                        {
                            Agent.isStopped = false;
                            run = true;
                            Agent.speed = 60.0f;
                            myAnim.SetTrigger("IsChaingRun");
                        }
                        else if(dis < runORwalk && !run && !attackPossible) //거리가 가까우면 걸어가라
                        {
                            Agent.isStopped = false;
                            run = true;
                            Agent.speed = 10.0f;
                            myAnim.SetTrigger("IsChaingWalk");
                        }
                        Agent.SetDestination(PlayerTransform.position);
                    }
                }
                break;
            case MonsterStates.Idle:
                if(attackDelay > 0)
                {
                    ChangeState(MonsterStates.Idle);
                }
                else
                {
                    ChangeState(MonsterStates.Fight);
                }
                //플레이어가 죽으면 여기로 들어온다.
                break;
            case MonsterStates.Fight:
                if (Vector3.Distance(this.transform.position, PlayerTransform.position) < AttackLength)
                {
                    attackPossible = true; // 공격가능
                }
                else
                {
                    attackPossible = false; // 공격 불가능
                }
                if (playerController.MyState == PlayerController.PlayerState.Play) //player가 죽거나 넘어져있는 상태가 아닐때만 공격
                {
                    AttackLength = 6.0f;
                    //AnimationEvent 에서 Fight false 처리중 애니메이션이 끝나면 다시 들어와서 공격함

                    //문제점
                    //fight상태에서 못나올때가 있음 animEvent.Fight가 true처리 안될때가 있음
                    //공격범위 안에 있지만 걷는다. 이유를 파악함 플레이어가 hitdown으로 거리가 attackRange보다 조금 멀어짐 그래서
                    //걷는 상태로 들어가자마자 공격범위 안에 들어와서 공격을 하는 부자연스러운 현상이 생김
                    if (attackDelay <= 0.0f)
                    {
                        Agent.isStopped = true;
                        Agent.velocity = Vector3.zero;
                        attackDelay = 5.0f;
                        animEvent.Fight = false;
                        rnd = Random.Range(0, 3); // 0 ~ 2
                        switch (rnd)
                        {
                            case 0:
                                AttackRange = 1.5f;
                                myAnim.SetTrigger("IsBasicAttack");
                                break;
                            case 1:
                                AttackRange = 2f;
                                myAnim.SetTrigger("IsClawAttack");
                                break;
                            case 2:
                                AttackRange = 5f;
                                myAnim.SetTrigger("IsBreath");
                                break;
                            default:
                                return;
                        }
                    }

                    if (attackDelay > 0.0f) // 공격 딜레이에 걸려있으면 쳐다봐라 player를
                    {
                        dir = PlayerTransform.transform.position - this.transform.position;
                        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
                    }

                    switch (rnd)
                    {
                        case 0:
                            RangeAttack1();
                            break;
                        case 1:
                            RangeAttack2();
                            break;
                    }

                    if (!attackPossible && animEvent.Fight) // 범위 안이 아니면 빠져나가야함
                    {
                        ChangeState(MonsterStates.Walk);
                        run = false;
                    }
                }
                break;
            case MonsterStates.Dead:
                changeTime -= 1.0f * Time.deltaTime;

                if (changeTime <= 0.0f)
                {
                    for (int i = 0; i < smokeEffect.Length; i++)
                    {
                        smokeEffect[i].SetActive(true);
                    }
                    if (changeTime < -5.0f)
                    {
                        dragonMaterial.material = changeMaterial;
                        dragonMaterial.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        dragonMaterial.material.SetFloat("_DissolveAmount", decreaseMaterial);
                        decreaseMaterial += 0.2f * Time.deltaTime;
                        if (decreaseMaterial > 1.0f)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
                break;
        }
    }

    void RangeAttack1()
    {
        Collider[] list = Physics.OverlapSphere(AttackPoint.position, AttackRange, enemyMask);
        
        foreach (Collider coll in list)
        {
            PlayerController playerController = coll.GetComponent<PlayerController>();
            if(playerController != null)
            {
                if (!coll.GetComponent<PlayerController>().invincibility)
                {
                    //if(coll.GetComponent<PlayerController>().MyState != PlayerController.PlayerState.Die)
                    //coll.GetComponent<PlayerController>().hit = true;
                    // coll.GetComponent<PlayerController>().Attacked(100f, attackType);
                    // coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
                    playerController.Attacked(20.0f, attackType);
                    playerController.hit = true;
                }
            }
        }
    }

    void RangeAttack2()
    {
        Collider[] list1 = Physics.OverlapSphere(AttackPoint1.position, AttackRange, enemyMask);
        Collider[] list2 = Physics.OverlapSphere(AttackPoint2.position, AttackRange, enemyMask);
        foreach (Collider coll in list1)
        {
            PlayerController playerController = coll.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (!coll.GetComponent<PlayerController>().invincibility)
                {
                    // coll.GetComponent<PlayerController>().hit = true;
                    // coll.GetComponent<PlayerController>().Attacked(70f, attackType);
                    // coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
                    playerController.Attacked(20.0f, attackType);
                    playerController.hit = true;    
                }
            }
        }
        foreach (Collider coll in list2)
        {
            PlayerController playerController = coll.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (!coll.GetComponent<PlayerController>().invincibility)
                {
                    // coll.GetComponent<PlayerController>().hit = true;
                    // coll.GetComponent<PlayerController>().Attacked(70f, attackType);
                    // coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
                    playerController.Attacked(20.0f, attackType);
                    playerController.hit = true;
                }
            }
        }
    }

    IEnumerator HpEffect()
    {
        while(!Mathf.Approximately(DragonUI.GetComponent<UIController>().DecreaseHP.value, 
            DragonUI.GetComponent<UIController>().DecreaseHP2.value))
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lerpTime)
            {
                currentTime = lerpTime;
            }
            DragonUI.GetComponent<UIController>().DecreaseHP2.value =
                Mathf.Lerp(DragonUI.GetComponent<UIController>().DecreaseHP2.value,
                DragonUI.GetComponent<UIController>().DecreaseHP.value, currentTime/lerpTime);
            yield return null;
        }
        currentTime = 0.0f;
        once = false;
    }
}
