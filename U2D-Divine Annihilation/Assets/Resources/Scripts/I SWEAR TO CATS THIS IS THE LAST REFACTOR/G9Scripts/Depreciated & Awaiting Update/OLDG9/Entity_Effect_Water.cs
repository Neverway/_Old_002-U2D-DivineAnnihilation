using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Effect_Water : MonoBehaviour
{
    public GameObject waterFX;
    public GameObject[] entityNotWaterFX;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteMask>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void EnterWater()
    {
        foreach (var obj in entityNotWaterFX)
        {
            obj.SetActive(false);
            waterFX.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void ExitWater()
    {
        foreach (var obj in entityNotWaterFX)
        {
            obj.SetActive(true);
            waterFX.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
