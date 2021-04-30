using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{

    //Referencias
    public GameObject[] display;
    public GameObject continueButton;
    public TextMeshProUGUI[] textDisplay;
    public PlayableDirector[] director;
    public PauseMenuCutscenes _pauseMenuCutscenes;
    public LevelChanger _levelChanger;
    public AudioSource _audio;

    //Texto
    public string[] sentences;
    public float typeSpeed;
    private string finalText;

    //General
    public int index; //display[index]
    public int[] indexStop; //Los momentos en los que habrá una animación.
    private int indexDirector = 0; //director[indexDirector]
    private int shape; //Para saber el cuadro de texto que debe mostrarse.
    private int lastShape = -1;
    public bool activedTimeline = true;
    public bool startTimeline = false;
    public bool playing = false;


    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            _audio = GetComponent<AudioSource>();
        }

        if (display != null)
        {
            //Almacenamos en texDisplay[] los cuadros de texto y los desactivamos.
            for (int i = 0; i < display.Length; i++)
            {
                textDisplay[i] = display[i].GetComponentInChildren<TextMeshProUGUI>();
                display[i].SetActive(false);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        
        if (indexDirector < director.Length)
        {
            //El botón de continuar se desactivará mientras una cinemática esté activa.
            if (director[indexDirector] != null && director[indexDirector].state == PlayState.Playing)
            {
                continueButton.SetActive(false);
            }
            else if (!_pauseMenuCutscenes.gameIsPaused)
            {
                continueButton.SetActive(true);
            }
            else
            {
                continueButton.SetActive(false);
            }
        }
    }

    //Mostrará el texto en uno de los cuadros de texto.
    IEnumerator Type()
    {
        lastShape = shape;

        //Es un filtro almacenado en cada frase para saber quién está hablando en cada momento
        if (sentences[index].Substring(0, 9) == "Square   ")
        {
            shape = 0;
        }
        else if (sentences[index].Substring(0, 9) == "Triangle ")
        {
            shape = 1;
        }
        else if (sentences[index].Substring(0, 9) == "Circle   ")
        {
            shape = 2;
        }
        else if (sentences[index].Substring(0, 9) == "Rectangle")
        {
            shape = 3;
        }
        else if (sentences[index].Substring(0, 9) == "All      ")
        {
            shape = 4;
        }
        else if (sentences[index].Substring(0, 9) == "MagicEnt ")
        {
            shape = 5;
        }
        else if (sentences[index].Substring(0,9) == "Eraser   ")
        {
            shape = 6;
        }

        //Se desactiva el anterior cuadro de texto cuando se pulsa el boton de continuar.
        if (lastShape >= 0)
        {
            display[lastShape].SetActive(false);
        }

        display[shape].SetActive(true); //Se activa el siguiente cuadro de texto.

        finalText = sentences[index].Substring(9); //Se recorta la frase para no mostrar el filtro.

        //Se muestra el texto letra por letra con una velocidad dada.
        foreach (char letter in finalText.ToCharArray())
        {
            textDisplay[shape].text += letter;

            if(!playing && _audio != null)
            {
                _audio.Play();
                playing = true;
            }

            if (textDisplay[shape].text == finalText && _audio != null)
            {
                _audio.Stop();
                playing = false;
            }
            yield return new WaitForSeconds(typeSpeed);
            
        }
    }

    public void NextSentence()
    {
        //Cambiará de escena cuando termine la cinemática.
        if (index == sentences.Length - 1 && textDisplay[shape].text == finalText)
        {
            StopAllCoroutines();
            display[shape].SetActive(false);
            _levelChanger.FadeToNextLevel();
        }
        //Si el texto ha llegado al final, al pulsar la tecla continuar se mostrará la siguiente frase
        else if (textDisplay[shape].text == finalText)
        {
            if (indexStop != null)
            {
                for (int i = 0; i < indexStop.Length; i++)
                {
                    if (index == indexStop[i])
                    {
                        startTimeline = true;
                    }
                }
            }

            //Filtro para los momentos en los que se mostrará una cinemática.
            if (startTimeline && activedTimeline)
            {
                ActiveTimeline();
            }
            else
            {
                if (!activedTimeline)
                {
                    director[indexDirector].gameObject.SetActive(false); //Desactiva el Timeline cuando termina la cinemática.

                    if (indexDirector < director.Length - 1)
                    {
                        ++indexDirector;
                    }
                }

                ++index;
                startTimeline = false;
                activedTimeline = true;
                textDisplay[shape].text = "";
                StartCoroutine(Type()); //Se inicia la corrutina para ecribir el texto
            }
        }
        //Si no, entonces inmediatamente se mostrará la frase completa
        else
        {
            StopAllCoroutines();
            textDisplay[shape].text = "";
            textDisplay[shape].text = finalText;

            if (_audio != null)
            { 
                _audio.Stop();
                playing = false;
            }
        }
    }

    //Mostrar una cinemática.
    public void ActiveTimeline()
    {
        display[shape].SetActive(false);

        activedTimeline = false;

        director[indexDirector].gameObject.SetActive(true);
    }
}
