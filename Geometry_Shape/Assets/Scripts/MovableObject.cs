using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    //Referencias.
    PlayerController playerController;
    public GameObject player;
    public static bool dragging;


    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }


    //Movimiento de objeto
    private void OnMouseDown() //Para mayor precisión
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseDrag()
    {
        if (playerController.shape == 1)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime);
            dragging = true;

            if (dragging)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }



    public static bool getDragging()
    {
        return dragging;
    }


}
