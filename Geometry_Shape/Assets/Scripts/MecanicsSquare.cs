using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Doble salto para la forma "Cuadrado"
public class MecanicsSquare : MonoBehaviour
{
    private bool doubleJump = false;

    //Referencias
    private PlayerController playerController;

    private void Awake()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        /*Si se pulsa la tecla "Espacio", el jugador no se encuentra en el suelo y 
         * no se ha hecho el doble salto aún, entonces se realiza un doble salto*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJump();
        }
        if (playerController._isGrounded)
        {
            doubleJump = false;
        }
        
    }

    public void DoubleJump()
    {
        if(!(playerController._isGrounded) && !doubleJump)
        {
            doubleJump = true;
            
            SendMessageUpwards("Jump", doubleJump);

        }
    }
}
