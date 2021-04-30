using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicsCircle : MonoBehaviour
{
    //Referencias
    public Rigidbody2D _rigidbody;
    private PlayerController playerController;

    //Movimiento
    public float speed; 
    public float maxSpeed = 20f;
    public float accelerate = 50f;
    public float decelerate = 80f;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        speed = _rigidbody.velocity.x;
    }

    //Control del movimiento de Círculo
    public void Accelerate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //Limita la velocidad máxima a la que puede llegar
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }
        else if (speed <= -maxSpeed)
        {
            speed = -maxSpeed;
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }

        //Acelera o decelera según el Input
        if (horizontalInput == 0 && speed > 0) //Si está en movimiento pero el jugador no está pulsando la tecla de movimiento el personaje decelera
        {
            if (speed < 2)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.AddForce(new Vector2(-decelerate, _rigidbody.velocity.y));
            }
        }
        else if (horizontalInput == 0 && speed < 0) //Igual que antes pero con velocidad negativa
        {
            if (speed > -2)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.AddForce(new Vector2(decelerate, _rigidbody.velocity.y));
            }
        }
        else if(horizontalInput == 1 || horizontalInput == -1) //El jugador pulsa y mantiene la tecla de movimiento, entonces el personaje acelera.
        {
            _rigidbody.AddForce(new Vector2(horizontalInput * accelerate, _rigidbody.velocity.y));
        }

        if (horizontalInput == -1 && playerController.facingRight || horizontalInput == 1 && !playerController.facingRight) //El personaje gira para mirar hacia donde se mueve
        {
            SendMessageUpwards("Flip");
        }
    }
}
