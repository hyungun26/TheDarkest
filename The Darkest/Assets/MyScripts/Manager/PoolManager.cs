using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. ��������� ������ ������ �ʿ��� �������� 2���� ����Ʈ�� 2�������Ѵ�
    public GameObject[] prefabs;

    // .. Ǯ ����� �ϴ� ����Ʈ���� �ʿ���    
    List<GameObject>[] pools;

    private void Awake()
    {
        //�迭 �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];
        //�迭 �ȿ� ��ҵ鵵 �ʱ�ȭ
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, float x, float z, float rotY, Transform par)
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ����
        foreach(GameObject item in pools[index])
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = par.position + Vector3.forward * z + Vector3.right * x;
                select.transform.rotation = par.localRotation * Quaternion.Euler(Vector3.up * rotY);
                select.SetActive(true);
                break;
            }
        }

        // ... �� ã������?
        if (!select) // null���̸� false���� ���´�
        {
            // ... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], par.position + Vector3.forward * z + Vector3.right * x,
               par.localRotation * Quaternion.Euler(Vector3.up * rotY));
            select.transform.parent = par.transform;
            pools[index].Add(select);
        }

        return select;
    }
    public GameObject Get(int index) // �ֻ����� ���������� �ؾ��ϸ� �̰�����
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ����
        foreach (GameObject item in pools[index])
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //�� ã������?
        if (!select) // null���̸� false���� ���´�
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index]);
            pools[index].Add(select);
        }
        return select;
    }
    public GameObject Get(int index, Transform Pos) // �θ� �ʿ��ϸ� �̰ɷ�
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ����
        foreach (GameObject item in pools[index])
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //�� ã������?
        if (!select) // null���̸� false���� ���´�
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], Pos);
            pools[index].Add(select);
        }
        return select;
    }
}
