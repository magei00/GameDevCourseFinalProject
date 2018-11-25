using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityAbilityB : IAbility {

  private PlayerPlatformerControllerB characterController;
	// Use this for initialization
	void Start () {
        characterController = GetComponent<PlayerPlatformerControllerB>();
	}

  override public void PerformAbility(PhysicsObject obj)
  {
    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerControllerB>();
      obj.gravityModifier *= -1;
      characterController.spriteRenderer.flipY = !characterController.spriteRenderer.flipY;
  }
}
