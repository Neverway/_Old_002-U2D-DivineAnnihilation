//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Draw the text on the right side of the battle menu
// Applied to: Text Events object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Text_Events : MonoBehaviour
{
    private Hud_Textbox_Manager DialogueManager;
    [Header("Starting Text")]
    public string[] dialogueLinesStart;
    public string[] dialogueLineNamesStart;
    public Sprite[] dialogueLinePortraitsStart;

    [Header("Enemy Half-Health")]
    public string[] dialogueLinesHalf;
    public string[] dialogueLineNamesHalf;
    public Sprite[] dialogueLinePortraitsHalf;

    [Header("Victory Text")]
    public string[] dialogueLinesVictory;
    public string[] dialogueLineNamesVictory;
    public Sprite[] dialogueLinePortraitsVictory;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();   // Find the dialogue manager script

    }

    // Update is called once per frame
    void Update()
    {

        DialogueManager.EventTrigger = false;
        DialogueManager.EventActive = true;
        // Check if the dialogue box is already open// Check if the player has pressed the action key
        if (Input.GetKeyDown("b"))
        {
            if (!DialogueManager.dialogueBoxActive)
            {
                DialogueManager.dialogueLines = dialogueLinesStart;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                DialogueManager.dialogueLineNames = dialogueLineNamesStart;          // Pass the dialogue line names value to the manager
                DialogueManager.dialogueLinePortraits = dialogueLinePortraitsStart;  // Pass the dialogue line portraits value to the manager
                DialogueManager.currentLine = 0;                                // Reset the current line (in case the dialogue manager failes to)
                DialogueManager.ShowDialogue();                                 // Execute the show dialogue function
                DialogueManager.targetTrigger = gameObject;
                DialogueManager.destroyOnFinish = false;
            }
        }
    }
}
