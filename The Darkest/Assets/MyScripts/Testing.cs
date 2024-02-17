using UnityEngine;

public class Testing : MonoBehaviour
{
    public Transform pos1, pos2;
    [Range(0,1)]
    public float value;

    private void Start()
    {
    }
    private void Update()
    {
        transform.position = Vector3.Slerp(pos1.position, pos2.position, value);
        
            
    }
}
