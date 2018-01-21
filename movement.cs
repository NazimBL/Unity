using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	public float speed = 5f;
	private CharacterController controller;



	// Use this for initialization
	void Start () {
		 controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {


		controller.SimpleMove(speed * Camera.main.transform.TransformDirection(Vector3.forward * Input.GetAxis("Vertical") +
			Vector3.right * Input.GetAxis("Horizontal")));

	}


		





}
