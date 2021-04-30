using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuCutscenes : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject _dialogBoxes;
    public Transform temp;
    public Dialog _dialog;
    public GameObject continueButton;
    public LevelChanger _levelChanger;


    //Referencias
    public GameObject pauseMenuUI;
    public DatabaseController _database;


    private void Update()
    {
        //El juego se pausa si se pulsa la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Vuelve el juego a la normalidad
    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        continueButton.SetActive(true);

        if (_dialog.playing)
        {
            _dialog._audio.Play();
        }

        if (temp != null && _dialog.activedTimeline == true)
        {
            temp.gameObject.SetActive(true);
        }

    }

    //Pausa el juego y abre el menú de pausa
    public void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        continueButton.gameObject.SetActive(false);
        _dialog._audio.Stop();

        foreach (Transform child in _dialogBoxes.transform)
        {
            if(child.gameObject.activeSelf)
            {
                temp = child;
                child.gameObject.SetActive(false);
            }
        }
        Time.timeScale = 0f;
    }

    public void Skip()
    {
        pauseMenuUI.SetActive(false);
        _levelChanger.FadeToNextLevel();
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }

}

