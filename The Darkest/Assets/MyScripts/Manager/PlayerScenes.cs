using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScenes : Scenes
{
    public PlayerController player;
    Rigidbody rigid;
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        rigid = player.GetComponent<Rigidbody>();
        player.anim.Rebind();
        player.DeadCamera.Rebind();
        player.DeadCamera.enabled = false;
        player.anim.enabled = false;
        player.anim.enabled = true;
        
        player.ChangeState(PlayerController.PlayerState.Play);
        string SceneName = SceneManager.GetActiveScene().name;

        if(rigid != null)
            rigid.useGravity = true;
        
        if (SceneName == "The Darkest RestPlace")
        {
            player.DeadSceneAll.gameObject.SetActive(false);
            player.transform.position = Vector3.up * 1.5f;
        }
        else if (SceneName == "MonsterArea")
        {
            player.transform.position = Vector3.up;
        }
        else if (SceneName == "BossStage")
        {
            player.transform.position = Vector3.forward * 67.0f + Vector3.left * 30.0f;
        }
        else //Loading & Intro Scene player gravity false
        {
            //if(rigid != null)
            //    rigid.useGravity = false;
            //bossStage 테스트로 풀어놨음 테스트 종료되면 주석풀것
        }
    }
}
