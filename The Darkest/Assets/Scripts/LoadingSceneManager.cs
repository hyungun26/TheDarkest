using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
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
        //LoadScene����� ���� ������ loading���϶� �ƹ��ϵ� ���Ѵ�
        //LoadSceneAsync�� �񵿱� ������ loading���϶� �ٸ� �۾��� �� �� �ִ�.
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;//����� �غ�� �� �� ����� Ȱ��ȭ�Ǵ� ���� ������� ������

        float timer = 0f;
        while (!op.isDone)//���ε��� ������ �ʾҴٸ�
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
                    yield break;
                }
            }
        }
    }
}
