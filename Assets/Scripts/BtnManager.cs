using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public Sprite Open;
    public Sprite Close;
    public Button btn;
    public GameObject go;
    public float x;
    public float y;

    private bool isOpen;
    private RectTransform rt;

    void Start()
    {
        rt = go.GetComponent<RectTransform>();
        go.SetActive(true);
        btn.image.sprite = Open;
        isOpen = false;
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 1100, 0);
    }

    public void OnChatButtonPress()
    {
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, isOpen  ? 1100 : 725, 300);
        btn.image.sprite = isOpen ? Open : Close;
        isOpen = !isOpen;
    }
    public void OnEndCall()
    {
        Application.Quit();
        Debug.Log("bye");
    }

    public void disc() {
        SceneManager.LoadScene("menu");
    }
}
