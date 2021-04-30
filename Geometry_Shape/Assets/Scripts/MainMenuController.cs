using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private int[] loadSceneSaved = new int[3];
    private string[] nameScene = new string[5];
    private int[] nameSceneInt = new int[5];
    private int indexNameScene;
    private int loadScene;
    private bool newGame;

    public TMP_Text[] slots;
    private int[] slotsInt;

    //Referencias
    public GameObject newGameMenu;
    public GameObject startMenu;
    public DatabaseController _database;

    private void Start()
    {
        nameScene = new string[] {"Llamada del árbol primigenio", "Buscando la cueva", "Cueva maldita: Entrada", "Cueva maldita: Profundidades", "Batalla final"}; //Texto del nivel en el que se guarda la partida
        nameSceneInt = new int[] { 2, 5, 8, 10, 12 }; //Escenas en las cuales puede guardar el jugador
    }

    private void Update()
    {
        if (newGameMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            newGameMenu.SetActive(false);
            startMenu.SetActive(true);

            newGame = false;
        }
    }

    //Abre el menú de partidas guardadas.
    public void SlotsMenu()
    {
        slotsInt = new int[slots.Length];

        newGame = false;

        loadSceneSaved = _database.GetLoadScene();
        newGameMenu.SetActive(true);
        startMenu.SetActive(false);

        //Establece el texto de cada hueco de partida guardada.
        for (int i = 0; i < slots.Length; i++)
        {
            if (loadSceneSaved[i] == -1 || loadSceneSaved[i] == 0)
            {
                slots[i].text = "Nueva Partida";
            }
            else
            {
                for (int j = 0; j < nameSceneInt.Length; j++)
                {
                    if (nameSceneInt[j] == loadSceneSaved[i])
                    {
                        indexNameScene = j;
                        break;
                    }
                    else
                    {

                    }
                }
                    slots[i].text = nameScene[indexNameScene];
                    slotsInt[i] = loadSceneSaved[i];
            }
        }

        
    }
    
    public void NewGameButton()
    {
        SlotsMenu();
        newGame = true;
    }
    public void LoadGameButton()
    {
        SlotsMenu();
    }

    //Se ejecuta al pulsar uno de los huecos de partidas guardadas.
    public void SlotSelection(int slotPressed)
    {
        if (newGame)
        {
            loadScene = 1;
        }
        else
        {
            loadScene = slotsInt[slotPressed - 1];
        }

        _database.SetLoadScene(loadScene, slotPressed);

        SceneManager.LoadScene(loadScene);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
