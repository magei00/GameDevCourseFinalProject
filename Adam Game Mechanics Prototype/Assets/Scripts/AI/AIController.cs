using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

  // stats. Move late to separate script
  public float moveSpeed = 10.0f;




  private Transform target;
  private Vector3 spawnPosition;

  public Transform patrolPoint; // Maybe extend to a list later if more complexity is needed
  public Vector3 currentDestination;

  enum State { Patrolling, Chasing}
  private State currentState;

  public float maxChaseTime = 2.0f;
  private float chaseTimer;
	// Use this for initialization
	void Start () {
    target = GameObject.FindGameObjectWithTag("Player").transform;
    currentState = State.Patrolling;
    spawnPosition = transform.position;
    currentDestination = patrolPoint.position;
	}
	
	// Update is called once per frame
	void Update () {




    switch (currentState)
    {
      case State.Patrolling:
        Patrol();
        break;
      case State.Chasing:
        Chase();
        break;
      default:
        break;
    }
  }

  private void Chase()
  {
    chaseTimer -= Time.deltaTime;
    if(chaseTimer <= 0.0f)
    {
      currentState = State.Patrolling;
      chaseTimer = maxChaseTime;
      return;
    }

    MoveTowards(target.position);


  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player" && currentState != State.Chasing)
    {
      currentState = State.Chasing;
      chaseTimer = maxChaseTime;
    }
  }

  private void Patrol()
  {
    MoveTowards(currentDestination);

    if(currentDestination == spawnPosition && Vector2.Distance(transform.position, spawnPosition) < 1.5f)
    {
      Debug.Log("SWITCHING");
      currentDestination = patrolPoint.position;
    }
    else if(currentDestination == patrolPoint.position && Vector2.Distance(transform.position, patrolPoint.position) < 1.5f)
    {
      Debug.Log("SWITCHING 2");
      currentDestination = spawnPosition;
    }
  }

  // Called each frame
  private void MoveTowards(Vector3 destination)
  {
    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
      new Vector2(destination.x, transform.position.y), moveSpeed * Time.deltaTime);
  }
}
