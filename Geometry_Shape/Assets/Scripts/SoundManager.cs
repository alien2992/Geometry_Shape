using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSource;

    private AudioSource jump;
    private AudioSource shapeChange;
    private AudioSource turnRectangle;
    private AudioSource activable;
    private AudioSource objectFall;
    private AudioSource hiddenWall;
    private AudioSource powerCircle;
    private AudioSource water;
    private AudioSource death;
    private AudioSource lever;
    private AudioSource powerTriangle;

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        shapeChange = audioSource[0];
        jump = audioSource[1];
        objectFall = audioSource[2];
        turnRectangle = audioSource[3];
        activable = audioSource[4];
        hiddenWall = audioSource[5];
        powerCircle = audioSource[6];
        water = audioSource[7];
        death = audioSource[8];
        lever = audioSource[9];
        powerTriangle = audioSource[10];

    }
    public void ShapeChange()
    {
        audioSource[0].Play();
    }

    public void Jump()
    {
        audioSource[1].Play();
    }

    public void ObjectFall()
    {
        audioSource[2].Play();
    }

    public void TurnRectangle()
    {
        audioSource[3].Play();
    }

    public void Activable()
    {
        audioSource[4].Play();
    }

    public void HiddenWall()
    {
        audioSource[5].Play();
    }

    public void PowerCircle()
    {
        audioSource[6].Play();
    }

    public void Water()
    {
        audioSource[7].Play();
    }

    public void Death()
    {
        audioSource[8].Play();
    }

    public void Lever()
    {
        audioSource[9].Play();
    }

    public void PowerTriangle()
    {
        audioSource[10].Play();
    }


}
