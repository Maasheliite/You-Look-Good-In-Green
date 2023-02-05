using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPTextCurrentAndMaxSetter : MonoBehaviour
{
    public FloatReference currentValue;
    public FloatReference maxValue;
    public TMPro.TMP_Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = currentValue + " / " + maxValue;
    }
}
