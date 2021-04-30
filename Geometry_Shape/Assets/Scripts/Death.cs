using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public SoundManager _audio;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shape"))
        {
            _audio.Death();
            other.GetComponentInParent<PlayerController>().Death();
        }
    }
}
