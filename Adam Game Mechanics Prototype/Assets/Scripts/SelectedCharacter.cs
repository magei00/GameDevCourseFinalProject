using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour {

    private GameObject player;
    public GameObject selectedCharacter;
    
	// Use this for initialization
	void Start () {
      player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
      selectedCharacter.SetActive(true);
      Debug.Log("You pressed X");
      Time.timeScale = 0.25f;

      if (Input.GetButtonDown("Character1"))
      {
          player.GetComponent<PlayerPlatformerController>().switchChar(0);
          Debug.Log("char 1");
      }
      else if (Input.GetButtonDown("Character2"))
      {
          player.GetComponent<PlayerPlatformerController>().switchChar(1);
      }
        else if (Input.GetButtonDown("Character3"))
      {
          player.GetComponent<PlayerPlatformerController>().switchChar(2);
      }
    }
}
