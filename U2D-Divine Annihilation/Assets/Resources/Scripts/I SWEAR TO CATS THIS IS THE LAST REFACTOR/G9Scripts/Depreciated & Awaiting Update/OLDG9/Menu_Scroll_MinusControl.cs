//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through menu objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Menu_Scroll_MinusControl : MonoBehaviour
{
    public Sprite[] frames;
    public Image spriteRenderer;
    public bool horizontalScrolling;
    public bool wrapAround;
    public bool canGoBack;
    public int currentFrame;
    private System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }

    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling)
        {
            if (Input.GetKeyDown(inputManager.controls["Up"]))
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = frames.Length; // Up arrow wrap around
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                }
            }
            if (Input.GetKeyDown(inputManager.controls["Down"]))
            {
                if (currentFrame == frames.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                }

                if (currentFrame != frames.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                }
            }
        }

        // Horizontal scrolling
        else if (horizontalScrolling)
        {
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                currentFrame += 1;
            }

            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                currentFrame -= 1;
            }
        }

        // Draw current image
        spriteRenderer.sprite = frames[currentFrame];
    }
}
