using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityAbility : IAbility
{

  private PlayerPlatformerController characterController;
  public int charges = 1;
  private float groundedTolerance = 0.07f;
  private float groundedToleranceTimer;
  // Use this for initialization
  void Start()
  {
    characterController = GetComponent<PlayerPlatformerController>();
    groundedToleranceTimer = groundedTolerance;
  }

  override public void PerformAbility(PhysicsObject obj)
  {
    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerController>();
    if (charges > 0)
    {
      obj.gravityModifier *= -1;
      obj.targetVelocity.y = 0;
      obj.velocity.y = 0;
      characterController.spriteRenderer.flipY = !characterController.spriteRenderer.flipY;
      charges--;
      Debug.Log("GRAVITY");
    }



  }

  private void Update()
  {
    if(characterController.grounded)
    {
      if (groundedToleranceTimer <= 0.0f)
      {
        if(charges == 0)
          charges = 1;
        groundedToleranceTimer = groundedTolerance;
      }
      else
      {
        groundedToleranceTimer -= Time.deltaTime;
      }
    }
    else
    {
      groundedToleranceTimer = groundedTolerance;
    }
  }
}
