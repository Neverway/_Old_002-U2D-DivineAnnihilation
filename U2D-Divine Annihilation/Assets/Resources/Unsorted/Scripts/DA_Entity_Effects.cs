//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Give effects to an entity
// Applied to: An entity object
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;

public class DA_Entity_Effects : MonoBehaviour
{
    // Public variables
    [Header("Effect Materials")]
    public Material water;
    public Material lava;
    public Material snow;
    public Material sand;

    [Header ("Effect Heights")]
    public float waterHeight = 0.405f;
    public float lavaHeight = 0.405f;
    public float lightSnowHeight = 0.08f;
    public float heavySnowHeight = 0.25f;
    public float sandHeight = 0.02f;

    public SpriteRenderer entityShadow;
    public SpriteRenderer effectMask;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Entity Shadow")
            {
                entityShadow = child.GetComponent<SpriteRenderer>();
            }
            if (child.tag == "Effect Mask")
            {
                effectMask = child.GetComponent<SpriteRenderer>();
            }
        }
    }

    void Update()
    {
        gameObject.GetComponent<SpriteMask>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void EnterWater()
    {
        entityShadow.enabled = false;
        effectMask.enabled = true;
        effectMask.material = water;
        effectMask.transform.localScale = new Vector2(effectMask.transform.localScale.x, waterHeight);

    }

    public void EnterLava()
    {
        entityShadow.enabled = false;
        effectMask.enabled = true;
        effectMask.material = lava;
        effectMask.transform.localScale = new Vector2(effectMask.transform.localScale.x, lavaHeight);
    }

    public void EnterSnow(bool heavySnow)
    {
        entityShadow.enabled = false;
        effectMask.enabled = true;
        effectMask.material = snow;
        if (heavySnow)
        {
            effectMask.transform.localScale = new Vector2(effectMask.transform.localScale.x, heavySnowHeight);
        }
        else
        {
            effectMask.transform.localScale = new Vector2(effectMask.transform.localScale.x, lightSnowHeight);
        }
    }

    public void EnterSand()
    {
        entityShadow.enabled = false;
        effectMask.enabled = true;
        effectMask.material = sand;
        effectMask.transform.localScale = new Vector2(effectMask.transform.localScale.x, sandHeight);
    }

    public void ExitEffectVolume()
    {
        entityShadow.enabled = true;
        effectMask.enabled = false;
    }
}
