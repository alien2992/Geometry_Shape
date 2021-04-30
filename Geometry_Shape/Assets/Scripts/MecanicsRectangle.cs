using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicsRectangle : MonoBehaviour
{
    public bool _isGroundedWater;
    public bool turned = false;
    public float accelerate = 2f;
    public float groundCheckRadius;

    //Referencias
    PlayerController playerController;
    Rigidbody2D _rigidbody;
    public GameObject floorPointTurned;
    public LayerMask groundLayer;
    public SoundManager _audio;

    private void Awake()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
        _rigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        //Gira a Rectángulo si se pulsa el botón izquierdo del ratón
        if (Input.GetButtonDown("Fire1"))
        {
            Turn();
        }

        _isGroundedWater = Physics2D.OverlapCircle(floorPointTurned.transform.position, groundCheckRadius, groundLayer);

        if (_isGroundedWater && Input.GetKeyDown(KeyCode.Space))
        {
            playerController.Jump(false);
        }
    }

    //Gira al personaje para poder flotar en el agua
    public void Turn()
    {
        //Para tener el floorPoint2 siempre mirando al suelo y evitar que Rectángulo se de la vuelta
        if (!turned)
        {
            turned = true;
            if(playerController.facingRight == false)
            {
                gameObject.GetComponent<Transform>().Rotate(0, 0, 90);
            }
            else
            {
                gameObject.GetComponent<Transform>().Rotate(0, 0, 90);
            }
        }
        else
        {
            turned = false;
            if(playerController.facingRight == false)
            {
                gameObject.GetComponent<Transform>().Rotate(0, 0, -90);
            }
            else
            {
                gameObject.GetComponent<Transform>().Rotate(0, 0, -90);
            }
        }

        //Activar o desactivar el floorPoint que corresponda.
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Transform>().gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
                playerController.groundCheck = child;
            }
        }

        _audio.TurnRectangle();
    }


}
