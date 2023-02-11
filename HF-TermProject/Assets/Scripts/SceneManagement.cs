using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1); // GameLevel
        AudioManager.amInstance.PlayAudio(TrackID.Castle);
    }

    public void ExitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}
