using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSaveIcons : MonoBehaviour
{
    public string saveID;
    public Image iconSprite;
    public Sprite saveBlankIcon;
    public Sprite saveHasIcon;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
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
