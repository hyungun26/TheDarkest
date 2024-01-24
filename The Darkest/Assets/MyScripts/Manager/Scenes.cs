using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Scenes : MonoBehaviour
{
    // 여기서는 씬이 변경될때마다 player위치 변경 + 사소한 변경사항에 대해 변화를 주는 script이다.
    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //여기서 씬이 변경이되면 초기화 해준다.
    public abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);

    // 게임이 종료되면 호출이 된다라...
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
