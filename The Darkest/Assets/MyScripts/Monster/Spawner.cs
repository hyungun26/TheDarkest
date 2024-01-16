using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public PoolManager pool;
    public Camera camera;
    public GameObject spawn;
    public float spawnTime;
    public int num = 0;
    private int Maxnum = 3;
    //Update is called once per frame
    void LateUpdate()
    {
        //원하는 동작 아무것도 없는곳에 3마리를 instantiate으로 생성하고
        //그 후로는 몬스터가 죽을때마다 pool로 비활성화 활성화를 반복하게 만든다.
        if(Maxnum > num) //일단 3마리 까지만 생성을하고
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
