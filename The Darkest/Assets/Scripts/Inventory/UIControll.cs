using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    //static�� ������ ���� �ٸ� ��ũ��Ʈ�� ������ ���� �ڵ忡 ���� static�� ������ class������ ������ �޴� ��
    public List<GameObject> list = new List<GameObject>();

    protected static bool inven = false;
    protected static bool stat = false;

    //statâ
    public RectTransform StatWin;

    //inventoryâ
    public RectTransform Inventory;

    private void Start()
    {
        StatWin.gameObject.SetActive(false);
        Inventory.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (list.Count != 0)
            {
                inven = false;
                stat = false;

                list[list.Count-1].SetActive(false);
                list.Remove(list[list.Count-1]);
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) //�ߺ��ڵ� �����ϰ� ó�� �� �� ������ ���ڴ�.
        {
            stat = !stat;
            StatWin.gameObject.SetActive(stat);
            if (stat)
            {
                list.Add(StatWin.gameObject);
                StatWin.transform.SetAsLastSibling();
            }
            else
                list.Remove(StatWin.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inven = !inven;
            Inventory.gameObject.SetActive(inven);
            if (inven)
            {
                list.Add(Inventory.gameObject);
                Inventory.transform.SetAsLastSibling();
            }
            else
                list.Remove(Inventory.gameObject);
        }
    }
}
