using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class ServerSocket : MonoBehaviour {

	
	// Incoming data from the client.
	public static string data = null;
	public Text text;
	// Data buffer for incoming data
	byte[] bytes = new Byte[1024];
	IPAddress ipAddress;
	IPEndPoint localEndPoint;
	Socket listener;

	public void StartListening() {


		serverSetup();

		Debug.Log ("Start Server...");
		try {
			// Bind the socket to the local endpoint and 
			// listen for incoming connections.
			listener.Bind(localEndPoint);
			listener.Listen(10);
			
			// Start listening for connections.
			MyThread myThread = new MyThread(listener,text);
			Thread t = new Thread(new ThreadStart(myThread.ThreadLoop));
			t.Start();


			//have to close socket after end;

		} catch (Exception e) {
			Debug.Log(e.ToString());
		}

	}
	
	void echo(Socket handler){
		//Echo the data back to the client.
		byte[] msg = Encoding.ASCII.GetBytes(data);
		handler.Send(msg);
		
	}
	void closeSocket(Socket handler){

		handler.Shutdown(SocketShutdown.Both);
		handler.Close();
	}

	void serverSetup(){

		//IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
		//ipAddress = ipHostInfo.AddressList[0];
		ipAddress = IPAddress.Loopback;
		localEndPoint = new IPEndPoint(ipAddress, 11000);
		Debug.Log("loopback ip address : "+ipAddress.ToString());
		// Create a TCP/IP socket.
		listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp );

	}


}
