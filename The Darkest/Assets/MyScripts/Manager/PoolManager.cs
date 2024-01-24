using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩들을 보관할 변수가 필요함 프리펩이 2개면 리스트도 2개여야한다
    public GameObject[] prefabs;

    // .. 풀 담당을 하는 리스트들이 필요함    
    List<GameObject>[] pools;

    private void Awake()
    {
        //배열 초기화
        pools = new List<GameObject>[prefabs.Length];
        //배열 안에 요소들도 초기화
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, float x, float z, float rotY, Transform par)
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = par.position + Vector3.forward * z + Vector3.right * x;
                select.transform.rotation = par.localRotation * Quaternion.Euler(Vector3.up * rotY);
                select.SetActive(true);
                break;
            }
        }

        // ... 못 찾았으면?
        if (!select) // null값이면 false값이 나온다
        {
            // ... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], par.position + Vector3.forward * z + Vector3.right * x,
               par.localRotation * Quaternion.Euler(Vector3.up * rotY));
            select.transform.parent = par.transform;
            pools[index].Add(select);
        }

        return select;
    }
    public GameObject Get(int index) // 최상위에 생성삭제를 해야하면 이곳에서
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못 찾았으면?
        if (!select) // null값이면 false값이 나온다
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index]);
            pools[index].Add(select);
        }
        return select;
    }
    public GameObject Get(int index, Transform Pos) // 부모가 필요하면 이걸로
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못 찾았으면?
        if (!select) // null값이면 false값이 나온다
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], Pos);
            pools[index].Add(select);
        }
        return select;
    }
}
