using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject windElement;
    public GameObject fireElement;
    public GameObject earthElement;
    public GameObject waterElement;

    public GameObject player;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_playerController.shape == 1)
        {
            windElement.SetActive(true);
            fireElement.SetActive(false);
            earthElement.SetActive(false);
            waterElement.SetActive(false);
        }
        else if (_playerController.shape == 3)
        {
            fireElement.SetActive(true);
            windElement.SetActive(false);
            earthElement.SetActive(false);
            waterElement.SetActive(false);
        }
        else if (_playerController.shape == 5)
        {
            earthElement.SetActive(true);
            windElement.SetActive(false);
            fireElement.SetActive(false);
            waterElement.SetActive(false);
        }
        else if (_playerController.shape == 7)
        {
            waterElement.SetActive(true);
            windElement.SetActive(false);
            fireElement.SetActive(false);
            earthElement.SetActive(false);
        }
    }
}
