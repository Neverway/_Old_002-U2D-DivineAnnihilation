using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudHealth : MonoBehaviour
{
    public RectTransform healthBar;
    public float currentHealth;
    public GameObject configTarget;
    private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetComponent<RectTransform>();
        saveManager = configTarget.GetComponent<SaveManager>();
        currentHealth = saveManager.activeSave.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, 8);
        saveManager.activeSave.playerHealth = currentHealth;
    }
}
