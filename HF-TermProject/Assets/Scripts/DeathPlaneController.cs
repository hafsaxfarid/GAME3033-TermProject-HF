using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Time.timeScale = 0.0f;
            gameOverPanel.SetActive(true);
        }
    }
}
