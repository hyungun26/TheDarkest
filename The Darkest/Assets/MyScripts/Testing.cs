using UnityEngine;

public class Testing : MonoBehaviour
{

    public Transform purpos;
    public float rotPower;
    private void Start()
    {
    }
    private void Update()
    {
        Vector3 dir = purpos.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), rotPower * Time.deltaTime);
    }
}
