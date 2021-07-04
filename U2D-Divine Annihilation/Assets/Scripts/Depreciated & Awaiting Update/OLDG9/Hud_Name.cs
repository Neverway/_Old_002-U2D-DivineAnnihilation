
using UnityEngine;
using UnityEngine.UI;

public class Hud_Name : MonoBehaviour
{
    // Public Variables
    public Text nameObject;

    // Private Variables
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    void Update()
    {
        nameObject.text = saveManager.activeSave.playerName;
    }
}
