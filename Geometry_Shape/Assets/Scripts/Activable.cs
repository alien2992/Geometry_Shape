using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    public GameObject movableObject;
    public GameObject activableObject;
    public SoundManager _audio;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == movableObject.gameObject)
        {
            activableObject.SetActive(true);

            if (!MovableObject.getDragging())
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                _audio.Activable();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == movableObject.gameObject)
        {
            activableObject.SetActive(false);

            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
