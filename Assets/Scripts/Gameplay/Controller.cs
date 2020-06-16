using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour {
	private Rigidbody rb;
	public Camera cam;
	Vector3 movement;
	public float movementForce = 100;
	void Start() {
		rb = GetComponent<Rigidbody>();
		//Cursor.visible = false;
		//Cursor.lockState = CursorLockMode.Confined;
	}
	void OnValidate() {
		rb = GetComponent<Rigidbody>();
	}
	void Update() {
		rb.AddForce((cam.transform.forward * movement.y +
			cam.transform.right * movement.x +
			movement.z * cam.transform.up).normalized * movementForce * Time.deltaTime);
	}
	void OnMove(InputValue value) {
		Vector2 m = value.Get<Vector2>();
		movement.x = m.x;
		movement.y = m.y;
	}
	void OnElevation(InputValue value) {
		movement.z = value.Get<float>();
	}

}
