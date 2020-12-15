//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Set the health bar image to equal the current health
// Applied to: The health hud sprite in a scene
//
//=============================================================================

using UnityEngine;

public class Hud_Health : MonoBehaviour
{
    public RectTransform healthBar;
    public float currentHealth;
    private SaveManager saveManager;

    void Start()
    {
        healthBar = transform.GetComponent<RectTransform>();
        saveManager = FindObjectOfType<SaveManager>();
        currentHealth = saveManager.activeSave.playerHealth;
    }


    void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, 8);
        saveManager.activeSave.playerHealth = currentHealth;
    }
}
