using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public PoolManager pool;
    public float spawnTime;
    public int num = 0;
    private const int Maxnum = 3;
    //Update is called once per frame
    void LateUpdate()
    {
        if(pool == null)
        {
            pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        }
        //���ϴ� ���� �ƹ��͵� ���°��� 3������ instantiate���� �����ϰ�
        //�� �ķδ� ���Ͱ� ���������� pool�� ��Ȱ��ȭ Ȱ��ȭ�� �ݺ��ϰ� �����.
        if(Maxnum > num) //�ϴ� 3���� ������ �������ϰ�
        {
            spawnTime += 1.0f * Time.deltaTime;
            if (spawnTime > 5.0f)
            {
                spawnTime = 0.0f;
                float x = Random.Range(-10, 11);
                float z = Random.Range(-10, 11);
                float rotY = Random.Range(0, 366);
                pool.Get(0, x, z, rotY, this.transform);
                num++;
            }
        }
    }
}
