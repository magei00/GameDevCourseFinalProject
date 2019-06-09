using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue_button_space : MonoBehaviour
{
    public Animator cinema_animator;
    public DialogueManager dm;
    private float timer=0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cinema_animator.GetBool("is_on"))
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }

        if (Input.GetButtonDown("Jump") && timer>5.0f)
        {
            
            dm.DisplayNextSentence();
        }
    }
}
