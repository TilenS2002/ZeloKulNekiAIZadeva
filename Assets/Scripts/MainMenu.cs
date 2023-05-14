using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    private void Start() {
        foreach (var device in Microphone.devices)
        {
            Debug.Log(device);
            dropdown.options.Add(new Dropdown.OptionData(device));
        }
        dropdown.onValueChanged.AddListener(ChangeMicrophone);
        var index = PlayerPrefs.GetInt("user-mic-device-index");
        dropdown.SetValueWithoutNotify(index);
    }

    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }

    public void call() {
        SceneManager.LoadScene("SampleScene");
    }

    public void quit() {
        Application.Quit();
    }
}
