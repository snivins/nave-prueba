using UnityEngine;
using System.Collections;

public class CiclularProgress : MonoBehaviour {

	private bool disparando = false;
	private float full_disparo;
	
	// Use this for initialization
	void Start () {
		//Use this to Start progress

	}
	void update () {
		if (Input.GetButtonDown ("Fire1")) {
			StartCoroutine(RadialProgressfull());
			full_disparo = Time.time + 15.0f;
			disparando = true;
		}
		if (Time.time > full_disparo) {
			disparando = false;
		}
	}
	
	IEnumerator RadialProgressfull()
	{
		
		float rate = 1 / 5.0f;
		float i = 0;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			yield return 0;
		}

		StartCoroutine (RadialProgressflat ());
	}	
	IEnumerator RadialProgressflat()
	{
		float rate = 1 / 10.0f;
		float i = 1;
		while (i > 0)
		{
			i -= Time.deltaTime * rate;
			gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			yield return 0;
		}
	}
}