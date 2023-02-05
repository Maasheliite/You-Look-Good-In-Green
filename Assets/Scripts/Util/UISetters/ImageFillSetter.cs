using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillSetter : MonoBehaviour
{
    public FloatReference currentValue;
    public FloatReference maxValue;
    public Image image;

    void Update()
    {
        image.fillAmount = Mathf.Clamp01(currentValue.Value / maxValue.Value);
    }
}
