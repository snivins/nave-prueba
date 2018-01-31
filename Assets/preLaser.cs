using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class preLaser : NetworkBehaviour {
	private ParticleSystem mimi;
	private float timerino;
	public GameObject pewpew;
	public GameObject cdown;
	private bool disparando;
	public AudioClip lala;
	public AudioClip lalala;
	// Use this for initialization
	void Start () {
		//AudioSource.PlayClipAtPoint (lala, transform.position);
		//Invoke ("disparar",10f);
		pewpew = (GameObject)Instantiate (pewpew, transform.position, transform.rotation);
		pewpew.transform.parent = transform;
		//pewpew.SetActive (false);
		//gameObject.SetActive(false);
	}
	void mm(){
		AudioSource audio = GetComponent<AudioSource> ();
		audio.PlayOneShot (lala);
		//AudioSource.PlayClipAtPoint (lala, transform.position);
	}
	void OnEnable(){
			Invoke ("disparar", 10f);
			Invoke ("apagar", 13f);
			//Invoke ("mm", 0.1f);
	}
	void OnDisable(){
		
		CancelInvoke ();
	}
	void disparar() {
		//AudioSource.PlayClipAtPoint (lalala, transform.position);
	//	pewpew.SetActive (true);
	}
	void apagar() {
		gameObject.SetActive (false);
	}
}
