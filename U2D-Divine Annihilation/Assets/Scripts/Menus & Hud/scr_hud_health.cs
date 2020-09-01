using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_hud_health : MonoBehaviour
{
    public RectTransform healthBar;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, 8);
    }
}
