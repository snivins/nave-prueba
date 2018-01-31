using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	private Rigidbody rb;
	public GameObject camera;
	public GameObject enemy_selected;
    public float camera_speed;
	public float speed;
	public Text texto;
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire;
    public float fireRate;

	public GameObject follower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera_speed = 100;
        speed = 10;
        Screen.lockCursor = true;

		float x_dif = transform.position.x;
		for (int i = 0; i < 10; i++) {
			x_dif += 20.0f;
			//primera linea
			Vector3 pos = new Vector3 (transform.position.x + x_dif, transform.position.y, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y + 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y - 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			pos = new Vector3 (transform.position.x - x_dif, transform.position.y, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif, transform.position.y + 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif, transform.position.y - 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			//segunda linea
			pos = new Vector3 (transform.position.x + x_dif - 10.0f, transform.position.y + 10.0f, transform.position.z - 10.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif - 10.0f, transform.position.y - 10.0f, transform.position.z - 10.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			pos = new Vector3 (transform.position.x - x_dif + 10.0f, transform.position.y + 10.0f, transform.position.z - 10.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif + 10.0f, transform.position.y - 10.0f, transform.position.z - 10.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
		}    

    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        
        if (Input.GetKey("q"))
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, 0.5f));
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        if (Input.GetKey("e"))
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, -0.5f));
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        
        Vector3 mov = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddRelativeForce(mov * speed);

        float moveRotacionX = Input.GetAxis("Mouse X");
        float moveRotacionY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(new Vector3 (0.0f, moveRotacionX,0.0f), Time.deltaTime * camera_speed,Space.Self);
        transform.Rotate(new Vector3 (-moveRotacionY,0.0f, 0.0f), Time.deltaTime * camera_speed,Space.Self);
    }
    
    
    
    
    
    
    
    
    
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
                Instantiate (shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
        }      */  
		float distancia = Vector3.Distance(enemy_selected.transform.position, transform.position);
		texto.text = "Enemy at " + distancia.ToString("F2") + " km";
	}
}
