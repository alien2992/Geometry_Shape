using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCutscene : MonoBehaviour
{
    AudioSource[] _audio;
    public bool play;
    public bool play2;
    private void Awake()
    {
        _audio = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (play)
        {
            _audio[0].Play();
        }

        if (play2)
        {
            _audio[1].Play();
        }
    }
}
