using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : MonoBehaviour
{
    //Referencias
    MecanicsRectangle rectangle;
    public GameObject player;
    PlayerController playerController;
    public SoundManager _audio;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        //Si "Rectángulo" girado entra en contacto con el agua, este se quedará en la parte de arriba.
        if(playerController.shape == 7)
        {
            rectangle = playerController.GetComponentInChildren<MecanicsRectangle>();

            if (rectangle.turned == false)
            {
                gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            }
        }
        else
        {
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audio.Water();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audio.Water();
    }


}
