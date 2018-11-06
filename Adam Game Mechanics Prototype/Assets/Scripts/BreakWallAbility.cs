﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallAbility : IAbility {

  public float abilityLength = 1.0f;


  private PlayerPlatformerController characterController;
	// Use this for initialization
	void Start () {
    characterController = GetComponent<PlayerPlatformerController>();
	}
	

  override public void PerformAbility(MonoBehaviour obj)
  {
    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerController>();
      Debug.Log("PRESSED");
      Vector2 startPos = new Vector2(0.0f, obj.transform.position.y);
    Debug.Log(characterController.spriteRenderer);
      startPos.x = characterController.spriteRenderer.flipX ? obj.transform.position.x + 1.25f : obj.transform.position.x - 1.25f;
      Vector2 dir = characterController.spriteRenderer.flipX ? new Vector2(1.0f, 0.0f) : new Vector2(-1.0f, 0.0f);
      RaycastHit2D hit = Physics2D.Raycast(startPos, -dir, abilityLength);
      Debug.Log(dir);
      Debug.DrawRay(startPos, dir, Color.black, abilityLength, false);
      if (hit && hit.collider.gameObject.tag == "BreakableWall")
      {
        Debug.Log("HIT!");
        Destroy(hit.collider.gameObject);
      }

  }

}