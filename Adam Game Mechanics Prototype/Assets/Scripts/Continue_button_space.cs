using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue_button_space : MonoBehaviour
{

    public DialogueManager dm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            dm.DisplayNextSentence();
        }
    }
}
