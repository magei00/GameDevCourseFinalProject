using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

public float maxSpeed = 7;
public float jumpTakeOffSpeed = 7;

public SpriteRenderer spriteRenderer;
private Animator animator;
private int characterIndex;
public Sprite adam;
public Sprite couch;

public IAbility[] abilities = new IAbility[4];
  
void Awake()
{
    characterIndex = 0;
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    abilities = new IAbility[4];
    abilities[0] = new EmptyAbility();
    abilities[1] = new BreakWallAbility();


        
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
    Debug.Log("Velocity");
    move.x = Input.GetAxis("Horizontal");

    if (Input.GetButtonDown("Jump") && grounded)
    {
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

    bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.00f) : (move.x < 0.00f));
    if(flipSprite)
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    animator.SetBool("grounded", grounded);
    animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

    targetVelocity = move * maxSpeed;
}

public void switchChar(int i)
{
    characterIndex = i;
    switch (i)
    {
        case 0:
            gravityModifier = 2f;
            jumpTakeOffSpeed = 7;
            spriteRenderer.sprite = adam;
            break;
        case 1:
            gravityModifier = 0.7f;
            jumpTakeOffSpeed = 0;
            spriteRenderer.sprite = couch;
                
            break;
        default:
            break;
    }
        

}
}
