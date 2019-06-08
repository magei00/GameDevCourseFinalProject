using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text name_text;
    public Text dialogue_text;
    public Animator animator;
    public CutsceneManager cutsceneManager;


    private Queue<string> sentences;


	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("is_open", true);
        name_text.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();

            StartCoroutine(TypeSentence(sentence));
        }
      
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogue_text.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogue_text.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("is_open", false);
        cutsceneManager.EndCutscene();
    }
}
