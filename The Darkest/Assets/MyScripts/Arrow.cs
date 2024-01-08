using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject CrashArrow;
    public LayerMask crashMask;

    [SerializeField]
    float Power = 1.0f;

    

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
                //이방식으로 Damage처리를 했지만 몹이 늘어나면 방식을 바꿔야함
                //드래곤 피를 깎는 코드 damage관련 기능을 여기서 수행...
                //이곳에서 치명타도 관리
                Hit a = hit.transform.GetComponent<Hit>();
                int rnd = Random.Range(1, 101); //1에서 100까지의 수를 넣음
                int moreDam = 1;
                Debug.Log(a.DragonHP.PlayerStat.Critical);
                if(a.DragonHP.PlayerStat.Critical > rnd)
                {
                    moreDam = 2;
                    Debug.Log("크리티컬!");
                }
                a.DragonHP.HP -= a.DragonHP.PlayerStat.Damage * moreDam;
                Debug.Log("데미지" + a.DragonHP.PlayerStat.Damage * moreDam);
            }

            brokenArrow.transform.SetParent(hit.transform);

            brokenArrow.transform.position = pos;
            Destroy(this.gameObject);
        }
    }
}
