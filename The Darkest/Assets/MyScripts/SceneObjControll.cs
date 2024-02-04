using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjControll : Scenes
{
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string str = SceneManager.GetActiveScene().name;
        if (str == "Intro" || str == "LoadingScene")
            this.transform.GetChild(0).gameObject.SetActive(false);
        else
            this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
