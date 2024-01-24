using System.Security.Claims;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : AnimatorAll
{
    public PoolManager pool;
    public DataManager dataManager;
    public UpButton2[] button;

    public GameObject UI_Aiming;

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
    float invincibilityTime = 3.0f; // 무적시간
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

    public PlayerStat PlayerStat;
    int dam;
    int hea;
    int sta;
    int cri;
    public RectTransform HpBar;
    public RectTransform StaminaBar;
    //player DeadScene UI

    public RectTransform DeadSceneAll;
    public Image DeadSceneBorder;
    public Image DeadSceneBar;
    float currentTime = 0;
    float lerpTime = 5;
    public TextMeshProUGUI DeadText;
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

    //exp 관련
    public TextMeshProUGUI LevelT;
    public TextMeshProUGUI LevelDecimal;
    public Image ExpGauge;
    public int Level = 0;
    public float Exp = 0;
    public float MaxExp = 1000;

    //scene 관련
    
    public enum PlayerState
    {
        Play, Die, HitDown, Aim, Heal
    }
    int n = 0;

    private void Awake()
    {
        dataManager.LoadDate();//임시로 처음 using UnityEngine.SceneManagement;시작하면 load되게끔
    }
    void Start()
    {
        //저장한 데이터 가져오기
        //Level = dataManager.nowPlayer.level;
        //LevelT.text = Level.ToString(); //text도 초기화
        //MaxExp = dataManager.nowPlayer.Maxexp;
        //Exp = dataManager.nowPlayer.Exp;
        //this.transform.position = new Vector3(dataManager.nowPlayer.x, dataManager.nowPlayer.y, dataManager.nowPlayer.z);
        //PlayerUI.Point.text = dataManager.uIData.point.ToString();
        //button[0].num = dataManager.uIData.DamageP;
        //button[1].num = dataManager.uIData.HealthP;
        //button[2].num = dataManager.uIData.StaminaP;
        //button[3].num = dataManager.uIData.DefenceP;
        //button[4].num = dataManager.uIData.CriticalP;

        n = transform.childCount;
        color = Color.white;
        HealOura.Stop();
        Arch2Oripos.position = Arch2.position;
        this.transform.position = Vector3.up;
    }
    

    // Update is called once per frame
    void Update()
    {
        ProcessState();
        InvincibleTime();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PlayerExp(5000.0f);
            ChangeState(PlayerState.Die);
        }

        PlayerStatus();

        if (Stamina.value != Stamina.maxValue && SGaugeFill)
        {
            Stamina.value += 2.0f * Time.deltaTime;
        }

        if(n < transform.childCount) //아이템을 먹을때 잠시 player안으로 들어오는데 바로 없애기위함
        {
            if (transform.GetChild(7).GetComponent<Item>())
            {
                transform.GetChild(7).parent = null;
            }
        }
    }

    public PlayerState MyState = PlayerState.Play;

    public void ChangeState(PlayerState s)// 이곳은 상태가 바꾸면 한번 실행이 되는곳
    {
        if (s == MyState) return;
        MyState = s;
        switch (MyState)
        { 
            case PlayerState.Play:
                break;
            case PlayerState.Die:
                time = 0;
                currentTime = 0;
                DeadText.fontSize = 100.0f;
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
                    Arch2.position = RightThumb.position;
                }
                else if (!animEvent.animAming)
                {
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
                        {
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

    public void Attacked(float dam, string s)
    {
        if (!invincibility) 
        {
            float d = dam - PlayerStat.Defence;
            if(d < 0f)
            {
                d = 1.0f;
            }
            PlayerHP.value -= d;

            if(PlayerHP.value <= 0.0f)
            {
                ChangeState(PlayerState.Die);
                return;
            }

            //이 코드 때문에 무조건 넘어짐 그럼 방법이 공격에 종류를 만들어 분류를 해야함
            //Mob한태 맞을때는 약공격으로 경직 모션을 넣고 
            //Boss한태 맞을때는 강공격으로 아래에 넘어짐 모션을 사용해야할듯
            //
            switch(s)
            {
                case "WeekAttack": myAnim.SetTrigger("IsHit");
                break;
                case "StrongAttack": 
                Vector3 hori = Right.localPosition - Left.localPosition;
                checkDir = Vector3.Cross(Vector3.up, hori);
                checkDir.Normalize();
                Vector3 myPosition = (DragonTr.position - Left.position).normalized;
                if (Vector3.Dot(checkDir, myPosition) < 0.0f)
                {
                    myAnim.SetTrigger("isGettingFrontUp");
                }
                else if (Vector3.Dot(checkDir, myPosition) > 0.0f)
                {
                    myAnim.SetTrigger("isGettingBackUp");
                }
                break;
            }
            //ChangeState(PlayerState.HitDown);
            //animEvent.PlayerDown = false;
            //hit = false;
            invincibility = true;
        }
    }

    public void PlayerExp(float num)
    {
        Exp += num;
        ExpGauge.fillAmount = Exp / MaxExp;
        if (ExpGauge.fillAmount == 1.0f) 
        {
            pool.Get(2, this.transform);
            PlayerUI.GetPoint(4); // Stat창 에 4포인트준다
            float carriedOver = Exp - MaxExp;
            Level += 1;
            LevelT.text = Level.ToString();
            MaxExp += 1000;
            Exp = carriedOver;
            ExpGauge.fillAmount = Exp / MaxExp;
        }
        double LevelVal = ExpGauge.fillAmount * 100.0f;
        LevelVal = System.Math.Truncate(LevelVal * 100) / 100;
        LevelDecimal.text = LevelVal.ToString() + "%";
    }
    #region 혹시몰라 남겨둔 player hit 코드
    // public void PlayerHitCode(float damage)
    // {
    //     hit = true;
    //     //Attacked(damage);
    //     ChangeState(MyState = PlayerController.PlayerState.HitDown);
    // }
    #endregion 
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
