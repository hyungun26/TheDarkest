using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSearch : Interact
{
    new public AudioSource audio;
    public AudioClip audioClip;
    public List<GameObject> list = new List<GameObject>();
    public Transform content;
    List<GameObject> slotList = new List<GameObject>();
    void Start()
    {
        audio.clip = audioClip;
        foreach(Transform i in content)
        {
            slotList.Add(i.gameObject);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            list.Add(other.gameObject);
        }
    }
    public override void OnTriggerExit(Collider other)
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
                audio.clip = audioClip;
                audio.Play();
                //일단 생성을 먼저해야합니다 instantiate 를 생성을 해야할것같네요  //완성~
                //다음문제 slot안에 item이 있다면 다음 slot에 생성
                Item item = list[0].gameObject.GetComponent<Item>();
                if (item != null)
                {
                    for (int i = 0; i < slotList.Count; i++)//아이템 들어온 순서대로 칸에 넣기
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
