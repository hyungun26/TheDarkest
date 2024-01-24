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
        if(SceneManager.GetActiveScene().name == "Testing")
        {
            player = this.transform.GetComponent<PlayerController>();
            player.anim.Rebind();
            player.DeadCamera.Rebind();
            player.DeadCamera.enabled = false;
            player.DeadSceneAll.gameObject.SetActive(false);
            player.anim.enabled = false;
            player.anim.enabled = true;
            player.transform.position = Vector3.up;
            player.ChangeState(PlayerController.PlayerState.Play);
            player.PlayerHP.value = 100.0f;
        }
    }
}
