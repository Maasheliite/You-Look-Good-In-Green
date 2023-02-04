using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public GameObject MapOverlay;
    public GameObject Frame;
    public GameObject Circle;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MapOverlay.SetActive(true);
            Frame.SetActive(true);
            Circle.SetActive(true);

        }



        if (Input.GetKeyUp(KeyCode.Tab))
        {

            MapOverlay.SetActive(false);
            Frame.SetActive(false);
            Circle.SetActive(false);
        }
    }



}
