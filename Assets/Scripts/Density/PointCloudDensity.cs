using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Rendering;

public class PointCloudDensity : DensityGenerator {
	[SerializeField]
	public TextAsset objFile = null;
	public static List<string> data = new List<string>();
	public static Vector3[] points;
	public bool Test = false;
	public float voxelSize = 1;
	static public Texture3D texture3D;
	void Start() {

	}
	private void OnValidate() {
		if (objFile != null) {
			data = objFile.text.Split(Environment.NewLine.ToCharArray()).ToList();
		}
		if (data.Count > 0) {
			if (Test) {
				Test = false;
				loadPoints();
			}
		}
	}
	private void Awake() {
		loadPoints();
	}
	public static void loadPoints() {
		if (data.Count <= 0) return;
		//Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		//Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
		List<string> dat = data.FindAll(d => d.StartsWith("v"));
		for(int i = 0; i< dat.Count; i++) {
			dat[i] = dat[i].Substring(1);
		}
		points = new Vector3[dat.Count];
		float[] a = new float[3];
		Vector3 min = Vector3.zero;
		Vector3 max = Vector3.zero;
		for (int i = 0; i < points.Length; i++) {
			string[] values = dat[i].Split(' ');
			Array.ForEach(values, v => Debug.Log(v));
			for(int j = 1; j<4; j++) {
				a[j-1] = float.Parse(values[j], NumberFormatInfo.InvariantInfo);
			}
			min.x = Mathf.Min(min.x, a[0]);
			min.y = Mathf.Min(min.y, a[1]);
			min.z = Mathf.Min(min.z, a[2]);
			max.x = Mathf.Max(max.x, a[0]);
			max.y = Mathf.Max(max.y, a[1]);
			max.z = Mathf.Max(max.z, a[2]);
			points[i] = new Vector3(a[0], a[1], a[2]);
		}
		Debug.Log(points);
		texture3D = new Texture3D((int)(max.x - min.x), (int)(max.y - min.y), (int)(max.z - min.z), TextureFormat.RGB565, 0);
		foreach (var p in points) {
			texture3D.SetPixel((int)p.x, (int)p.y, (int)p.z, texture3D.GetPixel((int)p.x, (int)p.y, (int)p.z) + Color.white);
		}
	}

	// Update is called once per frame

	public override ComputeBuffer Generate(ComputeBuffer pointsBuffer, int numPointsPerAxis, float boundsSize, Vector3 worldBounds, Vector3 centre, Vector3 offset, float spacing) {
		densityShader.SetTexture(0, "tex", texture3D);
		return base.Generate(pointsBuffer, numPointsPerAxis, boundsSize, worldBounds, centre, offset, spacing);
	}
}
