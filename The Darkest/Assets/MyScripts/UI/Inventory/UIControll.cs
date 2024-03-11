using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControll : DataManager
{
    //static의 역할은 대충 다른 스크립트에 영향을 받은 코드에 값을 static을 선언한 class에서도 영향을 받는 것
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
            #region 세이브 기능
            /*else //나가기 버튼 //임시 esc 저장기능
            //{
            //    nowPlayer.Name = "Archer";
            //    //nowPlayer.PlayerPos.position = playerController.transform.position;
            //    nowPlayer.x = playerController.transform.position.x;
            //    nowPlayer.y = playerController.transform.position.y;
            //    nowPlayer.z = playerController.transform.position.z;
            //    nowPlayer.level = playerController.Level;
            //    nowPlayer.Exp = playerController.Exp;
            //    nowPlayer.Maxexp = playerController.MaxExp;
            //    uIData.point = int.Parse(stat2.Point.text.ToString());
            //    uIData.Damage = stat1.Damage;
            //    uIData.DamageP = plusButton[0].num;
            //    uIData.Health = stat1.Health;
            //    uIData.HealthP = plusButton[1].num;
            //    uIData.Stamina = stat1.Stamina;
            //    uIData.StaminaP = plusButton[2].num;
            //    uIData.Defence = stat1.Defence;
            //    uIData.DefenceP = plusButton[3].num;
            //    uIData.Critical = stat1.Critical;
            //    uIData.CriticalP = plusButton[4].num;
            //    for (int i = 0; i < playerController.slot.Length; i++)
            //    {
            //        ItemIcon ItemInformaion = playerController.slot[i].GetComponentInChildren<ItemIcon>();
            //        if (ItemInformaion != null)
            //        {
            //            uIData.item.Add(playerController.slot[i].name, ItemInformaion);
            //        }
            //    }
            //    foreach(var i in uIData.item)
            //    {
            //        Debug.Log(i);
            //        if(i.Key == "Slot")
            //        {
            //            Instantiate(i.Value, this.transform, false);
            //        }
            //    }
            //    Debug.Log("저장");
            //    SaveData();
            }*/
            #endregion
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
