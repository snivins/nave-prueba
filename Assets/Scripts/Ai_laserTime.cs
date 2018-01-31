using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_laserTime : MonoBehaviour {
	LineRenderer line;
	public GameObject explosion;
	private bool disparando = false;
	private float full_disparo;
	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}
	public void shoot ()
	{
		if (!disparando) {
			StopCoroutine ("FireLaser");
			full_disparo = Time.time + 15.0f;
			disparando = true;
			StartCoroutine ("FireLaser");
		}
		if (Time.time > full_disparo) {
			disparando = false;
		}
	}

	IEnumerator FireLaser()
	{
		line.enabled = true;
		float m_width = 0.0f;
		float destroy_time =  5.0f;
		bool hiterino = false;
		while (Time.time < full_disparo) {
			line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, Time.time);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			float actual_time = full_disparo - Time.time;
			if (actual_time >= 10.0f)
				line.widthMultiplier = 0.1f;
			if (actual_time < 10.0f && actual_time > 9.2f)
				line.widthMultiplier += 0.25f;
			if (actual_time < 0.8f && line.widthMultiplier >0.01f)
				line.widthMultiplier -= 0.5f;
			line.SetPosition (0, ray.origin);

			if (Physics.Raycast (ray, out hit, 1000) && actual_time < 9.6f && actual_time > 0.5f) {
				line.SetPosition (1, hit.point);
				if (hit.rigidbody) {
					hit.rigidbody.AddForceAtPosition (transform.forward * 50, hit.point);
					if (!hiterino) {
						destroy_time = Time.time + 5.0f;
						hiterino = true;
					}
					if (Time.time > destroy_time) {
						Destroy (hit.rigidbody.gameObject);
						Instantiate (explosion, hit.rigidbody.transform.position,hit.rigidbody.transform.rotation);
					}
				} else
					hiterino = false;
			}
			else		
				line.SetPosition (1, ray.GetPoint(1000));

			yield return null;
		}

		line.enabled = false;
	}

}

