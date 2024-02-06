using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ��򰡷� data�� �ְ� ���� ���ش�

// �����ϴ� ���
// 1. ������ �����Ͱ� �����ؾ���
// 2. �����͸� Json���� ��ȯ
// 3. Json�� �ܺο� ����

//�ҷ����� ���
// 1. �ܺο� ����� Json�� ������
// 2. Json�� ���������·� ��ȯ
// 3. �ҷ��� �����͸� ���

//���Ժ��� �ٸ��� ����

public class PlayerData
{
    //�̸�, ����, ������������ �ϴ�, �������� ���� ����
    //���� �����ؾ��� ���� ��ġ + �κ��丮�� ���â
    public string Name = "Player";
    public Transform PlayerPos;
    public float x;
    public float y;
    public float z;
    public int level;
    public float Exp;
    public float Maxexp;
    //public int item; //�ӽ÷� �ϴ� ������ �˰� ���ô�
}

public class UIData
{
    //UI�� ��������ִٴ� ������ �������� �����Ͱ����� �ϴ� stat point ���ܺ���
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
        #region �̱���
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

    public void SaveData() // �����ϱ�
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + filename, data);
        data = JsonUtility.ToJson(uIData);
        File.WriteAllText(UIpath + UIfileName, data);
    }

    public void LoadDate() // �ҷ�����
    {
        string data = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        data = File.ReadAllText(UIpath + UIfileName);
        uIData = JsonUtility.FromJson<UIData>(data);
    }
}
