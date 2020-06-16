using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BluetoothArduino))]
public class DropdownList : Editor {
	// Start is called before the first frame update
	string[] _choices = SerialPort.GetPortNames();
	int _choiceIndex = 0;

	public override void OnInspectorGUI() {
		// Draw the default inspector
		DrawDefaultInspector();
		_choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
		// Update the selected choice in the underlying object
		var bluetoothArduino = target as BluetoothArduino;
		bluetoothArduino.PortName = _choices[_choiceIndex];
		// Save the changes back to the object
		EditorUtility.SetDirty(target);
	}

}
