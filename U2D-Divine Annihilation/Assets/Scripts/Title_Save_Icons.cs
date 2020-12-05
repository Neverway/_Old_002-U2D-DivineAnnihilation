//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Draw a portrait for the player character on the title screen
// Applied to: The Save menu on the title screen
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class TitleSaveIcons : MonoBehaviour
{
    public string saveID;
    public Image iconSprite;
    public Sprite saveBlankIcon;
    public Sprite saveHasIcon;

    void Update()
    {
        string dataPath = Application.persistentDataPath;
        if (!System.IO.File.Exists(dataPath + "/" + saveID + ".dasp"))
        {
            iconSprite.sprite = saveHasIcon;
        }

        else
        {
            iconSprite.sprite = saveBlankIcon;
        }
    }
}
