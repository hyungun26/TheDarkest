using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    //static의 역할은 대충 다른 스크립트에 영향을 받은 코드에 값을 static을 선언한 class에서도 영향을 받는 것
    public List<GameObject> list = new List<GameObject>();

    protected static bool inven = false;
    protected static bool stat = false;

    //stat창
    public RectTransform StatWin;

    //inventory창
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

        if (Input.GetKeyDown(KeyCode.U)) //중복코드 간단하게 처리 할 수 있으면 좋겠다.
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
