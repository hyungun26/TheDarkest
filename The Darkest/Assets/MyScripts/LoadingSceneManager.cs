using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    PoolManager pool;
    public static string nextScene;
    [SerializeField]
    Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
        //LoadScene방식은 동기 식으로 loading중일때 아무일도 못한다
        //LoadSceneAsync는 비동기 식으로 loading중일때 다른 작업을 할 수 있다.
    }

    IEnumerator LoadScene()
    {
        if(pool == null)
        {
            pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
            pool.PoolManagerInit();
            
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;//장면이 준비된 즉 시 장면이 활성화되는 것을 허용할지 안할지

        float timer = 0f;
        while (!op.isDone)//씬로딩이 끝나지 않았다면
        {
            
            yield return null;
            if(op.progress < 0.9f)
            {
                progressBar.value = op.progress;
            }
            else//fake loading
            {
                timer += Time.deltaTime;
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer/5.0f);
                if(progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    pool = null;
                    yield break;
                }
            }
        }
    }
}
