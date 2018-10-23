using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterMenu : MonoBehaviour {

    public GameObject switchMenu;
    public PauseMenu pauseMenu;
    private bool GameIsPaused;
    

	// Use this for initialization
	void Start () {
        GameIsPaused = pauseMenu.GameIsPaused;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Switch") && !pauseMenu.GameIsPaused)
        {
            switchMenu.SetActive(true);
            Debug.Log("You pressed X");
            Time.timeScale = 0.25f;
        }
        else
        {
            switchMenu.SetActive(false);
            Debug.Log("You released X");

            Time.timeScale = 1f;

        }
    }

    public void SwitchCharacter()
    {

    }
}
