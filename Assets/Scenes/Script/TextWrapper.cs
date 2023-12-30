using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextWrapper : MonoBehaviour
{
    private Text _text;

    public void SetText(string msg)
    {
        _text.text = msg;
    }
}
