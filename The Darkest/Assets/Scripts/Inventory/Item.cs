using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public GameObject Items;
    public enum Equipment
    {
        Body, Bow, Head, Shoes, Comsumable
    }
    public Equipment equipMent = default;
}
