using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controla el accionamiento de las palancas
public class LeverController : MonoBehaviour
{
    bool trigger;

    //Referencias
    public GameObject door;
    Animator _anim;
    public Animator _animKey;
    public SoundManager _audio;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (trigger)
        {
            _animKey.SetBool("Show_E", true);
        }
        else if (!trigger)
        {
            _animKey.SetBool("Show_E", false);
        }

        // La palanca se accionará si el jugador pulsa "E" cerca de ella
        if (Input.GetKeyDown(KeyCode.E) && trigger)
        {
            _audio.Lever();
            door.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _anim.SetTrigger("LeverActive");
        }
    }

    private void OnTriggerEnter2D()
    {
        trigger = true;
    }

    private void OnTriggerExit2D()
    {
        trigger = false;
    }

}