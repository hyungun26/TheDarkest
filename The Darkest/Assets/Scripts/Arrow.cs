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
            //�տ��ִ� ��ü �˻�
            GameObject brokenArrow = Instantiate(CrashArrow);
            Vector3 pos = transform.position;

            if(hit.transform.CompareTag("Monster"))
            {
                //�̹������ Damageó���� ������ ���� �þ�� ����� �ٲ����
                //�巡�� �Ǹ� ��� �ڵ� damage���� ����� ���⼭ ����...
                //�̰����� ġ��Ÿ�� ����
                Hit a = hit.transform.GetComponent<Hit>();
                int rnd = Random.Range(1, 101); //1���� 100������ ���� ����
                int moreDam = 1;
                Debug.Log(a.DragonHP.PlayerStat.Critical);
                if(a.DragonHP.PlayerStat.Critical > rnd)
                {
                    moreDam = 2;
                    Debug.Log("ũ��Ƽ��!");
                }
                a.DragonHP.HP -= a.DragonHP.PlayerStat.Damage * moreDam;
                Debug.Log("������" + a.DragonHP.PlayerStat.Damage * moreDam);
            }

            brokenArrow.transform.SetParent(hit.transform);

            brokenArrow.transform.position = pos;
            Destroy(this.gameObject);
        }
    }
}
