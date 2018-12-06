using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutScene : MonoBehaviour {

    // Use this for initialization
    public CutsceneManager manager;
    public PlayerPlatformerController playerController;
    public Animator NPC_animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerController.grounded)
        {
            playerController.enabled = false;
            manager.StartCutscene();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && playerController.grounded)
        {
            playerController.enabled = false;
            manager.StartCutscene();
        }
    }
}
