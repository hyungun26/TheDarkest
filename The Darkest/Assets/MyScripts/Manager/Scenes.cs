using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Scenes : MonoBehaviour
{
    // ���⼭�� ���� ����ɶ����� player��ġ ���� + ����� ������׿� ���� ��ȭ�� �ִ� script�̴�.
    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //���⼭ ���� �����̵Ǹ� �ʱ�ȭ ���ش�.
    public abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);

    // ������ ����Ǹ� ȣ���� �ȴٶ�...
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
