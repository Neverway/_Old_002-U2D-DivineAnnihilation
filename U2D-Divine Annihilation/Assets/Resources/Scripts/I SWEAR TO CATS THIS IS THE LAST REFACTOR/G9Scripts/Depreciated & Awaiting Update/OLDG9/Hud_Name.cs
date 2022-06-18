
using UnityEngine;
using UnityEngine.UI;

public class Hud_Name : MonoBehaviour
{
    // Public Variables
    public Text nameObject;

    // Private Variables
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    void Update()
    {
        nameObject.text = saveManager.activeSave.playerName;
    }
}
