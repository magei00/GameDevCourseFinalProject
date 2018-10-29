using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeAbility : MonoBehaviour {

  private CharacterController2D characterController;
  private PlayerMovement playerMovement;

  private bool timeSlowed;

	// Use this for initialization
	void Start () {
    characterController = GetComponent<CharacterController2D>();
    playerMovement = GetComponent<PlayerMovement>();
    timeSlowed = false;
	}


  private void Update()
  {
    if(Input.GetKeyDown(KeyCode.E))
    {
      if(!timeSlowed)
      {
        timeSlowed = true;
        SlowTime();
      }

    }
    else if(timeSlowed)
    {
      //timeSlowed = false;
      //SpeedUp();
    }
  }

  void SlowTime()
  {    
    Time.timeScale = 0.1f;
    characterController.m_JumpForce *= 2;
    playerMovement.runSpeed *= 2.0f;
  }

  void SpeedUp()
  {
    Time.timeScale = 1.0f;
    characterController.m_JumpForce /= 2;
    playerMovement.runSpeed /= 2.0f;
    
  }
}
