﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OTU_Title_KeyBind : MonoBehaviour
{
    public Text[] controlTextObjects;
    public GameObject bindingScreen;
    public UnityEvent onFinishKeybind;

    private GameObject currentKey;
    private OTU_System_InputManager inputManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        controlTextObjects[0].text = inputManager.controls["Up"].ToString();
        controlTextObjects[1].text = inputManager.controls["Down"].ToString();
        controlTextObjects[2].text = inputManager.controls["Left"].ToString();
        controlTextObjects[3].text = inputManager.controls["Right"].ToString();
        controlTextObjects[4].text = inputManager.controls["Interact"].ToString();
        controlTextObjects[5].text = inputManager.controls["Action"].ToString();
        controlTextObjects[6].text = inputManager.controls["Select"].ToString();
        controlTextObjects[7].text = inputManager.controls["Menu"].ToString();
        controlTextObjects[8].text = inputManager.controls["Special 1"].ToString();
        controlTextObjects[9].text = inputManager.controls["Special 2"].ToString();
        controlTextObjects[10].text = inputManager.controls["Special 3"].ToString();
        controlTextObjects[11].text = inputManager.controls["Special 4"].ToString();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f);
        onFinishKeybind.Invoke();
    }

    public void ChangeKey(GameObject passKey)
    {
        bindingScreen.SetActive(true);
        currentKey = passKey;
        passKey.transform.GetChild(1).GetComponent<Text>().text = "...";
    }

    void OnGUI()
    {
        if (currentKey != null && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            inputManager.controlsBuffered[currentKey.name + "Buffered"] = Event.current.keyCode;
            //SaveKeys();
            bindingScreen.SetActive(false);
            StartCoroutine("Delay");
            currentKey.transform.GetChild(1).GetComponent<Text>().text = Event.current.keyCode.ToString();
            currentKey = null;
        }
    }

    public void SaveKeys()
    {
        inputManager.controls["Up"] = inputManager.controlsBuffered["UpBuffered"];
        inputManager.controls["Down"] = inputManager.controlsBuffered["DownBuffered"];
        inputManager.controls["Left"] = inputManager.controlsBuffered["LeftBuffered"];
        inputManager.controls["Right"] = inputManager.controlsBuffered["RightBuffered"];
        inputManager.controls["Interact"] = inputManager.controlsBuffered["InteractBuffered"];
        inputManager.controls["Action"] = inputManager.controlsBuffered["ActionBuffered"];
        inputManager.controls["Select"] = inputManager.controlsBuffered["SelectBuffered"];
        inputManager.controls["Menu"] = inputManager.controlsBuffered["MenuBuffered"];
        inputManager.controls["Special 1"] = inputManager.controlsBuffered["Special 1Buffered"];
        inputManager.controls["Special 2"] = inputManager.controlsBuffered["Special 2Buffered"];
        inputManager.controls["Special 3"] = inputManager.controlsBuffered["Special 3Buffered"];
        inputManager.controls["Special 4"] = inputManager.controlsBuffered["Special 4Buffered"];
        foreach (var key in inputManager.controls)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

}
