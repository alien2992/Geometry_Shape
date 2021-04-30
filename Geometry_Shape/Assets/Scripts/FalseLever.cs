using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseLever : MonoBehaviour
{
    public GameObject player;
    private bool trigger = false;

    private void Update()
    {
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            trigger = false;
        }
    }
}
