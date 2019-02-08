using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class UdpCli : MonoBehaviour {

	public void StartClient() {



		try{
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			//argument is the boradcast address
			IPAddress broadcast = IPAddress.Parse("192.168.1.255");
			
			byte[] sendbuf = Encoding.ASCII.GetBytes("DEADLINE BABYY");
			IPEndPoint ep = new IPEndPoint(broadcast, 11000);
			
			s.SendTo(sendbuf, ep);
			
			Console.WriteLine("Message sent to the broadcast address");
			
		}  
		catch (Exception e ) {
			Console.WriteLine(e.ToString());
		}

	}
}
