using UnityEngine;

public class GridSlots : MonoBehaviour
{
    RectTransform tr;
    int childC = 28;
    
    void Start()
    {
        tr = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(childC + 1 <= tr.childCount)
        {
            childC += 4;
            tr.sizeDelta += new Vector2(0, 100);
        }
    }
}
