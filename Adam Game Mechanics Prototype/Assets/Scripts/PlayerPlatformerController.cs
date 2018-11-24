using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {

public float maxSpeed = 7;
public float jumpTakeOffSpeed = 7;


public SpriteRenderer spriteRenderer;
public Animator animator;
private int characterIndex;
public Sprite adam;
public Sprite couch;

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
    
}


  protected override void Update()
  {
    base.Update();
    if (Input.GetKeyDown(KeyCode.F))
      abilities[characterIndex].PerformAbility(this);
  }



  protected override void ComputeVelocity()
{
    Vector2 move = Vector2.zero;
    move.x = Input.GetAxisRaw("Horizontal");
    if(Mathf.Abs(velocity.x)>0)
    {
        animator.SetFloat("speed", Mathf.Abs(move.x));
    }
    else
    {
        animator.SetFloat("speed", 0f);
    }

    if (Input.GetButtonDown("Jump") && grounded)
    {
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

    if(!grounded && characterIndex!=2)
    {
        animator.SetBool("is_jumping", true);
    }
    else
    {
        animator.SetBool("is_jumping", false);
    }

    bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.00f) : (move.x < 0.00f));
    if(flipSprite)
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
            jumpTakeOffSpeed = 14f;
            //spriteRenderer.sprite = adam;
            break;
        case 1:
            animator.SetInteger("character", 1);
            animator.SetTrigger("character_change");
            
            gravityModifier = 2f;
            jumpTakeOffSpeed = 7f;
            //spriteRenderer.sprite = couch;
            break;
        case 2:
            animator.SetInteger("character", 2);
            animator.SetTrigger("character_change");
            gravityModifier = -2f;
            //spriteRenderer.sprite = char3;
            break;
        default:
            break;
    }
}


public void kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
