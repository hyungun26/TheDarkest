using UnityEngine;

public abstract class AnimationEvent : MonoBehaviour
{
    public AudioSource audioSource;

    public abstract void WalkSound();
    public abstract void RunSound();
}
