using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : AnimatorAll
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
    [SerializeField]
    public float HP = 5000.0f;
    float DHP;
    public float AttackDelay = 0f;
    private string attackType = "StrongAttack";
    public Collider search;

    public Transform DragonTr;
    public Transform Player;
    public Transform LookingPlayer;
    public PlayerController PlayerController;

    public LayerMask enemyMask;
    public Transform AttackPoint;
    public Transform AttackPoint1;
    public Transform AttackPoint2;
    public Transform Head;

    public float AttackRange = 0.0f;
    public float AttackLength = 5.0f;

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

    public enum MonsterState
    {
        Idle, Walk, Fight, Dead, Scream, Sleep
    }
    
    public MonsterState State = MonsterState.Sleep;
  
    void Start()
    {
        DHP = HP;
        Agent = GetComponent<NavMeshAgent>();
        DragonTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

        if (HP <= 0.0f)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (HP != DragonUI.GetComponent<UIController>().DecreaseHP.value && !wakeUp)
        {
            ChangeState(MonsterState.Walk);
            wakeUp = true;
        }

        if (Vector3.Distance(DragonTr.position, Player.position) < AttackLength)
        {
            attackPossible = true; // 공격가능
        }
        else
        {
            attackPossible = false; // 공격 불가능
        }

        //테스트 어택 딜레이
        AttackDelay -= Time.deltaTime;
        if(DHP != HP)
        {
            DragonUI.GetComponent<UIController>().DecreaseHP.value = HP;
            DHP = HP;
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

    public void ChangeState(MonsterState s) //행동이 바뀔때 최초 한번 실행 하는곳
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case MonsterState.Sleep:
                break;
            case MonsterState.Scream:
                DragonUI.GetComponent<UIController>().dragonState = true; // UI생성
                break;
            case MonsterState.Walk:
                break;
            case MonsterState.Idle:
                break;
            case MonsterState.Fight:
                break;
            case MonsterState.Dead:
                PlayerController.Exp += 5000; //player에게 주는 경험치
                PlayerController.expC = true;
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
            case MonsterState.Scream:
                myAnim.SetTrigger("IsScream");
                ChangeState(MonsterState.Walk);
                break;
            case MonsterState.Walk:
                AttackLength = 5.0f;
                if (!scream) // 1회성 코드
                {
                    myAnim.SetBool("IsWalk", true);
                    scream = true;
                    ChangeState(MonsterState.Scream);
                }
                if (animEvent.Scream)
                {
                    if(PlayerController.MyState == PlayerController.PlayerState.Play) //플레이어가 살아있다면 쫓아가라
                    {
                        float dis = Vector3.Distance(DragonTr.position, Player.position);
                        if (dis < AttackLength) // player 공격사거리
                        {
                            animEvent.Fight = false;
                            ChangeState(MonsterState.Fight);
                        }
                        dir = Player.transform.position - DragonTr.position;
                        DragonTr.rotation = Quaternion.Lerp(DragonTr.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
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
                        Agent.SetDestination(Player.position);
                    }
                }
                break;
            case MonsterState.Idle:
                if(AttackDelay > 0)
                {
                    ChangeState(MonsterState.Idle);
                }
                else
                {
                    ChangeState(MonsterState.Fight);
                }
                //플레이어가 죽으면 여기로 들어온다.
                break;
            case MonsterState.Fight:
                if(PlayerController.MyState == PlayerController.PlayerState.Play) //player가 죽거나 넘어져있는 상태가 아닐때만 공격
                {
                    AttackLength = 6.0f;
                    //AnimationEvent 에서 Fight false 처리중 애니메이션이 끝나면 다시 들어와서 공격함

                    //문제점
                    //fight상태에서 못나올때가 있음 animEvent.Fight가 true처리 안될때가 있음
                    //공격범위 안에 있지만 걷는다. 이유를 파악함 플레이어가 hitdown으로 거리가 attackRange보다 조금 멀어짐 그래서
                    //걷는 상태로 들어가자마자 공격범위 안에 들어와서 공격을 하는 부자연스러운 현상이 생김
                    if (AttackDelay <= 0.0f)
                    {
                        Agent.isStopped = true;
                        Agent.velocity = Vector3.zero;
                        AttackDelay = 5.0f;
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

                    if (AttackDelay > 0.0f) // 공격 딜레이에 걸려있으면 쳐다봐라 player를
                    {
                        dir = Player.transform.position - DragonTr.position;
                        DragonTr.rotation = Quaternion.Lerp(DragonTr.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
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
                        ChangeState(MonsterState.Walk);
                        run = false;
                    }
                }
                break;
            case MonsterState.Dead:
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
