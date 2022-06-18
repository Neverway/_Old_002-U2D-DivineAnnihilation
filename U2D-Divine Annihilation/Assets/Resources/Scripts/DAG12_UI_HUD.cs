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
using Cinemachine;

public class DAG12_UI_HUD : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public string characterName;
    public Sprite characterIcon;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    [SerializeField] private Text HUDName;
    [SerializeField] private Image HUDIcon;


    //=-----------------=
    // Mono Functions
    private void Start()
    {
        HUDName.text = characterName;
        HUDIcon.sprite = characterIcon;
    }

    private void Update()
    {
	
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void SetCameraIdleNoise(float amplitude)
    {
        gameObject.transform.GetChild(0).gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        CinemachineVirtualCamera vcam;
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }
}
