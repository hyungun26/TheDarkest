using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSearch : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public TextMeshProUGUI InteractT;
    public Transform content;
    List<GameObject> slotList = new List<GameObject>();
    void Start()
    {
        foreach(Transform i in content)
        {
            slotList.Add(i.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            list.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            list.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (list.Count != 0)
        {
            InteractT.gameObject.SetActive(true);
            InteractT.text = "Press \"Z\"";
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //�ϴ� ������ �����ؾ��մϴ� instantiate �� ������ �ؾ��ҰͰ��׿�  //�ϼ�~
                //�������� slot�ȿ� item�� �ִٸ� ���� slot�� ����
                Item item = list[0].gameObject.GetComponent<Item>();
                if (item != null)
                {
                    for (int i = 0; i < slotList.Count; i++)//������ ���� ������� ĭ�� �ֱ�
                    {
                        if (slotList[i].transform.childCount == 0)
                        {
                            Instantiate(item.Items, slotList[i].transform);
                            item.Items.transform.localPosition = Vector2.zero;
                            Destroy(list[0].gameObject);
                            list.RemoveAt(0);
                            break;
                        }
                    }
                }
            }
        }
        else
            InteractT.gameObject.SetActive(false);
    }
}
