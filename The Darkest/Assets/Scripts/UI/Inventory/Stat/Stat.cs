using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public GameObject StatManager;
    public TextMeshProUGUI statText;
    Transform me;
    void Start()
    {
        me = GetComponent<Transform>();
        statText = me.GetComponent<TextMeshProUGUI>();
    }
}
