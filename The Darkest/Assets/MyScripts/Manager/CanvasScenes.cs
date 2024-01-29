using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScenes : Scenes
{
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name != "LoadingScene")
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
