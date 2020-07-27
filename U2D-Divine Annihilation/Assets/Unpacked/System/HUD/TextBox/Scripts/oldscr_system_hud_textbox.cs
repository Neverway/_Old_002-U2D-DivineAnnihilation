using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oldscr_system_hud_textbox : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public bool playerInRange;
    public string sentance;

    // Trigger input
    public List<dialog> Dialog;

    [System.Serializable]
    public class dialog
    {
        public Sprite Portraits;
        public string Name;
        public string Sentances;
        public int Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z") && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(TypeSentance(sentance));
            }
        }
        IEnumerator TypeSentance(string sentance)
        {
            dialogText.text = "";
            foreach(char letter in sentance.ToCharArray())
            {
                dialogText.text += letter;
                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
