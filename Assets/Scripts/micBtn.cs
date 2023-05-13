using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class micBtn : MonoBehaviour
{
    public Sprite Open;
    public Sprite Close;
    public Button btn;
    public GameObject go;

    private bool isOpen;

    void Start()
    {
        btn.image.sprite = Open;
        isOpen = false;
    }

    public void OnChatButtonPress()
    {
    }


}
