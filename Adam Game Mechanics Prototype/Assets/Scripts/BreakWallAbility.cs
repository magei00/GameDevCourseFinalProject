using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallAbility : IAbility
{

  public float maxDashDuration = 0.25f;
  public float dashSpeed = 10.0f;
  public float dashCooldown = 1.0f;

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
      Debug.Log("Reducing cooldown");
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
      Vector2 dashDirection = characterController.spriteRenderer.flipX ? Vector2.left : Vector2.right;
      //rb.velocity = dashDirection * dashSpeed;
      characterController.Movement(dashDirection * dashSpeed * Time.deltaTime, false);
      characterController.gravityModifier = 0.0f;
      Debug.Log("DASHING");
      dashTimer -= Time.deltaTime;
      isDashing = true;
    }
    else
    {
      isDashing = false;
      characterController.gravityModifier = 2.0f;
      //rb.velocity = new Vector2(0.0f, 0.0f);
      Debug.Log("Stopping");
      cooldownTimer = dashCooldown;
      dashTimer = maxDashDuration;
    }
  }



  override public void PerformAbility(PhysicsObject obj)
  {

    if (!characterController)
      characterController = obj.GetComponent<PlayerPlatformerController>();
    if(cooldownTimer > 0.0f)
    {
      Debug.Log("COOLDOWN");
      return;
    }
    isDashing = true;

  }

  public void OnCollisionEnter2D(Collision2D other)
  {
    if(isDashing && other.gameObject.tag == "Breakable")
    {
      Destroy(other.gameObject);
    }
    Debug.Log("DASHING HIIIIIIIIIIIIT");
  }
}
