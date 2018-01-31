using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linea1px : MonoBehaviour {

	public Material mat;
	private Vector3 startVertex;
	private Vector3 mousePos;
	void Update() {


	}
	void OnPostRender() {
		if (!mat) {
			Debug.LogError("Please Assign a material on the inspector");
			return;
		}
		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadIdentity();
		Matrix4x4 pr = new Matrix4x4 ();
		pr [1] = 1;
		GL.MultMatrix (pr);
		GL.Begin(GL.LINES);
		GL.Color(Color.red);
		GL.Vertex(Vector3.one);
		GL.Vertex(new Vector3(10f, 10f, 10f));
		GL.End();
		GL.PopMatrix();
	}
}