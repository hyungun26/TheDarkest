using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject CrashArrow;
    public LayerMask crashMask;
    public PlayerStat playerStat;
    [SerializeField]
    float Power = 1.0f;

    void Start()
    {
        playerStat = GameObject.Find("StatValue").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Power * Time.deltaTime;

        this.transform.position += transform.forward * delta;
        
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, delta, crashMask))
        {
            //앞에있는 물체 검사
            GameObject brokenArrow = Instantiate(CrashArrow);
            Vector3 pos = transform.position;

            if(hit.transform.CompareTag("Monster"))
            {
                //이방식으로 Damage처리를 했지만 몹이 늘어나면 방식을 바꿔야함 - 어느정도 바꿈 collider가 있는 곳에 hit script를 넣어 최상위 스크립트에 접근하여 hp를 깎는 방식 최상위 접근방식이 좋은 방법인지는 모르겠으나 짱편리!
                
                //이곳에서 치명타도 관리
                Hit a = hit.transform.GetComponent<Hit>();
                GameObject rootObj = a.transform.root.gameObject;
                GameObject monsterPar = a.transform.gameObject;

                Dragon dragon = rootObj.GetComponent<Dragon>();
                Monster mon = monsterPar.GetComponent<Monster>();
                Debug.Log(mon);
                int rnd = Random.Range(1, 101); //1에서 100까지의 랜덤 수를 넣음
                int moreDam = 1;

                if(playerStat.Critical > rnd)
                {
                    moreDam = 2;
                    Debug.Log("크리티컬!");
                }
                //a.sendDam += playerStat.Damage * moreDam;
                Debug.Log("데미지" + playerStat.Damage * moreDam);
                if(mon != null)
                {
                    mon.monsterHit(playerStat.Damage * moreDam);
                }
                if(dragon != null)
                {
                    dragon.HP -= playerStat.Damage * moreDam;
                }
            }

            brokenArrow.transform.SetParent(hit.transform);

            brokenArrow.transform.position = pos;
            Destroy(this.gameObject);
        }
    }
}
