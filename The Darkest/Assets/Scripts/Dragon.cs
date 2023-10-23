using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dragon : AnimatorAll
{
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
            attackPossible = true; // ���ݰ���
        }
        else
        {
            attackPossible = false; // ���� �Ұ���
        }

        //�׽�Ʈ ���� ������
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

    public void ChangeState(MonsterState s) //�ൿ�� �ٲ� ���� �ѹ� ���� �ϴ°�
    {
        if (State == s) return;
        State = s;
        switch (State)
        {
            case MonsterState.Sleep:
                break;
            case MonsterState.Scream:
                DragonUI.GetComponent<UIController>().dragonState = true; // UI����
                break;
            case MonsterState.Walk:
                break;
            case MonsterState.Idle:
                break;
            case MonsterState.Fight:
                break;
            case MonsterState.Dead:
                PlayerController.Exp += 5000; //player���� �ִ� ����ġ
                PlayerController.expC = true;
                DragonUI.GetComponent<UIController>().dragonDead = true; // ������
                DragonUI.GetComponent<UIController>().dragonState = true; // UI����
                myAnim.SetTrigger("IsDead");
                break;
        }
    }

    public void StateProcess() //�ൿ�� ��� �ؾ��ϴ� ��
    {
        switch (State)
        {
            case MonsterState.Scream:
                myAnim.SetTrigger("IsScream");
                ChangeState(MonsterState.Walk);
                break;
            case MonsterState.Walk:
                AttackLength = 5.0f;
                if (!scream) // 1ȸ�� �ڵ�
                {
                    myAnim.SetBool("IsWalk", true);
                    scream = true;
                    ChangeState(MonsterState.Scream);
                }
                if (animEvent.Scream)
                {
                    if(PlayerController.MyState == PlayerController.PlayerState.Play) //�÷��̾ ����ִٸ� �Ѿư���
                    {
                        if (Vector3.Distance(DragonTr.position, Player.position) < AttackLength) // player ���ݻ�Ÿ�
                        {
                            animEvent.Fight = false;
                            ChangeState(MonsterState.Fight);
                        }
                        dir = Player.transform.position - DragonTr.position;
                        DragonTr.rotation = Quaternion.Lerp(DragonTr.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
                        if (Vector3.Distance(DragonTr.position, Player.position) > runORwalk && !run && !attackPossible) //�Ÿ��� �ָ� �پ��
                        {
                            Agent.isStopped = false;
                            run = true;
                            Agent.speed = 60.0f;
                            myAnim.SetTrigger("IsChaingRun");
                        }
                        else if(Vector3.Distance(DragonTr.position, Player.position) < runORwalk && !run && !attackPossible) //�Ÿ��� ������ �ɾ��
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
                //�÷��̾ ������ ����� ���´�.
                break;
            case MonsterState.Fight:
                if(PlayerController.MyState == PlayerController.PlayerState.Play) //player�� �װų� �Ѿ����ִ� �����϶��� ����
                {
                    AttackLength = 6.0f;
                    //AnimationEvent ���� Fight false ó���� �ִϸ��̼��� ������ �ٽ� ���ͼ� ������

                    //������
                    //fight���¿��� �����ö��� ���� animEvent.Fight�� trueó�� �ȵɶ��� ����
                    //���ݹ��� �ȿ� ������ �ȴ´�. ������ �ľ��� �÷��̾ hitdown���� �Ÿ��� attackRange���� ���� �־��� �׷���
                    //�ȴ� ���·� ���ڸ��� ���ݹ��� �ȿ� ���ͼ� ������ �ϴ� ���ڿ������� ������ ����
                    if (AttackDelay <= 0.0f)
                    {
                        Agent.isStopped = true;
                        Agent.velocity = Vector3.zero;
                        AttackDelay = 5.0f;
                        animEvent.Fight = false;
                        rnd = Random.Range(0, 2); // 0 ~ 1
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
                            default:
                                return;
                        }

                    }

                    if (AttackDelay > 0.0f) // ���� �����̿� �ɷ������� �Ĵٺ��� player��
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

                    if (!attackPossible && animEvent.Fight) // ���� ���� �ƴϸ� ������������
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
                    coll.GetComponent<PlayerController>().hit = true;
                    coll.GetComponent<PlayerController>().Attacked(100f);

                    coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
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
                    coll.GetComponent<PlayerController>().hit = true;
                    coll.GetComponent<PlayerController>().Attacked(70f);
                    coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
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
                    coll.GetComponent<PlayerController>().hit = true;
                    coll.GetComponent<PlayerController>().Attacked(70f);
                    coll.GetComponent<PlayerController>().ChangeState(PlayerController.MyState = PlayerController.PlayerState.HitDown);
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
