using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minGroundNormalY = 0.65f; 
    public float gravityModifier = 2f;
    public bool grounded; //is the player grounded or not?
    public Vector2 groundNormal;

    protected Vector2 targetVelocity; //horizontal, where is our object going to move?
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;

    //array to store results of what we hit
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //minimum distance to check whether we are going to hit something
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


	// Use this for initialization
	void Start () {
        //we're not going to check collisions against triggers
        contactFilter.useTriggers = false;

        //we're getting a layer mask from the project settings of physics 2d. 
        //use the settings to determine what layers we're gonna check collisions against
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        targetVelocity = Vector2.zero; //so we're not using target velocity from the pervious frame.
        ComputeVelocity();
	}

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        
        //Move object down every frame because of gravity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //horizontal movement
        //Debug.Log(gravityModifier);
        velocity.x = targetVelocity.x;

        //we first test the movement on the X-axis

        //until a collision is registered, we're not grounded
        grounded = false;

        //change in position
        Vector2 deltaPosition = velocity * Time.deltaTime;

        //we're picturing a vector along the ground
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        //now test vertical movement

        //apply this velocity to the object in order to move it

        //use that for movement
        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    public void Movement(Vector2 move, bool yMovement)
    {
        //check if were are going to hit something
        float distance = move.magnitude;

        //check if distance is greater than some minimal value
        if(distance > minMoveDistance)
        {
            //otherwise we're not going to check for collisions
            //Lets check if our rigid body is going to overlap with something in the next frame

            //we want to add some padding to the distance (a shell) to allow us to make sure we never get stuck in another collider
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear(); //lets make sure we're not using old data

            for(int i = 0; i<count; i++)
            {
        //each entry from the array is going to get copied to the list
              PlatformEffector2D platform = hitBuffer[i].collider.GetComponent<PlatformEffector2D>();
                if (!platform || (hitBuffer[i].normal == Vector2.up && velocity.y <= 0))
                {
                  hitBufferList.Add(hitBuffer[i]);
                }
            }
            //now we have a list of objects that are going to overlap our physics object's collider. 
        
            //check the normal of each of those objects to determine the angle of the thing we're colliding with
            for(int i=0; i<hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(gravityModifier >= 0 && currentNormal.y > minGroundNormalY)
                {
                    //if the angle of the obj we're colliding with can be considered a piece of ground
                    grounded = true;
                    if(yMovement)
                    {
                        //add a variable for our ground normal
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }

                }
                else if (gravityModifier <= 0 && currentNormal.y < minGroundNormalY)
                {
                  //if the angle of the obj we're colliding with can be considered a piece of ground
                  grounded = true;
                  if (yMovement)
                  {
                    //add a variable for our ground normal
                    groundNormal = -currentNormal;
                    currentNormal.x = 0;
                  }

                }
        //getting the diff between the velo and  the cunormal and determining whether we need to subtract
        //from the velocity to make sure the player doesnt get stuck in something

        //hit their head on a sloped ceiling and continue a little bit
        float projection = Vector2.Dot(velocity, currentNormal);
                if(projection<0)
                {
                    //cancel out the velocity that would be stopped by the collision
                    velocity = velocity - projection * currentNormal;

                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }


        }
        //rb2d.position = rb2d.position + move.normalized * distance;
        transform.position = transform.position + new Vector3(move.normalized[0], move.normalized[1], 0) * distance;
    }
}
