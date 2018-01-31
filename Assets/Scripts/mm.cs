using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mm : MonoBehaviour {
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void salir () {
		Application.Quit();
	}
	public void demo () {
		SceneManager.LoadScene ("Main",LoadSceneMode.Single);
	}
}
