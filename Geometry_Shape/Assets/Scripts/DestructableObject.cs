using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public Vector2 vel;

    //Referencias
    public GameObject player;
    PlayerController playerController;
    public SoundManager _audio;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        vel = collision.GetComponentInParent<Rigidbody2D>().velocity;

        //El objeto se destruye si Círculo colisiona con él y tiene una velocidad mayor o igual a 20.
        if (playerController.shape == 5 && (vel.x >= 27 || vel.x <= -27))
        {
            _audio.PowerCircle();
            Destroy(gameObject);
        }
    }
  
}
