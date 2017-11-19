using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttonPress(string buttonName)
    {
        if (buttonName == "Start")
        {
            SceneManager.LoadScene("Level1");

        }
        else if (buttonName == "Immersion")
        {
            SceneManager.LoadScene("Immersion");
        }
    }

}
