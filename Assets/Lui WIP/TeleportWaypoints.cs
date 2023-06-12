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


    private void Start()
    {
        // Add an onClick listener to each destination button
        for (int i = 0; i < destinationButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the lambda expression
            destinationButtons[i].onClick.AddListener(() => TeleportToWaypoint(index));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && PlayerMovement.CanTeleport)
        {
            if (MapActive)
            {
                MapCanvas.SetActive(false);
                MapActive = false;
            }

            else
            {
                MapCanvas.SetActive(true);
                MapActive = true;
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
