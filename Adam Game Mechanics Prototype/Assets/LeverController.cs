using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

  private bool off = false;

  public SecruityCameraController secruityCamera;



  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      secruityCamera.TurnOff();
    }
  }
}
