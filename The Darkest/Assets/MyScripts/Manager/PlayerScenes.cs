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
        player.ChangeState(PlayerController.PlayerState.Play);
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName == "The Darkest RestPlace")
        {
            player.DeadSceneAll.gameObject.SetActive(false);
            player.transform.position = Vector3.up;
            player.PlayerHP.value = 100.0f;
        }
        else if(SceneName == "MonsterArea")
        {
            player.transform.position = Vector3.up;
        }
        else if(SceneName == "BossStage")
        {
            player.transform.position = Vector3.up * 5.0f;
        }
    }
}
