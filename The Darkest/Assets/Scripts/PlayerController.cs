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
    float invincibilityTime = 3.0f; // 무적 시간 조절
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
    public Image DeadSceneBorder; // 점점 선명해질것
    public Image DeadSceneBar; // 점점 선명해질것
    float currentTime = 0;
    float lerpTime = 5;//값에따라 변하는 속도가 변함
    public TextMeshProUGUI DeadText; //점점 커질것
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
        PlayerExp();//임시로 만든것 나중에 몬스터 잡는 기능이 생기면 그때 활용할 것임
        PlayerStatus();
        if (Stamina.value != Stamina.maxValue && SGaugeFill)
        {
            Stamina.value += 2.0f * Time.deltaTime;
        }

        if(n < transform.childCount)
        {
            if (transform.GetChild(7).GetComponent<Item>()) //이 코드는 player안에 자식 갯수가 늘어나면 조정해줘야함
            {
                transform.GetChild(7).parent = null;
            }
        }
    }

    public PlayerState MyState = PlayerState.Play;

    public void ChangeState(PlayerState s) // 상태가 변경 되면 최초 한번 실행되는 곳
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
                    //활시위 당기기
                    Arch2.position = RightThumb.position;
                }
                else if (!animEvent.animAming)
                {
                    //활 쏘기 and 취소
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
                        {//방향키를 눌렀을때
                            //스태미나 줄어드는 곳
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
                //border는 255까지 bar는 180까지 text는 70에서 120까지
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

                //Scene 재시작 하면 될듯
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
        if (hit && !invincibility) //몬스터한테 맞았을때? 등등
        {
            //이곳에서 defence 관리
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
            if (Vector3.Dot(checkDir, myPosition) < 0.0f) //내적 
            {
                //앞에있다.
                myAnim.SetTrigger("isGettingBackUp");
            }
            else if (Vector3.Dot(checkDir, myPosition) > 0.0f)
            {
                //뒤에있다.
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
        if (Input.GetKeyDown(KeyCode.Space)) // 임시 코드 대충 몹을 잡았을때 한번 발동하게 만들면 될듯
        {
            Exp += 5000; // 잡은 몹에 따라 경험치량 다르게
        }
        ExpGauge.fillAmount = Exp * MaxExp;
        double LevelVal = ExpGauge.fillAmount * 100.0f;
        LevelVal = System.Math.Truncate(LevelVal * 100) / 100;
        LevelDecimal.text = LevelVal.ToString() + "%";

        if (ExpGauge.fillAmount == 1.0f) //레벨업 했을때
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/LevelUp") as GameObject);
            obj.name = "LevelUpParticle";
            obj.transform.SetParent(this.transform, false);
              
            float carryforward = Exp * MaxExp; //나머지 경험치량 이월값 (문제 있음)
            Level += 1;
            PlayerUI.GetPoint(4); // Stat창 포인트 획득
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
