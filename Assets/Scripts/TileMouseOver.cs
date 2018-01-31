using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseOver : MonoBehaviour {
	TileMap _tileMap;
	Vector3 currentTileCoord;
	public Transform selectionCube;
	// Use this for initialization
	void Start () {
		_tileMap = GetComponent<TileMap> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000)) {
			int x = Mathf.FloorToInt (hit.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt (hit.point.z / _tileMap.tileSize); 
			Debug.Log ("point x:" +x +", " +z);

			currentTileCoord.x = x;
			currentTileCoord.z = z;

			selectionCube.transform.position = currentTileCoord * _tileMap.tileSize;
		} else {

		}
	}
}
