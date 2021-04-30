using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static int slotPressed;

    //Referencias
    public GameObject pauseMenuUI;
    public GameObject statisticsMenuUI;
    public GameObject jumpGO;
    public GameObject timeGO;
    public TMP_Text jumpTxt;
    public TMP_Text timeTxt;
    public DatabaseController _database;

    private void Start()
    {
        slotPressed = DatabaseController.slotPressedDatabase;
    }

    private void Update()
    {
        //El juego se pausa si se pulsa la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(false);
    }

    //Pausa el juego y abre el menú de pausa
    public void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        statisticsMenuUI.SetActive(false);
    }

    //Abre el menú de Estadísticas.
    public void StatisticsMenu(int jumps, int time)
    {
        pauseMenuUI.SetActive(false);
        statisticsMenuUI.SetActive(true);

        jumpTxt = jumpGO.GetComponent<TMP_Text>();
        jumpTxt.text = jumps.ToString();

        timeTxt = timeGO.GetComponent<TMP_Text>();
        timeTxt.text = time.ToString();
    }

    public void Exit()
    {
        Time.timeScale = 1;
        print(SceneManager.GetActiveScene().buildIndex);
        _database.SetLoadScene(SceneManager.GetActiveScene().buildIndex, slotPressed);
        print(SceneManager.GetActiveScene().buildIndex);



        SceneManager.LoadScene(0);
    }

}
