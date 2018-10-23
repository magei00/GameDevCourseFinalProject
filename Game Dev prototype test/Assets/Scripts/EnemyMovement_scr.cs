using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_scr : MonoBehaviour {

    public float baseSpeed;
    public float slowSpeedMult;
    public float aggroRadius;
    private Transform playerPos;
    private float speed;

	// Use this for initialization
	void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector2.Distance( transform.position, playerPos.position) < aggroRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        }
        speed = baseSpeed;





    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Goo"))
        {
            //other.gameObject.SetActive(false);
            speed = baseSpeed * slowSpeedMult;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
       
    }
}
