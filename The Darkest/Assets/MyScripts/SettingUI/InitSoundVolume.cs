using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitSoundVolume : Scenes
{
    public AudioClip[] Bgm;
    new AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 0.2777778f;
    }

    public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string SceneName = SceneManager.GetActiveScene().name;
        switch(SceneName)
        {
            case "Intro": audio.clip = Bgm[0];
                break;
            case "The Darkest RestPlace": audio.clip = Bgm[1];
                break;
            case "BossStage": audio.clip = Bgm[2];
                break;
            case "MonsterArea": audio.clip = Bgm[3];
                break;
            default: audio.clip = null;
                    break;
        }
        audio.Play();
    }
}
