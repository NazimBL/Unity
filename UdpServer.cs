using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class UdpServer : MonoBehaviour {
	

	private const int listenPort = 11000;

	public void StartListening() {
		
		
		UdpClient listener = new UdpClient(listenPort);

		UdpThread myUdpThread = new UdpThread(listener);
		Thread t = new Thread(new ThreadStart(myUdpThread.ThreadLoop));
		t.Start();

		
	}
	

	

}
