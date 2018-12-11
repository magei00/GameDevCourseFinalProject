using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCharacterMenu : MonoBehaviour {

    private GameObject player;
    private GameObject switchMenu;
    public PauseMenu pauseMenu;
    private bool GameIsPaused;
    private GameControllerScript gameController;

    public Animator effect_animator;
    

	// Use this for initialization
	void Start () {
        GameIsPaused = pauseMenu.GameIsPaused;
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
	}

    void ChangeArrowKeyColor(int charIndex) {
        GameObject.Find("Character1Button").GetComponent<Button>().image.color = charIndex == 1 ? Color.yellow : Color.white;
        GameObject.Find("Character2Button").GetComponent<Button>().image.color = charIndex == 2 ? Color.yellow : Color.white;
        GameObject.Find("Character3Button").GetComponent<Button>().image.color = charIndex == 3 ? Color.yellow : Color.white;
        GameObject.Find("Character4Button").GetComponent<Button>().image.color = charIndex == 4 ? Color.yellow : Color.white;
    }
	
	// Update is called once per frame
	void Update () {
        // switchMenu.SetActive(true);
    if(!gameController)
      gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    if ((Input.GetButton("Character1") || Input.GetButton("Character2") || Input.GetButton("Character3")) && !pauseMenu.GameIsPaused)
        {

            if (Input.GetButtonDown("Character1") && gameController.GetCurrentPlayerIndex() != 1)
            {
                ChangeArrowKeyColor(1);
                player.GetComponent<PlayerPlatformerController>().switchChar(0);

                effect_animator.SetTrigger("change_trigger");

                gameController.SetCurrentPlayerIndex(1);

            }
            else if (Input.GetButtonDown("Character2") && gameController.IsCharacterUnlocked(1) && gameController.GetCurrentPlayerIndex() != 2)
            {
                ChangeArrowKeyColor(2);
                player.GetComponent<PlayerPlatformerController>().switchChar(1);
 
                effect_animator.SetTrigger("change_trigger");

                gameController.SetCurrentPlayerIndex(2);


            }
            else if (Input.GetButtonDown("Character3") && gameController.IsCharacterUnlocked(2) && gameController.GetCurrentPlayerIndex() != 3)
            {

                ChangeArrowKeyColor(3);
                player.GetComponent<PlayerPlatformerController>().switchChar(2);

                effect_animator.SetTrigger("change_trigger");

                gameController.SetCurrentPlayerIndex(3);


            }
            Time.timeScale = 1f;
        }
    }
}
