using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float speed = 25f;
	public float jump = 6f;
	public float gravity = 20f;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private float rotatoTo, currentRotationAngle;
	private Quaternion currentRotation;


	// Use this for initialization
	void Start () {
		 controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// is the controller on the ground?
		if (controller.isGrounded) {
		//	transform.LookAt (Camera.main.transform);
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);
			moveDirection = transform.TransformDirection(moveDirection);
			//Multiply it by speed.
			//moveDirection *= speed;
			//Jumping
			if (Input.GetButton("Jump"))
				moveDirection.y = jump;

		}
		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}


		





}
