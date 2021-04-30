using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    public GameObject player;
    PlayerController playerController;
    public SoundManager _audio;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnMouseDown()
    {
        //Si Triángulo está como máximo a 10 de distancia y hace click en el objecto, este se destruye.
        if (playerController.shape == 3 && Vector2.Distance(transform.position, player.transform.position) < 30)
        {
            _audio.PowerTriangle();
            Destroy(this.gameObject);
        }
    }
}
