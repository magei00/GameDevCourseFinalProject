using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecruityCameraController : MonoBehaviour {

  private Vector3[] vertices;
  private Color[] colors;
  private int[] indices;
  private MeshFilter spotLightMeshF;
  private Mesh spotLightMesh;
  private Transform spotLightTransform;
  private bool isOn = true;
  private LayerMask mask;
  private float maxDistance = 100; // Workaround
  private GameObject player;

  public float fov = 90;
  public float maxRotAngle = 45; // maximum cameraRotation in degrees
  public float rotationSpeed = 5; //Degrees per second
  

	// Use this for initialization
	void Start () {
    player = GameObject.FindGameObjectWithTag("Player");
    mask = ~(1 << LayerMask.NameToLayer("Player"));
    spotLightTransform = GetComponentsInChildren<Transform>()[1];
    spotLightMeshF = GetComponentInChildren<MeshFilter>();
    // Setting up the graphics
    vertices = new Vector3[] {spotLightTransform.position, new Vector3(3, 1, 0), new Vector3(-3, 1, 0) };
    colors = new Color[] { Color.yellow, Color.yellow, Color.yellow};
    indices = new int[]{ 0,1,2};
    

    spotLightMesh = new Mesh
    {
      vertices = vertices,
      triangles = indices,
      colors = colors
    };

    spotLightMesh.RecalculateNormals();
    spotLightMesh.RecalculateBounds();

    spotLightMeshF.mesh = spotLightMesh;
	}
	
	// Update is called once per frame
	void Update () {
    if (isOn)
    {
      //Rotate the camera
      if(transform.rotation.eulerAngles.z > maxRotAngle && transform.rotation.eulerAngles.z < 360-maxRotAngle)
      {
        rotationSpeed = -rotationSpeed;
      }
      Vector3 newRot = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
      spotLightTransform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime)); //TODO Fix with matrices
      //Draw the updated spotlight
      ShineSpotLight();

    }

  }

  //Refactor into two functions
  void ShineSpotLight()
  {

    //Check for player (Right now check angle)
    RaycastHit2D playerHit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
    if(playerHit.collider != null && playerHit.collider.tag == "Player" && Mathf.Abs(Vector3.Angle(-transform.up, player.transform.position - transform.position)) < fov / 2)
    {
      
      playerHit.collider.gameObject.GetComponent<PlayerPlatformerController>().Kill(); // Change to gameover or later to guards chasing you
    }
      


    //TODO Change to use transformation matrices


    Vector2 leftBound = new Vector3(0, 0, 0);
    float cos = Mathf.Cos((-fov / 2) * Mathf.Deg2Rad);
    float sin = Mathf.Sin((-fov / 2) * Mathf.Deg2Rad);
    leftBound.x = (cos * -transform.up.x) - (sin * -transform.up.y);
    leftBound.y = (sin * -transform.up.x) + (cos * -transform.up.y);
    Vector3[] nVertices = new Vector3[] { spotLightTransform.position - transform.position, new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    RaycastHit2D hit = Physics2D.Raycast(spotLightTransform.position, leftBound, maxDistance, mask);
    if (hit.collider != null && hit.collider != gameObject)
    {
      Debug.DrawLine(spotLightTransform.position, hit.point);
      Vector3 leftSide = hit.point - new Vector2(transform.position.x, transform.position.y);
      nVertices[1] = leftSide;
    }

    Vector2 rightBound = new Vector3(0, 0, 0);
    cos = Mathf.Cos((fov / 2) * Mathf.Deg2Rad);
    sin = Mathf.Sin((fov / 2) * Mathf.Deg2Rad);
    rightBound.x = (cos * -transform.up.x) - (sin * -transform.up.y);
    rightBound.y = (sin * -transform.up.x) + (cos * -transform.up.y);
    hit = Physics2D.Raycast(spotLightTransform.position, rightBound, maxDistance, mask);
    if (hit && hit.collider != gameObject && hit.collider.tag != "Player")
    {
      Debug.DrawLine(spotLightTransform.position, hit.point);
      //Debug.Log(hit.collider.tag);
      Vector3 rightSide = hit.point - new Vector2(transform.position.x, transform.position.y);
      nVertices[2] = rightSide;
    }
    spotLightMesh.vertices = nVertices;

    spotLightMesh.RecalculateNormals();
    spotLightMesh.RecalculateBounds();

    spotLightMeshF.mesh = spotLightMesh;
  }

  public void TurnOff()
  {
    isOn = false;
    spotLightMeshF.mesh = null;
  }

}
