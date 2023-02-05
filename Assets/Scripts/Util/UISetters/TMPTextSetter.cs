using UnityEngine;

public class TMPTextSetter : MonoBehaviour
{
    public FloatReference variable;

    public TMPro.TMP_Text text;
    void Update()
    {
        text.text = variable?.Value.ToString();
    }
}
