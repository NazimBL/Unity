using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;


public class Menu_Net : NetworkManager {

	public NetworkDiscovery discovery;



	void OnEnable(){
		
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable(){

		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if (scene.buildIndex == 0) {
			DisableVr ();
			StartCoroutine (SetupMenu ());
		}
		 else EnableVr ();
	}

		
		


	//************************************************************
	//************************************************************
	//************************************************************

	/*void SetIpAdress(){

		string ipAdress = GameObject.Find ("InputFiledAdress").transform.Find("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAdress;

	}

	void SetPort(){
		MyNetManager.singleton.networkPort = 7777;
	}*/


		

	public void StartGame(){
		
		if(discovery.running) discovery.StopBroadcast ();
		print ("Startinging client");
		NetworkManager.singleton.StartHost ();
		discovery.Initialize();
		discovery.StartAsServer();

	}

	public void JoinHost(){

		if(discovery.running) discovery.StopBroadcast ();
		discovery.Initialize();	
		discovery.StartAsClient ();

	}

	/*public void Quit(){
		print ("Quiting client");
		NetworkManager.singleton.StopHost ();
	}*/
		

	public override void OnStartHost()
	{
		Debug.Log ("Start Host");
	}

	public override void OnStopHost ()
	{
		print ("stop client");
		if(discovery.running) discovery.StopBroadcast ();

	}

	//************************************************************
	//************************************************************
	//************************************************************

	IEnumerator SetupMenu(){

		yield return new WaitForSeconds (0.4f);
		GameObject.Find ("StartHost").GetComponent<Button> ().onClick.RemoveAllListeners();
		GameObject.Find ("StartHost").GetComponent<Button> ().onClick.AddListener(StartGame);

		GameObject.Find ("JoinHost").GetComponent<Button> ().onClick.RemoveAllListeners();
		GameObject.Find ("JoinHost").GetComponent<Button> ().onClick.AddListener (JoinHost);

		GameObject.Find ("Quit").GetComponent<Button> ().onClick.RemoveAllListeners();
		GameObject.Find ("Quit").GetComponent<Button> ().onClick.AddListener (Application.Quit);


	}


	 void EnableVr(){
		StartCoroutine(LoadDevice("Cardboard", true));
	}

	 void DisableVr(){
		StartCoroutine(LoadDevice("Cardboard", false));
	}

	IEnumerator LoadDevice(string newDevice, bool enable){

		UnityEngine.XR.XRSettings.LoadDeviceByName (newDevice);
		yield return null;
	    UnityEngine.XR.XRSettings.enabled = enable;

	}







}
