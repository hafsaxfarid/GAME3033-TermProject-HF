using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    GameObject victoryPanel;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Time.timeScale = 0.0f;
            victoryPanel.SetActive(true);
        }
    }
}
