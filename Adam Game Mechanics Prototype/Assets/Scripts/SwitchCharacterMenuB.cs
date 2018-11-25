using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterMenuB : MonoBehaviour
{

  private GameObject currentPlayer;
  public GameObject[] players = new GameObject[3];
  public GameObject switchMenu;
  public PauseMenu pauseMenu;
  private bool GameIsPaused;

  public GameObject mainCamera;


  // Use this for initialization
  void Start()
  {
    GameIsPaused = pauseMenu.GameIsPaused;
    currentPlayer = players[0];
    Debug.Log(players.Length);
  }

  // Update is called once per frame
  void Update()
  {
    if ((Input.GetButton("Character1") || Input.GetButton("Character2") || Input.GetButton("Character3")) && !pauseMenu.GameIsPaused)
    {
      switchMenu.SetActive(true);
      //Debug.Log("You pressed X");
      Time.timeScale = 0.25f;
      
      if (Input.GetButtonDown("Character1"))
      {
        Debug.Log("CHANGES1");
        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = false;
        currentPlayer = players[0];
        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = true;

      }
      else if (Input.GetButtonDown("Character2"))
      {
        Debug.Log("CHANGES2");
        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = false;
        currentPlayer = players[1];
        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = true;
      }
      else if (Input.GetButtonDown("Character3"))
      {
        Debug.Log("CHANGES3");
        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = false;
        currentPlayer = players[2];

        currentPlayer.GetComponent<PlayerPlatformerControllerB>().isCurrentPlayer = true;
      }
    }
    else
    {
      switchMenu.SetActive(false);

      Time.timeScale = 1f;

    }
  }

  public void SwitchCharacter()
  {

  }


}
