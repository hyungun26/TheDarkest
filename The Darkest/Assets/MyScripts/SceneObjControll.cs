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
            
        //������ player�� Scene�� �̵��ϸ鼭 �����ϱ� PlayerScenes ��ũ��Ʈ�� ������ ���� �׷��ٰ�
        //�ѱ�� ������ �ε��� �ϸ� ���� ����ȭ�鿡���� �Ⱥ������� player�������� ����� ������ ���߿� �и���
        //������ ���沨��� ������ ��Ȯ�� ������ �𸣰����� ���߿� Scene�� �ű�°������� DonDestroyObj�� �����ϸ�
        //������ �߻��Ҳ��� ������
    }
}
