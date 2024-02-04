using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScenes : Scenes
{
    public PlayerController player;
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.anim.Rebind();
        player.DeadCamera.Rebind();
        player.DeadCamera.enabled = false;
        player.anim.enabled = false;
        player.anim.enabled = true;
        Debug.Log("확인");
        player.ChangeState(PlayerController.PlayerState.Play);
        string SceneName = scene.name;
        if (SceneName == "The Darkest RestPlace")
        {
            player.DeadSceneAll.gameObject.SetActive(false);
            player.transform.position = Vector3.up * 1.5f;
            player.PlayerHP.value = 100.0f;
        }
        else if(SceneName == "MonsterArea")
        {
            Debug.Log("MonsterArea 입장");
            player.transform.position = Vector3.up;
        }
        else if(SceneName == "BossStage")
        {
            Debug.Log("왜 안들어옴???");
            player.transform.position = Vector3.forward * 67.0f + Vector3.left * 30.0f;
            Debug.Log(player.transform.position);
        }
    }
}
