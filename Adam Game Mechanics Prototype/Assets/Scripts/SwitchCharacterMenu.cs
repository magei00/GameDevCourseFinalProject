using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCharacterMenu : MonoBehaviour {

    private GameObject player;
    private GameObject switchMenu;
    public PauseMenu pauseMenu;
    private bool GameIsPaused;
    

	// Use this for initialization
	void Start () {
        GameIsPaused = pauseMenu.GameIsPaused;
        player = GameObject.FindGameObjectWithTag("Player");
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
		if((Input.GetButton("Character1") || Input.GetButton("Character2") || Input.GetButton("Character3")) && !pauseMenu.GameIsPaused)
        {
            Debug.Log("You pressed X");
            Time.timeScale = 0.25f;

            if (Input.GetButtonDown("Character1"))
            {
                ChangeArrowKeyColor(1);
                player.GetComponent<PlayerPlatformerController>().switchChar(0);
            }
            else if (Input.GetButtonDown("Character2"))
            {
                ChangeArrowKeyColor(2);
                player.GetComponent<PlayerPlatformerController>().switchChar(1);
            }
             else if (Input.GetButtonDown("Character3"))
            {
                ChangeArrowKeyColor(3);
                player.GetComponent<PlayerPlatformerController>().switchChar(2);
            }
            Time.timeScale = 1f;
        }
    }
}
