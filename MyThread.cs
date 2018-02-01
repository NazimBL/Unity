using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class MyThread {
	
	Socket listener;
	Text text;

		
		public MyThread(Socket listener,Text text)
		{
			this.listener = listener;
		    this.text = text;

		}
		
		// Méthode de modification du paramètre
		public void SetParam(Socket param1,Text param2)
		{
			this.listener = param1;
			this.text = param2;
		}
		
		// Méthode boucle du thread
		public void ThreadLoop()
		{
			while(true){
			
			Debug.Log ("Start looping...");
			// Program is suspended while waiting for an incoming connection.
			
			Socket handler = listener.Accept();
			String data = null;
			byte[] bytes=new byte[1023];
			
			if(handler.Connected){
				
				Debug.Log ("Connected true");
				readLoop(handler,bytes,data);

			}
			//echo function 
			//echo(handler);
		}
			
		}
	void readLoop(Socket handler,byte[] bytes,String data){
		// An incoming connection needs to be processed.
		while (true) {

			Debug.Log ("reading..");
			bytes = new byte[1024];
			int bytesRec = handler.Receive(bytes);
			data += Encoding.ASCII.GetString(bytes,0,bytesRec);
			if (data.IndexOf("<EOF>") > -1) {
				text.text="text received : "+data;
				Debug.Log ("Connected true Text received..."+data);
				break;
			}
		}

		
	}

	}

