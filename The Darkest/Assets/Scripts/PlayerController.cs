using System.Security.Claims;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : AnimatorAll
{
    public GameObject UI_Aiming;

    //public Animator anim;
    public SpringArm arm; 
    Vector2 targetDir = Vector2.zero;
    public Transform Left;
    public Transform Right;
    public Transform Arch2;
    public Transform Arch2Oripos;
    public Transform RightThumb;
    public Transform Arrow;
    float run = 0.5f;
    [SerializeField]
    public bool invincibility = false;
    float invincibilityTime = 3.0f; // ���� �ð� ����
    bool running = false;
    public bool hit = false;
    
    public AnimationEvent animEvent;

    Vector3 checkDir = Vector3.zero;
    public Transform DragonTr;

    //player UI
    public RectTransform UIAll;
    public Slider Stamina;
    public Slider PlayerHP;
    bool SGaugeFill = false;
    uint a = 1;
    public TextMeshProUGUI LevelT;
    public TextMeshProUGUI LevelDecimal;
    public Image ExpGauge;

    public int Level = 0;
    public float Exp = 1;
    public float MaxExp = 0.01f;

    public PlayerStat PlayerStat;
    int dam;
    int hea;
    int sta;
    int cri;
    public RectTransform HpBar;
    public RectTransform StaminaBar;
    //player DeadScene UI

    public RectTransform DeadSceneAll;
    public Image DeadSceneBorder; // ���� ����������
    public Image DeadSceneBar; // ���� ����������
    float currentTime = 0;
    float lerpTime = 5;//�������� ���ϴ� �ӵ��� ����
    public TextMeshProUGUI DeadText; //���� Ŀ����
    Color AlphaColorBor;
    Color AlphaColorBar;
    public Animator DeadCamera;
    float ReloadingTime = 5.0f;
    float time = 0;

    public PlayerStat2 PlayerUI;

    //Particle
    public ParticleSystem HealOura;
    bool RestCheck = false;
    Color color;

    public Animator anim;
    public GameObject UIAim;

    public enum PlayerState
    {
        Play, Die, HitDown, Aim, Heal
    }
    int n = 0;
    void Start()
    {
        n = transform.childCount;
        color = Color.white;
        HealOura.Stop();
        Arch2Oripos.position = Arch2.position;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessState();
        InvincibleTime();
        PlayerExp();//�ӽ÷� ����� ���߿� ���� ��� ����� ����� �׶� Ȱ���� ����
        PlayerStatus();
        if (Stamina.value != Stamina.maxValue && SGaugeFill)
        {
            Stamina.value += 2.0f * Time.deltaTime;
        }

        if(n < transform.childCount)
        {
            if (transform.GetChild(7).GetComponent<Item>()) //�� �ڵ�� player�ȿ� �ڽ� ������ �þ�� �����������
            {
                transform.GetChild(7).parent = null;
            }
        }
    }

    public PlayerState MyState = PlayerState.Play;

    public void ChangeState(PlayerState s) // ���°� ���� �Ǹ� ���� �ѹ� ����Ǵ� ��
    {
        if (s == MyState) return;
        MyState = s;
        switch (MyState)
        { 
            case PlayerState.Play:
                break;
            case PlayerState.Die:
                UIAll.gameObject.SetActive(false);
                DeadSceneAll.gameObject.SetActive(true);
                DeadCamera.SetTrigger("DeadScene");
                myAnim.SetTrigger("IsDead");
                break;
            case PlayerState.HitDown:
                break;
            case PlayerState.Aim:
                break;
            case PlayerState.Heal:
                currentTime = 0;
                HealOura.gameObject.SetActive(true);
                HealOura.startColor = Color.white;
                break;
        }
    }

    public void ProcessState()
    {
        switch (MyState)
        {
            case PlayerState.Play:

                if (anim.GetBool("Aiming"))
                {
                    UIAim.SetActive(true);
                }
                else if (!anim.GetBool("Aiming"))
                {
                    UIAim.SetActive(false);
                }

                if (animEvent.animAming)
                {
                    //Ȱ���� ����
                    Arch2.position = RightThumb.position;
                }
                else if (!animEvent.animAming)
                {
                    //Ȱ ��� and ���
                    Arch2.position = Arch2Oripos.position;
                }

                targetDir.x = Input.GetAxisRaw("Horizontal");
                targetDir.y = Input.GetAxisRaw("Vertical");
                float x = Mathf.Lerp(myAnim.GetFloat("X"), targetDir.x, Time.deltaTime * 3.0f);
                float y = Mathf.Lerp(myAnim.GetFloat("Y"), targetDir.y, Time.deltaTime * 3.0f);
                if (Input.GetKey(KeyCode.LeftShift) && Stamina.value > 0.0f)
                {
                    if (!Mathf.Approximately(myAnim.GetFloat("X"), 0) || !Mathf.Approximately(myAnim.GetFloat("Y"), 0))
                    {
                        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
                        {//����Ű�� ��������
                            //���¹̳� �پ��� ��
                            SGaugeFill = false;
                            Stamina.value -= 10.0f * Time.deltaTime;
                        }
                        else if(MyState != PlayerState.Play)
                        {
                            SGaugeFill = true;
                        }
                        else
                        {
                            SGaugeFill = true;
                        }
                    }
                    
                    run = 1.0f;
                    running = true;
                    if (Stamina.value <= 0.1f)
                    {
                        running = false;
                    }
                }

                if(Stamina.value <= 0.1f)
                {
                    if (!running)
                    {
                        UnPossibleRun();
                    }
                }
                if (!running)
                {
                    UnPossibleRun();
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    running = false;
                    SGaugeFill = true;
                }

                if (Input.GetMouseButton(1) && animEvent.ReadyToAim)
                {
                    myAnim.SetBool("Aiming", true);
                    //UI_Aiming.GetComponentsInChildren<Image>().enabled = true;
                    if (Input.GetMouseButtonDown(0) && animEvent.ReadyToShoot)
                    {

                        myAnim.SetTrigger("Shooting");
                    }
                }
                if (Input.GetMouseButtonUp(1))
                {
                    UI_Aiming.GetComponentsInChildren<Image>();

                    animEvent.SAim = false;
                    myAnim.SetBool("Aiming", false);
                }

                x = Mathf.Clamp(x, -run, run);
                y = Mathf.Clamp(y, -run, run);

                myAnim.SetFloat("X", x);
                myAnim.SetFloat("Y", y);
                break;
            case PlayerState.Die:
                //border�� 255���� bar�� 180���� text�� 70���� 120����
                currentTime += Time.deltaTime;
                if (currentTime >= lerpTime)
                {
                    currentTime = lerpTime;
                }

                DeadCamera.enabled = true;

                AlphaColorBor.a = alphaVal(0.01f, 1, 3);
                DeadSceneBorder.color = AlphaColorBor;

                AlphaColorBar.a = alphaVal(0.01f, 0.8f, 3);
                DeadSceneBar.color = AlphaColorBar;
                DeadSceneBar.rectTransform.sizeDelta = new Vector2(0, alphaVal(100.0f, 200.0f, 1f));
                DeadText.fontSize = alphaVal(100.0f, 120.0f, 2);

                //Scene ����� �ϸ� �ɵ�
                time += Time.deltaTime;
                if (ReloadingTime <= time)
                {
                    LoadingSceneManager.LoadScene("Testing");
                    time = 0;
                }
                
                break;
            case PlayerState.HitDown:
                if(PlayerHP.value <= 0.0f)
                {
                    ChangeState(PlayerState.Die);
                }
                else
                {
                    animEvent.OnInactiveArrow();
                    animEvent.OnReleaseBow();
                    myAnim.SetBool("Aiming", false);
                    if (animEvent.PlayerDown)
                    {
                        ChangeState(PlayerState.Play);
                    }
                }
                break;
            case PlayerState.Heal:

                if(myAnim.GetBool("IsResting"))
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= lerpTime)
                    {
                        currentTime = lerpTime;
                    }
                    color = Color.white;
                    HealOura.startColor = color;
                    HealOura.Play();
                    RestCheck = true;
                    PlayerHP.value = alphaVal(PlayerHP.value, PlayerHP.maxValue, 100.0f);
                    Stamina.value = alphaVal(Stamina.value, Stamina.maxValue, 100.0f);
                }

                if (!myAnim.GetBool("IsResting") && RestCheck)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= lerpTime)
                    {
                        currentTime = lerpTime;
                    }
                    
                    color.a = alphaVal(1, 0.01f, 1.0f);
                    HealOura.startColor = color;
                    if(Mathf.Approximately(color.a, 0.01f) && animEvent.RestEnd)
                    {
                        Arrow.gameObject.SetActive(true);
                        HealOura.gameObject.SetActive(false);
                        DeadCamera.enabled = false;
                        animEvent.RestEnd = false;
                        RestCheck = false;
                        ChangeState(PlayerState.Play);
                    }
                }
                break;
        }
    }

    float alphaVal(float Start, float End, float lerp)
    {
        float val = Mathf.Lerp(Start, End, currentTime / lerp);
        return val;
    }
    void UnPossibleRun()
    {
        if((uint)run <= a)
        {
            run -= 1.0f * Time.deltaTime;
            if (run < 0.5f)
            {
                running = true;
                return;
            }
        }
    }
    void InvincibleTime()
    {
        if(invincibility)
        {
            if (invincibilityTime > 0.0f)
            {
                invincibilityTime -= Time.deltaTime;

            }
            else if (invincibilityTime <= 0.0f)
            {
                invincibility = false;
                invincibilityTime = 3.0f;
            }
        }
    }

    public void Attacked(float dam)
    {
        if (hit && !invincibility) //�������� �¾�����? ���
        {
            //�̰����� defence ����
            PlayerHP.value -= dam - PlayerStat.Defence;

            if(PlayerHP.value <= 0.0f)
            {
                ChangeState(PlayerState.Die);
                return;
            }

            Vector3 hori = Right.localPosition - Left.localPosition;
            checkDir = Vector3.Cross(Vector3.up, hori);
            checkDir.Normalize();
            Vector3 myPosition = (DragonTr.position - Left.position).normalized;
            if (Vector3.Dot(checkDir, myPosition) < 0.0f) //���� 
            {
                //�տ��ִ�.
                myAnim.SetTrigger("isGettingBackUp");
            }
            else if (Vector3.Dot(checkDir, myPosition) > 0.0f)
            {
                //�ڿ��ִ�.
                myAnim.SetTrigger("isGettingFrontUp");
            }

            ChangeState(PlayerState.HitDown);
            animEvent.PlayerDown = false;
            hit = false;
            invincibility = true;
        }
    }

    void PlayerExp()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �ӽ� �ڵ� ���� ���� ������� �ѹ� �ߵ��ϰ� ����� �ɵ�
        {
            Exp += 5000; // ���� ���� ���� ����ġ�� �ٸ���
        }
        ExpGauge.fillAmount = Exp * MaxExp;
        double LevelVal = ExpGauge.fillAmount * 100.0f;
        LevelVal = System.Math.Truncate(LevelVal * 100) / 100;
        LevelDecimal.text = LevelVal.ToString() + "%";

        if (ExpGauge.fillAmount == 1.0f) //������ ������
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/LevelUp") as GameObject);
            obj.name = "LevelUpParticle";
            obj.transform.SetParent(this.transform, false);
              
            float carryforward = Exp * MaxExp; //������ ����ġ�� �̿��� (���� ����)
            Level += 1;
            PlayerUI.GetPoint(4); // Statâ ����Ʈ ȹ��
            LevelT.text = Level.ToString();
            ExpGauge.fillAmount = 0.0f;
            Exp = 0.0f;
            Exp = carryforward;
            MaxExp *= 0.4f;
        }
    }

    void PlayerStatus()
    {
        if (PlayerStat.Health != hea)
        {
            hea = PlayerStat.Health;
            float val = 10 * (hea - 100);
            HpBar.offsetMax = new Vector2(val, 0);
            PlayerHP.maxValue = (val * 0.1f) + 100;
        }
        if (PlayerStat.Stamina != sta)
        {
            sta = PlayerStat.Stamina;
            float val = 10 * (sta - 100);
            StaminaBar.offsetMax = new Vector2(val, 25);
            Stamina.maxValue = (val * 0.1f) + 100;
        }
    }
}
