using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
