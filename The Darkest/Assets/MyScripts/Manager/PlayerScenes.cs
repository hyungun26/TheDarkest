using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScenes : Scenes
{
    public RectTransform stamina;
    public RectTransform hp;
    public PlayerController player;
    public InitSoundVolume BgmVol;
    Rigidbody rigid;
    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.enabled = true;
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
            hp.gameObject.SetActive(true);
            stamina.gameObject.SetActive(true);
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
            player.enabled = false;
            if (rigid != null)
                rigid.useGravity = false;
        }
    }
}
