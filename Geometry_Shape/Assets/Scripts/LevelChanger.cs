using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator _animator;
    public DatabaseController _database;
    private int slotPressed;

    private int levelToLoad;

    private void Start()
    {
        slotPressed = DatabaseController.slotPressedDatabase;
    }

    public void FadeToNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            TheEnd();
        }
        else
        {
            FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        _animator.SetTrigger("FadeOut");
        levelToLoad = levelIndex;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void TheEnd()
    {
        _animator.SetTrigger("TheEnd");
    }

    public void Exit()
    {
        _database.SetLoadScene(12, slotPressed);
        SceneManager.LoadScene(0);
    }
}
