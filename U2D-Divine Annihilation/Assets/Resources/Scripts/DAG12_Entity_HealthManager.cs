//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAG12_Entity_HealthManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public float maxHealth = 100;
    public float invulnerabilityDuration = 2.5f; // How long will the entity be invulnerable for before it can take damage again
    public bool useDamageMask = true; // Play damage and invulnerable animations (requires damageMask)


    //=-----------------=
    // Private variables
    //=-----------------=
    [Header("Read-Only Variables")] 
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private bool invulnerable;
    [SerializeField] private bool dead; // Is the player in the process of dying


    //=-----------------=
    // Reference variables
    //=-----------------=
    [Header("Refernce Variables")] 
    [SerializeField] private Animator damageMask;
    [SerializeField] private RectTransform healthBar; // This is only really ever used for the players hud
    private DAG12_System_TransitionManager transitionManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        transitionManager = FindObjectOfType<DAG12_System_TransitionManager>();
        currentHealth = maxHealth;
    }

    IEnumerator Invulnerabity()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        if (useDamageMask)
        {
            if (damageMask != null) { damageMask.Play("Idle"); }
            else { Debug.LogWarning("The DamageMwask has not been assigned! Please assign one, or disable UseDamageMask!"); }
        }
        invulnerable = false;
    }

    private void Update()
    {
        
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void Die()
    {
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<NUPTopdownController>().canMove = false;
            gameObject.GetComponent<NUPTopdownController>().SetNewMovement(0, 0, 0);
            gameObject.GetComponent<Animator>().Play("Knockout");
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            //menuManager.alternateMenuActive = true;
            transitionManager.TransitionGameover();
            dead = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("Knockout");
        }
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Heal(float amount)
    {

    }

    public void TakeDamage(float amount)
    {
        if (!invulnerable && !dead)
        {
            // Take damage and play damage animation (If there is one)
            currentHealth -= amount;
            if (healthBar != null) { healthBar.sizeDelta = new Vector2 (100 * (currentHealth/maxHealth), healthBar.sizeDelta.y); }
            if (useDamageMask)
            {
                if (damageMask != null) { damageMask.Play("Damage"); }
                else { Debug.LogWarning("The DamageMwask has not been assigned! Please assign one, or disable UseDamageMask!"); }
            }

            // Check to see if this amount of damage kills the entity
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Invulnerabity());
            }
        }
    }
}
