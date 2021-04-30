using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivable : MonoBehaviour
{
    public GameObject movableObject;
    public GameObject activableObject;
    public SoundManager _audio;
    public LevelChanger _levelChanger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == movableObject.gameObject)
        {
            if (!MovableObject.getDragging() )
            {
                _audio.Activable();
            }

            _levelChanger.FadeToNextLevel();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == movableObject.gameObject)
        {
            activableObject.SetActive(false);

            if (!MovableObject.getDragging())
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }

}
