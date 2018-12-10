using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject
{

  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;

  public SpriteRenderer spriteRenderer;
  public Animator animator;
  private int characterIndex;
  public Sprite adam;
  public Sprite couch;
  public int charges = 1;
  private float groundedTolerance = 0.07f;
  private float groundedToleranceTimer;
  private GameControllerScript gameController;

  public GameObject effectObject;
  private Animator effectAnimator;

  public Sprite char3;

  public IAbility[] abilities = new IAbility[4];

  void Awake()
  {
    Application.targetFrameRate = 300;
    characterIndex = 0;
    spriteRenderer = GetComponent<SpriteRenderer>();
    abilities[0] = GetComponent<EmptyAbility>();
    abilities[1] = GetComponent<BreakWallAbility>();
    abilities[2] = GetComponent<ReverseGravityAbility>();

    effectAnimator = effectObject.GetComponent<Animator>();
    gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    GameObject.Find("Character1Button").GetComponent<Button>().image.color = Color.yellow;
  }

  protected override void Update()
  {
    base.Update();
    if (grounded)
    {
      if (groundedToleranceTimer <= 0.0f)
      {
        if (charges == 0)
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

    if (Input.GetButtonDown("Ability"))
    {
      abilities[characterIndex].PerformAbility(this);
    }


    if (Input.GetKeyDown("r"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      gameController.ResetCoins();
    }
  }

  protected override void ComputeVelocity()
  {
    Vector2 move = Vector2.zero;
    move.x = Input.GetAxisRaw("Horizontal");
    if (Mathf.Abs(velocity.x) > 0)
    {
      animator.SetFloat("speed", Mathf.Abs(move.x));
    }
    else
    {
      animator.SetFloat("speed", 0f);
    }

    if (Input.GetButtonDown("Jump") && (grounded || charges > 0))
    {
      if (!grounded)
        charges--;
      animator.SetBool("is_jumping", true);
      velocity.y = jumpTakeOffSpeed;
    }
    else if (Input.GetButtonUp("Jump"))
    {
      //cancel jump
      if (velocity.y > 0)
      {
        //we're moving upwards
        velocity.y = velocity.y * 0.5f;
      }
    }

    if (!grounded && characterIndex != 2)
    {
      animator.SetBool("is_jumping", true);
    }
    else
    {
      animator.SetBool("is_jumping", false);
    }

    bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.00f) : (move.x < 0.00f));
    if (flipSprite)
    {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    //animator.SetBool("grounded", grounded);
    //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

    targetVelocity = move * maxSpeed;
  }

  public void switchChar(int i)
  {
    //if we're changing from 2 and the 
    //characterController.spriteRenderer.flipY = true then we need to flip the sprite

    characterIndex = i;
    effectAnimator.SetTrigger("change_trigger");

    switch (i)
    {
      case 0:
        animator.SetInteger("character", 0);
        animator.SetTrigger("character_change");
        gravityModifier = 2f;
        jumpTakeOffSpeed = 11f;
        spriteRenderer.flipY = false;
        //spriteRenderer.sprite = adam;
        break;
      case 1:
        animator.SetInteger("character", 1);
        animator.SetTrigger("character_change");
        spriteRenderer.flipY = false;
        gravityModifier = 2f;
        jumpTakeOffSpeed = 8f;
        //spriteRenderer.sprite = couch;
        break;
      case 2:
        animator.SetInteger("character", 2);
        animator.SetTrigger("character_change");
        jumpTakeOffSpeed = 8f;
        spriteRenderer.flipY = true;
        //spriteRenderer.sprite = char3;
        break;
      default:
        break;
    }
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Coin")
    {
      Destroy(other.gameObject);
      gameController.IncrementCoin(1);
    }

    if (other.CompareTag("Door"))
    {
      gameController.SaveCoins();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }
  public void Kill()
  {
    gameController.ResetCoins();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
