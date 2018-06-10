using UnityEngine;
using System.Collections;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

ublic class ClientSocket : MonoBehaviour {


	public int id;

	public void StartClient() {
		// Data buffer for incoming data.
		byte[] bytes = new byte[1024];

		// Connect to a remote device.
		try {

			IPAddress ipAddress;
			//192.168.4.1
			//station point address
			ipAddress=IPAddress.Parse("192.168.0.152");
			Debug.Log("client sending");
			IPEndPoint remoteEP = new IPEndPoint(ipAddress,80);
		


			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork, 
			                           SocketType.Stream, ProtocolType.Tcp );
			
			// Connect the socket to the remote endpoint. Catch any errors.
			try {
				sender.Connect(remoteEP);
				// Encode the data string into a byte array.
				byte[] msg = Encoding.ASCII.GetBytes(id+"<EOF>");


				// Send the data through the socket.
				int bytesSent = sender.Send(msg);
				// Release the socket.
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
				
			} catch (ArgumentNullException ane) {
				Debug.Log("ArgumentNullException : {0}"+ane.ToString());
			} catch (SocketException se) {
				Debug.Log("SocketException : {0}"+se.ToString());
			} catch (Exception e) {
				Debug.Log("Unexpected exception : {0}"+e.ToString());
			}
			
		} catch (Exception e) {
			Debug.Log(e.ToString());
		}
	}


}
