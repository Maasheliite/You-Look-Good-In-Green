using UnityEngine;
using UnityEngine.UI;

public class TMPTextAndIconSetter : MonoBehaviour
{
    public FloatReference variable;
    public Sprite img;

    public TMPro.TMP_Text text;
    public Image imageElement;
    void Update()
    {
        text.text = variable?.Value.ToString();
        imageElement.sprite = img;
    }
}
