using UnityEngine;

public class Testing : MonoBehaviour
{

    public float dis;
    private void Start()
    {
    }
    private void Update()
    {
        float sizeOfCircle = this.transform.localScale.x;
        float radius = GetRadius(sizeOfCircle);
        Debug.Log("원의 사이즈 : " + sizeOfCircle + " 원의 반지름: " + radius);

        Collider[] attack = Physics.OverlapSphere(this.transform.position, dis);
        foreach (Collider coll in attack)
        {
            coll.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    float GetRadius(float size)
    {
        float pi = 3.14f;
        float tmp = size / pi;
        float radius = Mathf.Sqrt(tmp);
        return radius;
    }
}
