using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 
 public class RotateCamera : MonoBehaviour {
     
     public Transform target;
     public float angularSpeed;
 
    public GameObject mm;
     [SerializeField][HideInInspector] 
     private Vector3 initialOffset;
 
     private Vector3 currentOffset;
 
     [ContextMenu("Set Current Offset")]
     private void SetCurrentOffset () {
         if(mm == null) {
             return;
         }
 
         initialOffset = transform.position - target.position;
     }
 
     private void Start () {
         if(mm == null) {
             Debug.LogError ("Assign a target for the camera in Unity's inspector");
         }
 
         currentOffset = initialOffset;
     }
 
     private void LateUpdate () {
         transform.position = target.position + currentOffset;
 
         float movement = Input.GetAxis ("Horizontal") * angularSpeed * Time.deltaTime;
         if(!Mathf.Approximately (movement, 0f)) {
             transform.RotateAround (target.position, Vector3.up, movement);
             currentOffset = transform.position - target.position;
         }
     }
 }