using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 어딘가로 data를 넣고 빼고를 해준다

// 저장하는 방법
// 1. 저장할 데이터가 존재해야함
// 2. 데이터를 Json으로 변환
// 3. Json을 외부에 저장

//불러오는 방법
// 1. 외부에 저장된 Json을 가져옴
// 2. Json을 데이터형태로 변환
// 3. 불러온 데이터를 사용

//슬롯별로 다르게 저장

public class PlayerData
{
    //이름, 레벨, 코인은없지만 일단, 착용중인 무기 좋다
    //내가 저장해야할 변수 위치 + 인벤토리와 장비창
    public string Name = "Player";
    public Transform PlayerPos;
    public float x;
    public float y;
    public float z;
    public int level;
    public float Exp;
    public float Maxexp;
    //public int item; //임시로 일단 개념을 알고 갑시다
}

public class UIData
{
    //UI에 뭐가들어있다는 구현이 생각보다 빡셀것같으니 일단 stat point 남겨보기
    public int point;
    public int Damage;
    public int DamageP;
    public int Health;
    public int HealthP;
    public int Stamina;
    public int StaminaP;
    public int Defence;
    public int DefenceP;
    public int Critical;
    public int CriticalP;
    public Dictionary<string, ItemIcon> item = new Dictionary<string, ItemIcon>();
    public Queue<ItemIcon> itemIcons = new Queue<ItemIcon>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();
    public UIData uIData = new UIData();

    string UIpath;
    string UIfileName = "UIInformation";
    string path;
    string filename = "PlayerData";

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        UIpath = Application.persistentDataPath + "/";
        path = Application.persistentDataPath + "/"; //C:\Users\user\AppData\LocalLow\DefaultCompany\The Darkest
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData() // 저장하기
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + filename, data);
        data = JsonUtility.ToJson(uIData);
        File.WriteAllText(UIpath + UIfileName, data);
    }

    public void LoadDate() // 불러오기
    {
        string data = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        data = File.ReadAllText(UIpath + UIfileName);
        uIData = JsonUtility.FromJson<UIData>(data);
    }
}
