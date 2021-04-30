using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    public bool autoSwap;
    private bool trigger = false;
    private Animator _animator;
    public LevelChanger _levelChanger;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (trigger)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _levelChanger.FadeToNextLevel();
            }
            else if (autoSwap)
            {
                _levelChanger.FadeToNextLevel();
            }

            if (trigger)
            {
                if (_animator != null)
                {
                    _animator.SetBool("Show_W", true);
                }
            }
            
        }
        else if (!trigger)
        {
            if (_animator != null)
            {
                _animator.SetBool("Show_W", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }
}
