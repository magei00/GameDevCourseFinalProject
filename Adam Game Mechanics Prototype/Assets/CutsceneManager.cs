using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {

    public Animator cinema_animator;
    public Animator player_animator;
    public DialogueManager dialogueManager;
    public Animator NPC_animator;
    public GameObject NPC;
    public int newCharacterIndex = 0;

    public Image dash_image;
    public Image gravity_image;


    public Sprite character1sprite;

    public GameObject Player;
    public PlayerPlatformerController playerController;

    public GameObject cutsceneTrigger;

    public Dialogue dialogue;


    public void StartCutscene()
    {
        NPC_animator.SetBool("is_walking", true);
        cinema_animator.SetBool("is_on", true);
        
        //set the animation of the player to idle
        player_animator.SetFloat("speed", 0f);
        player_animator.SetBool("is_jumping", false);


        dialogueManager.StartDialogue(dialogue);
    }

    public void EndCutscene()
    {
        cutsceneTrigger.SetActive(false);
        //should probably be in a cutscene manager
        cinema_animator.SetBool("is_on", false);
        NPC_animator.SetBool("is_walking", false);
        NPC_animator.enabled = false;

        //Change the sprite of the NPC to the main character
        SpriteRenderer NPCsprite = NPC.GetComponent<SpriteRenderer>();
        NPCsprite.enabled = false;

        //Player.transform.position = NPCpos;
        GameControllerScript gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        switch (newCharacterIndex)
        {
            case 1:
                gameController.dash_unlocked = true;
                dash_image.color = Color.white;
                break;
            case 2:
                gameController.gravity_unlocked = true;
                gravity_image.color = Color.white;
                break;
            default:
                break;
        }

        playerController.enabled = true;

    }

}
