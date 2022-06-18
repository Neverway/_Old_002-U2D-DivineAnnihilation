using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_BookID : MonoBehaviour
{
    public Hud_SI_object_Snapzone snapzone1;
    public Hud_SI_object_Snapzone snapzone2;
    public Hud_SI_object_Snapzone snapzone3;
    public GameObject targetTrigger;
    public string[] book1Text;
    public string[] book2Text;
    public string[] book3Text;
    public string[] book4Text;
    public string[] book5Text;
    public string[] book6Text;
    [Header ("These are assigned to during gameplay and should stay blank. -UCC")]
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}
    void Update()
    {
        book1 = snapzone1.target;
        book2 = snapzone2.target;
        book3 = snapzone3.target;

        // Assign book text for the first book
        if (snapzone1.target.name == "SI Pickup 1")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book1Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book1Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book1Text[2];
        }

        else if (snapzone1.target.name == "SI Pickup 2")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book2Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book2Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book2Text[2];
        }

        else if (snapzone1.target.name == "SI Pickup 3")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book3Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book3Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book3Text[2];
        }

        else if (snapzone1.target.name == "SI Pickup 4")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book4Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book4Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book4Text[2];
        }

        else if (snapzone1.target.name == "SI Pickup 5")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book5Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book5Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book5Text[2];
        }

        else if (snapzone1.target.name == "SI Pickup 6")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book6Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book6Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book6Text[2];
        }

        // Assign book text for the second book
        if (snapzone2.target.name == "SI Pickup 1")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book1Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book1Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book1Text[2];
        }

        else if (snapzone2.target.name == "SI Pickup 2")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book2Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book2Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book2Text[2];
        }

        else if (snapzone2.target.name == "SI Pickup 3")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book3Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book3Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book3Text[2];
        }

        else if (snapzone2.target.name == "SI Pickup 4")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book4Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book4Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book4Text[2];
        }

        else if (snapzone2.target.name == "SI Pickup 5")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book5Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book5Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book5Text[2];
        }

        else if (snapzone2.target.name == "SI Pickup 6")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[3] = book6Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[4] = book6Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[5] = book6Text[2];
        }

        // Assign book text for the third book
        if (snapzone3.target.name == "SI Pickup 1")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[6] = book1Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[7] = book1Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[8] = book1Text[2];
        }

        else if (snapzone3.target.name == "SI Pickup 2")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[6] = book2Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[7] = book2Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[8] = book2Text[2];
        }

        else if (snapzone3.target.name == "SI Pickup 3")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[6] = book3Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[7] = book3Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[8] = book3Text[2];
        }

        else if (snapzone3.target.name == "SI Pickup 4")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[6] = book4Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[7] = book4Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[8] = book4Text[2];
        }

        else if (snapzone3.target.name == "SI Pickup 5")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[0] = book5Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[1] = book5Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[2] = book5Text[2];
        }

        else if (snapzone3.target.name == "SI Pickup 6")
        {
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[6] = book6Text[0];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[7] = book6Text[1];
            targetTrigger.GetComponent<Trigger_Interact>().dialogueLines[8] = book6Text[2];
        }
    }
}
