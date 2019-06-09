using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallAbility : IAbility
{

  public float maxDashDuration = 0.25f;
  public float dashSpeed = 7.0f;
  public float dashCooldown = 0.3f;

  private float dashTimer;
  private float cooldownTimer;
  private bool isDashing;
  


  private PlayerPlatformerController characterController;
  private Rigidbody2D rb;
  // Use this for initialization
  void Start()
  {
    characterController = GetComponent<PlayerPlatformerController>();
    rb = GetComponent<Rigidbody2D>();
    isDashing = false;
    dashTimer = maxDashDuration;
    cooldownTimer = 0.0f;
  }

  void Update()
  {

    if (cooldownTimer > 0.0f)
    {
      cooldownTimer -= Time.deltaTime;
    }
    if (!isDashing)
      return;
    if (dashTimer > 0.0f)
    {
      //Apply velocity for dashing
      Vector2 dashDirection = characterController.spriteRenderer.flipX ? Vector2.left : Vector2.right;
      //rb.velocity = dashDirection * dashSpeed;
      characterController.Movement(dashDirection * dashSpeed * Time.deltaTime, false);
      characterController.gravityModifier = 0.0f;
      dashTimer -= Time.deltaTime;
      isDashing = true;
    }
    else
    {
      //Stop dashing and start cooldown
      isDashing = false;
      characterController.gravityModifier = 2.0f;
      rb.velocity = new Vector2(0.0f, 0.0f);
      cooldownTimer = dashCooldown;
      dashTimer = maxDashDuration;
    }
  }



  override public void PerformAbility(PhysicsObject obj)
  {

    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerController>();
    //Check if cooldown is over
    if(cooldownTimer > 0.0f)
    {
      return;
    }
    isDashing = true;

  }

 

  public void OnCollisionStay2D(Collision2D collision)
  {
    if (isDashing && collision.gameObject.tag == "Breakable")
    {
      Destroy(collision.gameObject);
    }
    
  }
  
}
