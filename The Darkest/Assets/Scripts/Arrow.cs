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
                //드래곤 피를 깎는 코드 damage관련 기능을 여기서 수행...
                //이곳에서 치명타도 관리
                Hit a = hit.transform.GetComponent<Hit>();
                int rnd = Random.Range(1, 101); //1에서 100까지의 수를 넣음
                int moreDam = 1;
                if(a.DragonHP.PlayerStat.Critical > rnd)
                {
                    moreDam = 2;
                    Debug.Log("크리티컬!");
                }
                a.DragonHP.HP -= a.DragonHP.PlayerStat.Damage * moreDam;//이곳에서 값에대한 처리를하면 되는데 값을 가져오는 방식이 생각보다 까다로움 
                Debug.Log("데미지" + a.DragonHP.PlayerStat.Damage);
            }

            brokenArrow.transform.SetParent(hit.transform);

            brokenArrow.transform.position = pos;
            Destroy(this.gameObject);
        }
    }
}
