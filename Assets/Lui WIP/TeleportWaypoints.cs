using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportWaypoints : MonoBehaviour
{
    public GameObject player;
    public Button[] destinationButtons;
    public Transform[] waypoints;

    public GameObject MapCanvas;
    private bool MapActive;

    public GameObject UICanvas;
    public static bool PlayerInRange = false;

    private void Start()
    {
        // Add an onClick listener to each destination button
        for (int i = 0; i < destinationButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the lambda expression
            destinationButtons[i].onClick.AddListener(() => TeleportToWaypoint(index));
        }
        MapCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && PlayerMovement.CanTeleport && PlayerInRange)
        {
            if (MapActive)
            {
                MapCanvas.SetActive(false);
                MapActive = false;
                UICanvas.SetActive(true);
            }

            else
            {
                MapCanvas.SetActive(true);
                MapActive = true;
                UICanvas.SetActive(false);
            }
        }
    }

    private void TeleportToWaypoint(int index)
    {
        if (index >= 0 && index < waypoints.Length)
        {
            player.transform.position = waypoints[index].position;
        }
    }

    
}
