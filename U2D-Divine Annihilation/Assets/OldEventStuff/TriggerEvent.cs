using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject targetEntity;
    public GameObject eventEntity;
    public Animator eventEntityAnimator;
    public bool ToAnimation;
    public bool swapActiveOnFinish;
    public GameObject[] newActives;
    private bool triggered;
    public bool animationFinished;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Vector2.Distance(targetEntity.transform.position, eventEntity.transform.position) <= 1 && ToAnimation && triggered)
        {
            eventEntityAnimator.SetFloat("animationSpeed", 1f);
        }
        if (animationFinished && swapActiveOnFinish)
        {
            foreach (var obj in newActives)
                if (obj.activeInHierarchy == true)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
                }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            triggered = true;
            targetEntity.GetComponent<CharacterFollower>().target = eventEntity.transform;
        }
    }
}
