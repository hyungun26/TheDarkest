using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public GameObject[] dropItem;
    public void DropItem()
    {
        int num = Random.Range(0, dropItem.Length);
        Instantiate(dropItem[num], this.transform.position, this.transform.rotation);
    }
}
