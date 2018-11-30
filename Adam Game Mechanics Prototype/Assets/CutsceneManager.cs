using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public Animator cinema_animator;
    public Animator player_animator;
    public DialogueManager dialogueManager;
    public Animator NPC_animator;
    public GameObject NPC;

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
        //Now we want to shift to Dash and use him.
        player_animator.SetTrigger("character_change");
        player_animator.SetInteger("character", 1);

        //Change the position of the two
        Vector3 playerPos = Player.transform.position;
        Vector3 NPCpos = NPC.transform.position;
        NPC.transform.position = Player.transform.position;

        //Change the sprite of the NPC
        SpriteRenderer NPCsprite = NPC.GetComponent<SpriteRenderer>();
        NPCsprite.sprite = character1sprite;

        Player.transform.position = NPCpos;
        playerController.jumpTakeOffSpeed = 8f;
        //Player.transform.position = NPCpos;

        playerController.enabled = true;

    }

}
