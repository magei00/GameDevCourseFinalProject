using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour {

    public GameObject doorToOpen;
    Animator doorAnimator;


    void Awake()
    {
        doorAnimator = doorToOpen.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered the trigger");
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("key_collected", true);
            Debug.Log("Player enter");
            OpenDoor();
            Destroy(gameObject);
        }
    }

    void OpenDoor()
    {
        BoxCollider2D boxCollider = doorToOpen.GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }
}
