using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Discovery_Custom : NetworkDiscovery {




	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		Debug.Log ("Recived some shit");
		var stuff = data.Split(':'); 
		 
		if(stuff.Length == 3 && stuff[0]=="NetworkManager"){
			Debug.Log ("Data is ready");
			if (NetworkManager.singleton != null && NetworkManager.singleton.client == null) {
				Debug.Log ("Starting the player as Clinet");
				Debug.Log (fromAddress);
				NetworkManager.singleton.networkAddress = fromAddress;
				Debug.Log (Convert.ToInt32 (stuff [2]));
				NetworkManager.singleton.networkPort = Convert.ToInt32 (stuff [2]);
				NetworkManager.singleton.StartClient ();
				
			}

		}
	}
}
