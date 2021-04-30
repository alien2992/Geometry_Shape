using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Controlador de las "VirtualCamera" para cambiar de una a otra cuando el jugador entre en dicha habitación
public class RoomCamera : MonoBehaviour
{
    public GameObject vCamGO;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Cambiar de cámara cuando el jugador entre a otra habitación.
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            vCamGO.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Cuando el jugador salga de la habitación, la cámara de esa habitación se desactiva.
        if (other.CompareTag("Player"))
        {
            vCamGO.SetActive(false);
        }
    }
}
