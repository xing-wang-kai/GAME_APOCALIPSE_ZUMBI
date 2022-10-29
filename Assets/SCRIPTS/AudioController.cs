using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource myAudioSouce;
    public static AudioSource currentAdioSource;

    public void Awake()
    {
        this.myAudioSouce = GetComponent<AudioSource>();
        AudioController.currentAdioSource = this.myAudioSouce;
    }
}
