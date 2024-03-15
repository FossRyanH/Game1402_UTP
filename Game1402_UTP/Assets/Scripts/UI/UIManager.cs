using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;

    public string startScene;
    public string restartScene;
    public bool IsGamePaused = false;

    //start a new game
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    //set game time back to normal and load MainScene
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(restartScene);
    }

    public void HandlePauseResume()
    {
        if (!IsGamePaused)
            Pause();
        else
            Resume();
    }

    //freeze game time, bring up pauseMenuUI, bring out statisticsUI change GameIsPaused to true
    public void Pause()
    {
        IsGamePaused = true;

        pauseMenuUI.SetActive(true);

        //pause time
        Time.timeScale = 0f;

        //enables the cursor and turns it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //set game time back to normal, bring out pauseMenuUI, bring up statisticsUI, change GameIsPaused to false
    public void Resume()
    {
        IsGamePaused = false;

        pauseMenuUI.SetActive(false);

        //resume time
        Time.timeScale = 1f;

        //locks the cursor and turns it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //quit the game, load MainMenu
    public void Quit()
    {
        //resume time
        Time.timeScale = 1f;

        //Application.Quit(); // Shut down the running application. The 'Application.Quit' call is ignored in the Editor.
        SceneManager.LoadScene("MainMenu"); // Quit to Main Menu.
    }
}
