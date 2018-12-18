using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PhysicsObject {

  // stats. Move late to separate script
  public float baseMoveSpeed = 10.0f;
  public float visionRadius = 5.0f;
    private float moveSpeed;
    private float chaseMoveSpeed;

    public int bribeCost = 5;

    private Animator guard_animator;

    private int facingDir;

  private Transform target;
  private Vector3 spawnPosition;

  public Transform patrolPoint; // Maybe extend to a list later if more complexity is needed
  public Vector3 currentDestination;

  enum State { Patrolling, Waiting, Chasing, Bribed}
  private State currentState;

  public float maxChaseTime = 2.0f;
    private float chaseTimer;
    public float maxWaitTime = 2.5f;
    private float waitTimer;
    private SpriteRenderer spriteRenderer;
 
	// Use this for initialization
	void Start () {

        guard_animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = State.Patrolling;
        spawnPosition = transform.position;
        currentDestination = patrolPoint.position;
        waitTimer = maxWaitTime;
        moveSpeed = baseMoveSpeed;
        chaseMoveSpeed = baseMoveSpeed * 1.3f;
    }

    // Update is called once per frame
    override protected void Update () {

        


        switch (currentState)
    {
      case State.Patrolling:
        Patrol();
        break;
      case State.Chasing:
        Chase();
        break;
        case State.Waiting:
        Wait();
        break;
        case State.Bribed:
        Idle();
        break;
        default:
        break;
    }
  }

    private void Wait()
    {
        guard_animator.SetBool("is_alert", false);
        guard_animator.SetFloat("speed", 0f);
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0.0f)
        {
            currentState = State.Patrolling;
            waitTimer = maxWaitTime;
            return;
        };

        checkIfPlayerInFront();
    }

   

    private void Chase()
  {
    
    guard_animator.SetBool("is_alert", true);
    moveSpeed = chaseMoveSpeed;
    guard_animator.SetFloat("speed", moveSpeed);

    chaseTimer -= Time.deltaTime;
    if(chaseTimer <= 0.0f)
    {
      // Stop chasing and go back to patrolling
        currentState = State.Patrolling;
        guard_animator.SetBool("is_alert", false);
        chaseTimer = maxChaseTime;
        moveSpeed = baseMoveSpeed;
        guard_animator.SetFloat("speed", moveSpeed);
        return;
    }

    //Check if there is ground to walk on
    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(facingDir * 1, 0, 0), (transform.position + new Vector3(facingDir, -1, 0)) - (transform.position + new Vector3(facingDir * 1, 0, 0)), 1.0f);
    Debug.DrawLine(transform.position + new Vector3(facingDir * 1, 0, 0), transform.position + new Vector3(facingDir, -1, 0), Color.red);
    if (hit.collider == null)
    {
      // If there is no ground ahead, stop moving
        guard_animator.SetFloat("speed", 0f);
        return;
    }
    checkIfPlayerInFront();
    MoveTowards(target.position);


  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      GameControllerScript GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

            if (GC.GetCoins() >= bribeCost)
            {
                GC.DecrementCoin(bribeCost);
                GetComponent<CapsuleCollider2D>().enabled = false;
                gravityModifier = 0;
                currentState = State.Bribed;
                guard_animator.SetBool("is_alert", false);
                guard_animator.SetFloat("speed", 0f);


            }
            else
            {
                currentState = State.Chasing;
                chaseTimer = maxChaseTime;
                collision.gameObject.GetComponent<PlayerPlatformerController>().Kill();
            }
      
    }
  }

  private void Patrol()
  {
        guard_animator.SetBool("is_alert", false);
        guard_animator.SetFloat("speed", 1f);

        MoveTowards(currentDestination);
    checkIfPlayerInFront();

    if(currentDestination == spawnPosition && Vector2.Distance(transform.position, spawnPosition) < 0.2f)
    {
      Debug.Log("SWITCHING");
      currentDestination = patrolPoint.position;
      currentState = State.Waiting;
    }
    else if(currentDestination == patrolPoint.position && Vector2.Distance(transform.position, patrolPoint.position) < 0.2f)
    {
      Debug.Log("SWITCHING 2");
      currentDestination = spawnPosition;
      currentState = State.Waiting;
    }
  }

  private void Idle()
    {
        //do nothing
    }

    private void checkIfPlayerInFront()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position+ new Vector3(facingDir*1,0,0), new Vector2(facingDir,0), 5);
        //Debug.DrawLine(transform.position, transform.position + new Vector3(5*facingDir,0), Color.red);

        try
        {
            
            if ( hit.collider.gameObject.tag == "Player")
            {
                chaseTimer = maxChaseTime;
                currentState = State.Chasing;
            }
        }
        catch (NullReferenceException)
        {

        }


    }

    // Called each frame
    private void MoveTowards(Vector3 destination)
  {
    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
      new Vector2(destination.x, transform.position.y), moveSpeed * Time.deltaTime);

        float xdir= destination.x - transform.position.x;
        if (xdir > 0.00f) { facingDir = 1; };
        if (xdir < 0.00f) { facingDir = -1; };

        bool flipSprite = (spriteRenderer.flipX ? (xdir > 0.00f) : (xdir < 0.00f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
