using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector cutsceneTimeline;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneTimeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox" && !activated)
        {
            cutsceneTimeline.Play();
            activated = true;
        }
    }
}
