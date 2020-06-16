using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditor;

public class BluetoothArduino : MonoBehaviour {

	public int Baudrate = 9600;
	[HideInInspector]
	public string PortName = "COM5";
	SerialPort serialPort;
	[HideInInspector]
	public List<string> data;
	public BluetoothArduino(int Baudrate, string PortName) {
		this.Baudrate = Baudrate;
		this.PortName = PortName;
	}
	public BluetoothArduino() { }
	public void Setup() {

		serialPort = new SerialPort();
		serialPort.BaudRate = Baudrate;
		serialPort.PortName = PortName; // Set in Windows
		serialPort.Open();
		serialPort.DataReceived += dataRecieved;
	}

	private void dataRecieved(object sender, SerialDataReceivedEventArgs e) {
		SerialPort sp = (SerialPort)sender;
		data.Add(sp.ReadLine());
	}
}
