using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource[] audioSource;
    public int selector;

    private AudioSource calmMusic;
    private AudioSource firstCaveMusic;
    private AudioSource secondCaveMusic;
    private AudioSource bossTalkMusic;
    private AudioSource bossFightMusic;
    private AudioSource endMusic;


    private void Awake()
    {
        audioSource = GetComponents<AudioSource>();
        calmMusic = audioSource[0];
        firstCaveMusic = audioSource[1];
        secondCaveMusic = audioSource[2];
        bossTalkMusic = audioSource[3];
        bossFightMusic = audioSource[4];
        endMusic = audioSource[5];
    }

    private void Start()
    {
        if (selector == 0)
        {
            CalmMusic();
        }
        else if (selector == 1)
        {
            FirstCaveMusic();
        }
        else if (selector == 2)
        {
            SecondCaveMusic();
        }
        else if (selector == 3)
        {
            BossTalkMusic();
        }
        else if (selector == 4)
        {
            BossFightMusic();
        }
        else if (selector == 5)
        {
            EndMusic();
        }
    }

    public void CalmMusic()
    {
        audioSource[0].Play();
    }

    public void FirstCaveMusic()
    {
        audioSource[1].Play();
    }
    public void SecondCaveMusic()
    {
        audioSource[2].Play();
    }
    public void BossTalkMusic()
    {
        audioSource[3].Play();
    }
    public void BossFightMusic()
    {
        audioSource[4].Play();
    }
    public void EndMusic()
    {
        audioSource[5].Play();
    }
}
