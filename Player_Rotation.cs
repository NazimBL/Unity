using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Rotation : NetworkBehaviour {


	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCamRotation;
	[SerializeField] private Transform playerT;
	[SerializeField] private Transform camT;
	[SerializeField] private float learpRate = 15f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotation ();
		LearpRotation ();
	}

	void LearpRotation(){
		if (!isLocalPlayer) {
			camT.rotation = Quaternion.Lerp (camT.rotation, syncCamRotation, learpRate * Time.deltaTime);
			playerT.rotation = Quaternion.Lerp (playerT.rotation, syncPlayerRotation, learpRate * Time.deltaTime);
		}
	}
	[Command]
	void CmdProvideRotationToServer(Quaternion playRot, Quaternion camRot){
		syncPlayerRotation = playRot;
		syncCamRotation = camRot;
		
	}

	[Client]
	void TransmitRotation(){
		if(isLocalPlayer){
			CmdProvideRotationToServer (playerT.rotation, camT.rotation);
		}
	}
}
