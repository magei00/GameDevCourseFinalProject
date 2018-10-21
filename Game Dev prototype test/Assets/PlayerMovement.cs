﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
	}

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+moveVelocity*Time.fixedDeltaTime);
    }
}
