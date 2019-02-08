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

public class UdpThread {

	UdpClient listner;
	// Use this for initialization
	public UdpThread(UdpClient listener)
	{
		this.listner = listener;

		
	}
	
	public void SetParam(UdpClient param1)
	{
		this.listner = param1;

	}
	public void ThreadLoop()
	{

		IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 11000);
		Debug.Log ("Start looping...");
			
			try
			{	
			while (true)
			{
				Console.WriteLine("Waiting for broadcast");
				byte[] bytes = listner.Receive(ref groupEP);
				//byte[] msg = Encoding.ASCII.GetBytes("Yo man <EOF>");
				//listner.Send(msg,msg.Length);
				
				
				Debug.Log (" received : "+Encoding.ASCII.GetString(bytes, 0, bytes.Length));
			}

			}
		
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}
