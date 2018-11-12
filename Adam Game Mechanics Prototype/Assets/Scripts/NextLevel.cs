using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    private bool NextLevelButton = false;
    public Canvas canvas;
    Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

	// Use this for initialization
	void Start () {
        animator = canvas.GetComponent<Animator>();

    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("hæ");
            FadeToLevel(5);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        Debug.Log("FadeToLevel");
        animator.SetTrigger("FadeOut");
    }
	
}
