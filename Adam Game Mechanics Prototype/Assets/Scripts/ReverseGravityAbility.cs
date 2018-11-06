using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityAbility : IAbility {

  private PlayerPlatformerController characterController;
	// Use this for initialization
	void Start () {
    characterController = GetComponent<PlayerPlatformerController>();
	}

  override public void PerformAbility(PhysicsObject obj)
  {
    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerController>();
      obj.gravityModifier *= -1;
      characterController.spriteRenderer.flipY = !characterController.spriteRenderer.flipY;
  }
}
