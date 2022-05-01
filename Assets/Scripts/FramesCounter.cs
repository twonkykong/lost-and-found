using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramesCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Start()
    {
        InvokeRepeating("ShowFPS", 1, 0.5f);
    }

    private void ShowFPS()
    {
        float fps = (int)(1 / Time.unscaledDeltaTime);
        _text.text = "FPS: " + fps;
    }
}
