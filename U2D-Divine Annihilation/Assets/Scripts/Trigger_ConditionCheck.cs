using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_ConditionCheck : MonoBehaviour
{
    public string itemName;
    public bool checkDoesNotHave;
    public UnityEvent onConditionsMet;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!checkDoesNotHave)
            {
                if (saveManager.activeSave.item1 == itemName || saveManager.activeSave.item2 == itemName || saveManager.activeSave.item3 == itemName || saveManager.activeSave.item4 == itemName || saveManager.activeSave.item5 == itemName || saveManager.activeSave.equipment1 == itemName || saveManager.activeSave.equipment2 == itemName || saveManager.activeSave.equipment3 == itemName || saveManager.activeSave.equipment4 == itemName || saveManager.activeSave.equipment5 == itemName)
                {
                    onConditionsMet.Invoke();
                }
            }

            if (checkDoesNotHave)
            {
                if (saveManager.activeSave.item1 != itemName && saveManager.activeSave.item2 != itemName && saveManager.activeSave.item3 != itemName && saveManager.activeSave.item4 != itemName && saveManager.activeSave.item5 != itemName && saveManager.activeSave.equipment1 == itemName && saveManager.activeSave.equipment2 == itemName && saveManager.activeSave.equipment3 == itemName && saveManager.activeSave.equipment4 == itemName && saveManager.activeSave.equipment5 == itemName)
                {
                    onConditionsMet.Invoke();
                }
            }
        }
    }
}
