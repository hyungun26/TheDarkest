using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat2 : MonoBehaviour
{     
    public TextMeshProUGUI Point;
    int usePoint;
    public void GetPoint(int p)
    {
        usePoint = int.Parse(Point.text);
        usePoint += p;
        string a = usePoint.ToString();
        Point.text = a;
    }
}
