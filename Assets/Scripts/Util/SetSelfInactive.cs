using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelfInactive : MonoBehaviour
{
    public float timerMax;
    private float timer;
    public void display()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            gameObject.SetActive(false);
        }
    }
}
