using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInventoryItemAssignment : MonoBehaviour
{
    public Text[] inventoryItemName;
    public Text[] inventoryItemIcon;
    public Text[] inventoryEquipmentName;
    public Text[] inventoryEquipmentIcon;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
