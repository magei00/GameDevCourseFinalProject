using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PhysicsObject {

  // stats. Move late to separate script
  public float baseMoveSpeed = 10.0f;
    private float moveSpeed;
    private float chaseMoveSpeed;



  private Transform target;
  private Vector3 spawnPosition;

  public Transform patrolPoint; // Maybe extend to a list later if more complexity is needed
  public Vector3 currentDestination;

  enum State { Patrolling, Waiting, Chasing}
  private State currentState;

  public float maxChaseTime = 2.0f;
    private float chaseTimer;
    public float maxWaitTime = 2.5f;
    private float waitTimer;
 
	// Use this for initialization
	void Start () {
    target = GameObject.FindGameObjectWithTag("Player").transform;
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
      default:
        break;
    }
  }

    private void Wait()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0.0f)
        {
            currentState = State.Patrolling;
            waitTimer = maxWaitTime;
            return;
        };

        checkPlayerDistance();
    }

   

    private void Chase()
  {
    moveSpeed = chaseMoveSpeed;
    chaseTimer -= Time.deltaTime;
    if(chaseTimer <= 0.0f)
    {
      currentState = State.Patrolling;
      chaseTimer = maxChaseTime;
      moveSpeed = baseMoveSpeed;
      return;
    }

    MoveTowards(target.position);


  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      currentState = State.Chasing;
      chaseTimer = maxChaseTime;
      collision.gameObject.GetComponent<PlayerPlatformerController>().kill();
    }
  }

  private void Patrol()
  {
    MoveTowards(currentDestination);
    checkPlayerDistance();

    if(currentDestination == spawnPosition && Vector2.Distance(transform.position, spawnPosition) < 1.5f)
    {
      Debug.Log("SWITCHING");
      currentDestination = patrolPoint.position;
      currentState = State.Waiting;
    }
    else if(currentDestination == patrolPoint.position && Vector2.Distance(transform.position, patrolPoint.position) < 1.5f)
    {
      Debug.Log("SWITCHING 2");
      currentDestination = spawnPosition;
      currentState = State.Waiting;
    }
  }

    private void checkPlayerDistance()
    {
        if (Vector2.Distance(transform.position, target.position) < 1f)
        {
            currentState = State.Chasing;
        }
    }

    // Called each frame
    private void MoveTowards(Vector3 destination)
  {
    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
      new Vector2(destination.x, transform.position.y), moveSpeed * Time.deltaTime);
  }
}
