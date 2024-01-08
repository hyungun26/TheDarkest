using UnityEngine;

public class PlayerStat : MonoBehaviour
{    
    public UpButton2[] buttonUp;
    public Stat[] StatClient;
    public EquipmentSlot Weapon;
    public EquipmentSlot Head;
    public EquipmentSlot Body;
    public EquipmentSlot Shoes;
    public int Damage;
    public int Health;
    public int Stamina;
    public int Defence;
    public int Critical;

    public bool once = false;

    int saveDam = 0;
    int saveHea = 0;
    int saveSta = 0;
    int saveDef = 0;
    int saveCri = 0;

    void Start()
    {
        saveDam = Damage;
        saveHea = Health;
        saveSta = Stamina;
        saveDef = Defence;        
        saveCri = Critical;
    }
    void Update()
    {
        if(once)
        {
            StatManager();
            once = false;
        }
    }

    void StatManager()
    {
        int Wdam = 0;
        int Whea = 0;
        int Wsta = 0;
        int Wdef = 0;
        int Wcri = 0;
        int Bdam = 0;
        int Bhea = 0;
        int Bsta = 0;
        int Bdef = 0;
        int Bcri = 0;
        int Hdam = 0;
        int Hhea = 0;
        int Hsta = 0;
        int Hdef = 0;
        int Hcri = 0;
        int Sdam = 0;
        int Shea = 0;
        int Ssta = 0;
        int Sdef = 0;
        int Scri = 0;
        ItemStat WStat = Weapon.GetComponentInChildren<ItemStat>();
        ItemStat BStat = Body.GetComponentInChildren<ItemStat>();
        ItemStat HStat = Head.GetComponentInChildren<ItemStat>();
        ItemStat SStat = Shoes.GetComponentInChildren<ItemStat>();
        if(WStat != null)
        {
            Wdam = WStat.Dam;
            Whea = WStat.Hea;
            Wsta = WStat.Sta;
            Wdef = WStat.Def;
            Wcri = WStat.Cri;
        }
        else
        {
            Wdam = 0;
            Whea = 0;
            Wsta = 0;
            Wdef = 0;
            Wcri = 0;
        }

        if(BStat != null)
        {
            Bdam = BStat.Dam;
            Bhea = BStat.Hea;
            Bsta = BStat.Sta;
            Bdef = BStat.Def;
            Bcri = BStat.Cri;
        }
        else
        {
            Bdam = 0;
            Bhea = 0;
            Bsta = 0;
            Bdef = 0;
            Bcri = 0;
        }

        if(HStat != null)
        {
            Hdam = HStat.Dam;
            Hhea = HStat.Hea;
            Hsta = HStat.Sta;
            Hdef = HStat.Def;
            Hcri = HStat.Cri;
        }
        else
        {
            Hdam = 0;
            Hhea = 0;
            Hsta = 0;
            Hdef = 0;
            Hcri = 0;
        }
        if(SStat != null)
        {
            Sdam = SStat.Dam;
            Shea = SStat.Hea;
            Ssta = SStat.Sta;
            Sdef = SStat.Def;
            Scri = SStat.Cri;
        }
        else
        {
            Sdam = 0;
            Shea = 0;
            Ssta = 0;
            Sdef = 0;
            Scri = 0;
        }
        Damage = saveDam + Wdam + Bdam + Hdam + Sdam + buttonUp[0].num;
        StatClient[0].statText.text = Damage.ToString();
        Health = saveHea + Whea + Bhea + Hhea + Shea + buttonUp[1].num;
        StatClient[1].statText.text = Health.ToString();
        Stamina = saveSta + Wsta + Bsta + Hsta + Ssta + buttonUp[2].num;
        StatClient[2].statText.text = Stamina.ToString();
        Defence = saveDef + Wdef + Bdef + Hdef + Sdef + buttonUp[3].num;
        StatClient[3].statText.text = Defence.ToString();
        Critical = saveCri + Wcri + Bcri + Hcri + Scri + buttonUp[4].num;
        StatClient[4].statText.text = Critical.ToString() + "%";
    }
}
