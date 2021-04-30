using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    public GameObject player;
    public SoundManager _audio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            Destroy(this.gameObject);
            _audio.HiddenWall();
        }
    }
}
