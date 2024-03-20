using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControll : DataManager
{
    new public AudioSource audio;
    public AudioClip[] audioClip;
    public List<GameObject> list = new List<GameObject>();
    public SpringArm Controll;
    protected static bool inven = false;
    protected static bool stat = false;
    public PlayerController playerController;
    public PlayerStat stat1;
    public PlayerStat2 stat2;
    public UpButton2[] plusButton;
    public SettingUI setting;


    //stat창
    public RectTransform StatWin;

    //inventory창
    public RectTransform Inventory;

    private void Awake()
    {
        stat1.once = true; //저장된 값을 넣고 초기화하기 위한 코드
    }

    private void Start()
    {
        setting = GameObject.Find("SettingUI").GetComponent<SettingUI>();
        StatWin.gameObject.SetActive(false);
        Inventory.gameObject.SetActive(false);
    }

    void Update()
    {
        if(list.Count != 0)
            Controll.enabled = false;
        else
            Controll.enabled = true;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (list.Count != 0)
            {
                //CloseUI Sound
                UISound(1);
                inven = false;
                stat = false;

                list[list.Count-1].SetActive(false);
                if(list[list.Count-1] == setting.transform.GetChild(0).gameObject)
                {
                    playerController.enabled = true;
                }
                list.Remove(list[list.Count-1]);
            }
            else
            {
                UISound(0);
                SettingControll();
            }
        }

        if(!setting.transform.GetChild(0).gameObject.activeSelf && SceneManager.GetActiveScene().name != "Intro")
        {
            if (Input.GetKeyDown(KeyCode.U)) //중복코드 간단하게 처리 할 수 있으면 좋겠다.
            {
                stat = !stat;
                StatWin.gameObject.SetActive(stat);
                if (stat)
                {
                    UISound(0);
                    list.Add(StatWin.gameObject);
                    StatWin.transform.SetAsLastSibling();
                }
                else
                {
                    UISound(1);
                    list.Remove(StatWin.gameObject);
                }
                    
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                inven = !inven;
                Inventory.gameObject.SetActive(inven);
                if (inven)
                {
                    UISound(0);
                    list.Add(Inventory.gameObject);
                    Inventory.transform.SetAsLastSibling();
                }
                else
                {
                    UISound(1);
                    list.Remove(Inventory.gameObject);
                }
            }
        }
    }

    public void SettingControll()
    {
        setting.transform.GetChild(0).gameObject.SetActive(true);
        list.Add(setting.transform.GetChild(0).gameObject);
        //player 애니메이션 다시시작
        if (SceneManager.GetActiveScene().name != "Intro")
        {
            playerController.anim.Rebind();
            playerController.anim.enabled = false;
            playerController.anim.enabled = true;
            //player조종 가능한 모든건 차단
            playerController.enabled = false;
        }
    }
    public void UISound(int num)
    {
        audio.clip = audioClip[num];
        audio.Play();
    }
}
