using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjControll : Scenes
{
    public GameObject UIAll;
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string str = SceneManager.GetActiveScene().name;
        if (str == "The Darkest RestPlace")
            UIAll.SetActive(true);
        //if (str == "Intro" || str == "LoadingScene")
        //    this.transform.GetChild(0).gameObject.SetActive(false);
        //else
        //    this.transform.GetChild(0).gameObject.SetActive(true);
            
        //문제점 player가 Scene을 이동하면서 꺼지니까 PlayerScenes 스크립트가 반응을 못함 그렇다고
        //켜기는 싫은게 로딩을 하면 물론 게임화면에서는 안보이지만 player떨어지는 모습을 보여줌 나중에 분명히
        //문제가 생길꺼라고 생각함 정확한 문제는 모르겠지만 나중에 Scene을 옮기는과정에서 DonDestroyObj을 많이하면
        //문제가 발생할꺼라 생각함
    }
}
