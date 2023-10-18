using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat2 : MonoBehaviour
{

    public TextMeshProUGUI Point;

    public int usePoint;
    public int savemaxPoint;

    public void GetPoint(int p)
    {
        savemaxPoint += p;
        usePoint += p;
        string a = usePoint.ToString();
        Point.text = a;
    }
}
