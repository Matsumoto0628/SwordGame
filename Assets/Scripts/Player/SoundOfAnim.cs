using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOfAnim : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] audioClips;

    public void AttackSound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
}
