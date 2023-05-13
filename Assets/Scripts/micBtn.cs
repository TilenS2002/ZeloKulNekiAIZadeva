using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class micBtn : MonoBehaviour
{
    int n;
    public Sprite micOn;
    public Sprite micOff;
    public Sprite micMuted;
    public Button btn;

    void Start()
    {

    }

    public void OnButtonPress()
    {
        if (btn.image.sprite == micOn)
            btn.image.sprite = micMuted;
        else
        {
            btn.image.sprite = micOn;
        }
    }
}