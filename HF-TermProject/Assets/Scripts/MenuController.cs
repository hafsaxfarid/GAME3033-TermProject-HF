using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Transform startPoint;
    [SerializeField]
    GameObject victoryPanel, gameOverPanel;
    public void Respawn(GameObject go)
    {
        go.transform.position = startPoint.position;
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
