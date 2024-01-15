using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Camera camera;
    public GameObject spawn;
    public float spawnTime;
    //Update is called once per frame
    void LateUpdate()
    {
        spawnTime += 1.0f * Time.deltaTime;
        if (spawnTime > 5.0f)
        {
            spawnTime = 0.0f;
            float x = Random.Range(-10, 11);
            float z = Random.Range(-10, 11);
            float rotY = Random.Range(-360, 361);
            Instantiate(spawn, this.transform.position + Vector3.forward * z + Vector3.right * x,
                this.transform.localRotation * Quaternion.Euler(Vector3.up * rotY));
        }
    }
}
