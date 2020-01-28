using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField]private bool canPause = false;
    private bool isPaused = false;

    private void Update()
    {
        HandleMenuInput();
    }

    private void HandleMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused && canPause)
            {
                Pause(panel);
            } else if (isPaused)
            {
                Unpause(panel);
            }   
        }
    }
    
    public void StartGame(string sceneName)
    {
        Time.timeScale = 1f;
        FindObjectOfType<Fader>().FadeTo(sceneName);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Pause(GameObject panel)
    {
        isPaused = true;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
    public void Unpause(GameObject panel)
    {
        isPaused = false;
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
