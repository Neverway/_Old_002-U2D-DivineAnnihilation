//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Starts a cutscene event upon collision
// Applied to: Cutscene trigger
//
//=============================================================================

using UnityEngine;
using UnityEngine.Playables;

public class Trigger_Cutscene : MonoBehaviour
{
    public PlayableDirector cutsceneTimeline;
    private bool activated;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        //cutsceneTimeline = GetComponent<PlayableDirector>();
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
