using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableMultiple : MonoBehaviour
{
    public GameObject activableObject;
    public int count = 0;
    public SoundManager _audio;

    private void Update()
    {
        if (count == 3)
        {
            activableObject.SetActive(false);
        }
        else
        {
            activableObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            count += 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {

            if(!MovableObject.getDragging())
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                _audio.Activable();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovableObject"))
        {
            count -= 1;

            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
