using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnclockables : MonoBehaviour {

    public GameControllerScript gameControllerScript;
    bool dash;
    bool gravity;
    bool fourth;

    public GameObject dash_image;
    public GameObject gravity_image;
    public GameObject fourth_image;


	// Use this for initialization
	void Start () {
        Debug.Log(gameControllerScript.IsCharacterUnlocked(1));
        Debug.Log(gameControllerScript.IsCharacterUnlocked(2));
        Debug.Log(gameControllerScript.IsCharacterUnlocked(3));
        if (gameControllerScript.IsCharacterUnlocked(1))
        {
            dash_image.GetComponent<Image>().color = Color.white;
        }
        else
        {
            dash_image.GetComponent<Image>().color = Color.black;
        }
        if (gameControllerScript.IsCharacterUnlocked(2))
        {
            gravity_image.GetComponent<Image>().color = Color.white;
        }
        else
        {
            gravity_image.GetComponent<Image>().color = Color.black;
        }
        if (gameControllerScript.IsCharacterUnlocked(3))
        {
            fourth_image.GetComponent<Image>().color = Color.white;
        }
        else
        {
            fourth_image.GetComponent<Image>().color = Color.black;
        }
    }
}
