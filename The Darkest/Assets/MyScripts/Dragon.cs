using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonsterState
{
    //드래곤에 문제점 움직임이 이상함 고쳐야함
    public GameObject DragonUI;
    public NavMeshAgent Agent;
    public AnimationEvent animEvent;

    [SerializeField]
    bool attackPossible = false;
    float runORwalk = 20.0f;
    
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
    
    public MonsterStates State = MonsterStates.Sleep;
  
    void Start()
    {
        attackDelay = 0;
        Agent.speed = moveSpeed;
        DragonUI = GameObject.Find("Dragon");
        Hp = 5000.0f;
        Agent = GetComponent<NavMeshAgent>();
        AttackLength = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        if (attackDelay <= 5.0f)
        {
            StartCoroutine(Attack());
            attackDelay += 1.0f * Time.deltaTime;
        }
        else if (!attackPossible)
        {
            attackPossible = true;
        }
            
    }

    public void ChangeState(MonsterStates s) //행동이 바뀔때 최초 한번 실행 하는곳
    {
        Debug.Log(s);
        if (State == s) return;
        State = s;
        switch (State)
        {
            case MonsterStates.Sleep:
                break;
            case MonsterStates.Scream:
                myAnim.SetTrigger("IsScream");
                playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                PlayerTransform = playerController.gameObject.transform;
                DragonUI.GetComponent<UIController>().dragonState = true; // UI생성
                ChangeState(MonsterStates.Chase);
                break;
            case MonsterStates.Chase:
                Agent.updateRotation = true;
                Agent.isStopped = false;
                break;
            case MonsterStates.Idle:
                break;
            case MonsterStates.Fight:
                Agent.updateRotation = false;
                Agent.isStopped = true;
                Agent.velocity = Vector3.zero;
                myAnim.SetBool("IsChasingWalk", false);
                myAnim.SetBool("IsChasingRun", false);
                break;
            case MonsterStates.Dead:
                Agent.isStopped = true;
                Agent.velocity = Vector3.zero;
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
                break;
            case MonsterStates.Chase:
                if (animEvent.Scream)
                {
                    if (playerController.MyState == PlayerController.PlayerState.Play) //플레이어가 살아있다면 쫓아가라
                    {
                        Agent.SetDestination(PlayerTransform.position); // player쫒기
                        //드래곤과 player의 거리계산
                        float dis = Vector3.Distance(PlayerTransform.position, this.transform.position);
                        if (dis < AttackLength) // player 공격사거리
                        {
                            animEvent.Fight = false;
                            ChangeState(MonsterStates.Fight);
                            break;
                        }

                        if (dis > runORwalk) //거리가 멀면 뛰어가라
                        {   
                            Agent.speed = 8.0f;
                            myAnim.SetBool("IsChasingWalk", false);
                            myAnim.SetBool("IsChasingRun", true);
                        }
                        else if(dis < runORwalk && !myAnim.GetBool("IsChasingRun"))//거리가 가까우면 걸어가라
                        {
                            Agent.speed = 3.0f;
                            myAnim.SetBool("IsChasingWalk", true);
                        }
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
                //현재 문제 공격중일때 거리가 attackLength보다 멀어지면 Chase상태로 변경되어 버림 
                //해결 방법 Fight 공격모션이 끝나면 chase로 보내기
                //한가지더 문제 Fight에서 나가지 않고 사정거리안에 계속있으면 멍청하게 앞만보고 공격을 냅다 갈김 뒤잡으면 끝남
                //이 문제를 해결해야함 공격모션이 끝나면 한번 rotation을 해야할듯
                if (Vector3.Distance(this.transform.position, PlayerTransform.position) < AttackLength)
                {
                    //방향벡터 계산후 player방향보기
                    if (attackDelay >= 5.0f)
                    {
                        //공격을해야함
                        if (playerController.MyState == PlayerController.PlayerState.Play) //player가 죽거나 넘어져있는 상태가 아닐때만 공격
                        {
                            Agent.isStopped = true;
                            Agent.velocity = Vector3.zero;
                            animEvent.Fight = false;
                            attackDelay = 0;
                            int rnd = Random.Range(0, 2); // 0 ~ 2
                            switch (rnd)
                            {
                                case 0:
                                    AttackRange = 2.5f;
                                    myAnim.SetTrigger("IsBasicAttack");
                                    break;
                                case 1:
                                    AttackRange = 3f;
                                    myAnim.SetTrigger("IsClawAttack");
                                    break;
                                case 2:
                                    myAnim.SetTrigger("IsBreath");
                                    attackDelay = -3.0f;
                                    break;
                                default:
                                    return;
                            }
                            attackPossible = false;
                        }
                    }    
                    else if(attackDelay > 2.0f)
                    {
                        Debug.Log("asdf");
                        StartCoroutine(LookPlayer());
                    }
                }
                if(attackPossible)
                {
                    ChangeState(MonsterStates.Chase);
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
            yield return new WaitForSeconds(currentTime / lerpTime);
        }
        currentTime = 0.0f;
    }

    IEnumerator LookPlayer()
    {   
        Vector3 dir = PlayerTransform.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(dir), this.transform.rotation, 1.0f);
        yield return null;
    }
    public override void monsterHit(float dam)
    {
        if(!once) //딱 한번만 실행
        {
            ChangeState(MonsterStates.Scream);
            //여기서
            once = true;
        }
        Hp -= dam;
        DragonUI.GetComponent<UIController>().DecreaseHP.value = Hp;
        StartCoroutine(HpEffect());
        if (Hp <= 0.0f)
        {
            ChangeState(MonsterStates.Dead);
        }
    }
}
