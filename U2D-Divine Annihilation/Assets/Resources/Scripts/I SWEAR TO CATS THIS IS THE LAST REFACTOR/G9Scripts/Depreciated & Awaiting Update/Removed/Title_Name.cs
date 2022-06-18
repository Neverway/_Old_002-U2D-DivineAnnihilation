//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Allow the player to select a name for their save profile
// Applied to: Name menu object in the title scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Name : MonoBehaviour
{
    // Public Variable
    [Range(0, 2)]
    public int row = 2;
    [Range(0, 9)]
    public int column = 9;
    public int characterLimit = 12;
    public Text nameText;
    public Text row0;
    public Text row1;
    public Text row2;
    public Text controls;
    public Color selected;
    public Color unselected;
    public GameObject systemMessageScreen;
    public Text message;
    public Text actions;
    public bool active;
    public GameObject loadingScreen;

    // Private Variables
    private string nameTakenError = "That user is already active in the system!";
    private System_InputManager inputManager;
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        saveManager = FindObjectOfType<SaveManager>();
    }


    void Update()
    {
        controls.text = "[" + inputManager.controls["Interact"] + "] Select   [" + inputManager.controls["Action"] + "] Backspace   [" + inputManager.controls["Select"] + "] Clear";
        if (active)
        {
            if (row == 0)
            {
                if (Input.GetKeyDown(inputManager.controls["Up"])) { row = 2; }
                if (Input.GetKeyDown(inputManager.controls["Down"])) { row += 1; }
            }
            else if (row == 2)
            {
                if (Input.GetKeyDown(inputManager.controls["Up"])) { row -= 1; }
                if (Input.GetKeyDown(inputManager.controls["Down"])) { row = 0; }
            }
            else
            {
                if (Input.GetKeyDown(inputManager.controls["Up"])) { row -= 1; }
                if (Input.GetKeyDown(inputManager.controls["Down"])) { row += 1; }
            }


            if (column == 0)
            {
                if (Input.GetKeyDown(inputManager.controls["Left"])) { column = 9; }
                if (Input.GetKeyDown(inputManager.controls["Right"])) { column += 1; }
            }
            else if (column == 9)
            {
                if (Input.GetKeyDown(inputManager.controls["Left"])) { column -= 1; }
                if (Input.GetKeyDown(inputManager.controls["Right"])) { column = 0; }
            }
            else
            {
                if (Input.GetKeyDown(inputManager.controls["Left"])) { column -= 1; }
                if (Input.GetKeyDown(inputManager.controls["Right"])) { column += 1; }
            }
            UpdateKeys();
        }
    }

    public void UpdateKeys()
    {
        if (Input.GetKeyDown(inputManager.controls["Action"]) && nameText.text.Length != 0) { nameText.text = nameText.text.Remove(nameText.text.Length - 1); }
        if (Input.GetKeyDown(inputManager.controls["Select"]) && nameText.text.Length != 0) { nameText.text = ""; }
        if (row == 0) 
          {
                 // Deselect Rows 1 & 2
                row1.text = "   K   L   M   N   O   P   Q   R   S   T";
                row2.text = "   U   V   W   X   Y   Z   !   ?   _   OK";
                row0.color = selected;
                row1.color = unselected;
                row2.color = unselected;
                if (column == 0) { row0.text = " >A   B   C   D   E   F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "A"; } }
                if (column == 1) { row0.text = "   A >B   C   D   E   F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "B"; } }
                if (column == 2) { row0.text = "   A   B >C   D   E   F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "C"; } }
                if (column == 3) { row0.text = "   A   B   C >D   E   F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "D"; } }
                if (column == 4) { row0.text = "   A   B   C   D >E   F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "E"; } }
                if (column == 5) { row0.text = "   A   B   C   D   E >F   G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "F"; } }
                if (column == 6) { row0.text = "   A   B   C   D   E   F >G   H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "G"; } }
                if (column == 7) { row0.text = "   A   B   C   D   E   F   G >H   I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "H"; } }
                if (column == 8) { row0.text = "   A   B   C   D   E   F   G   H >I   J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "I"; } }
                if (column == 9) { row0.text = "   A   B   C   D   E   F   G   H   I >J"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "J"; } }
        }

          if (row == 1)
          {
                // Deselect Rows 0 & 2
                row0.text = "   A   B   C   D   E   F   G   H   I   J";
                row2.text = "   U   V   W   X   Y   Z   !   ?   _   OK";
                row0.color = unselected;
                row1.color = selected;
                row2.color = unselected;
                if (column == 0) { row1.text = " >K   L   M   N   O   P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "K"; } }
                if (column == 1) { row1.text = "   K >L   M   N   O   P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "L"; } }
                if (column == 2) { row1.text = "   K   L >M   N   O   P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "M"; } }
                if (column == 3) { row1.text = "   K   L   M >N   O   P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "N"; } }
                if (column == 4) { row1.text = "   K   L   M   N >O   P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "O"; } }
                if (column == 5) { row1.text = "   K   L   M   N   O >P   Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "P"; } }
                if (column == 6) { row1.text = "   K   L   M   N   O   P >Q   R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "Q"; } }
                if (column == 7) { row1.text = "   K   L   M   N   O   P   Q >R   S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "R"; } }
                if (column == 8) { row1.text = "   K   L   M   N   O   P   Q   R >S   T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "S"; } }
                if (column == 9) { row1.text = "   K   L   M   N   O   P   Q   R   S >T"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "T"; } }
        }

          if (row == 2)
          {
                // Deselect Rows 0 & 1
                row0.text = "   A   B   C   D   E   F   G   H   I   J";
                row1.text = "   K   L   M   N   O   P   Q   R   S   T";
                row0.color = unselected;
                row1.color = unselected;
                row2.color = selected;
                if (column == 0) { row2.text = " >U   V   W   X   Y   Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "U"; } }
                if (column == 1) { row2.text = "   U >V   W   X   Y   Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "V"; } }
                if (column == 2) { row2.text = "   U   V >W   X   Y   Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "W"; } }
                if (column == 3) { row2.text = "   U   V   W >X   Y   Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "X"; } }
                if (column == 4) { row2.text = "   U   V   W   X >Y   Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "Y"; } }
                if (column == 5) { row2.text = "   U   V   W   X   Y >Z   !   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "Z"; } }
                if (column == 6) { row2.text = "   U   V   W   X   Y   Z >!   ?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "!"; } }
                if (column == 7) { row2.text = "   U   V   W   X   Y   Z   ! >?   _   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "?"; } }
                if (column == 8) { row2.text = "   U   V   W   X   Y   Z   !   ? >_   OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { if (nameText.text.Length < characterLimit) nameText.text = nameText.text + "_"; } }
                if (column == 9) { row2.text = "   U   V   W   X   Y   Z   !   ?   _ >OK"; if (Input.GetKeyDown(inputManager.controls["Interact"])) { AttemptConfirm(); } }
         }
    }

    public void AttemptConfirm()
    {
        if (nameText.text == "DEV_TSTR!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in developer mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            active = false;
        }
        else if (nameText.text == "DEV_SRV_HST!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in server host mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            active = false;
        }
        else if (nameText.text == "DEV_SRV_CLI!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in server client mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            active = false;
        }
        else if (nameText.text == "MIYU")
        {
            systemMessageScreen.SetActive(true);
            message.text = nameTakenError;
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "SAM")
        {
            systemMessageScreen.SetActive(true);
            message.text = nameTakenError;
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "CASEY")
        {
            systemMessageScreen.SetActive(true);
            message.text = nameTakenError;
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "FOX_IS_DEAD")
        {
            systemMessageScreen.SetActive(true);
            message.text = "-_-";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "ZOOSMELL")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Try again, smartass!";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "POOPLORD")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Try again, smartass!";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else if (nameText.text == "")
        {
            systemMessageScreen.SetActive(true);
            message.text = "The name can't be blank!";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            active = false;
        }
        else
        {
            AcceptConfirm();
        }
    }

    public void AcceptConfirm()
    {
        PlayerPrefs.SetString("PlayerName", nameText.text);
        saveManager.CreateSave();
        saveManager.Save();
        loadingScreen.SetActive(true);
        saveManager.loadFileOnCreation = true;
        saveManager.activeSave.scene = "C1S1";
        SceneManager.LoadScene("C1S1");
        gameObject.SetActive(false);
    }
}
