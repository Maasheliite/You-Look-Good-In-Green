using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Camera main;
    public Camera mapCam;
    public GameObject MapOverlay;
    public GameObject[] mapObjects;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            main.gameObject.SetActive(false);
            mapCam.gameObject.SetActive(true);
            MapOverlay.SetActive(true);
            foreach (GameObject obj in mapObjects)
            {
                obj.SetActive(true);
            }

        }


        if (Input.GetKeyUp(KeyCode.Tab))
        {
            main.gameObject.SetActive(true);
            mapCam.gameObject.SetActive(false);
            MapOverlay.SetActive(false);
            foreach (GameObject obj in mapObjects)
            {
                obj.SetActive(false);
            }
        }
    }



}
