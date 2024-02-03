using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    private void Awake()
    {
        LoadingSceneManager.LoadScene("Intro");
    }
}
