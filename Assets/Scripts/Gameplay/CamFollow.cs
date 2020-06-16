using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class CamFollow : MonoBehaviour {
	public Transform target;

	public float distance = 10f;
	public float zoomDampening = 5.0f;
	bool mode = true;
	Vector2 movement;
	Quaternion rotation, desiredRotation;
	public float minLimit = 80f;
	Vector2 Potation;
	Vector3 rbRot;
	public float speed = 10f;
	Rigidbody rb;

	void OnValidate() {
		rb = target.GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	void Start() {
		rb = target.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {

		// If middle mouse and left alt are selected? ORBIT
		Potation.x -= movement.y * speed*0.02f;
		Potation.y -= movement.x * speed*0.02f;

		////////OrbitAngle

		//Clamp the vertical axis for the orbit
		//Potation.y = ClampAngle(Potation.y, -minLimit, minLimit);
		// set camera rotation 
		//desiredRotation = Quaternion.Euler(Potation.x, Potation.y, 0);
		if (rb.velocity.sqrMagnitude > 1) rbRot = rb.velocity.normalized;
		desiredRotation = Quaternion.LookRotation(target.position - transform.position + rbRot);
		desiredRotation = Quaternion.Euler(Potation.x, Potation.y, 0);

		rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * zoomDampening);
		transform.rotation = rotation;
		if (mode) transform.position = Vector3.Lerp(transform.position, target.position - (rotation * Vector3.forward * distance), Time.deltaTime * speed);
		if (rb.velocity.sqrMagnitude > 25) mode = true;
	}
	void OnLook(InputValue value) => movement = value.Get<Vector2>();

	void OnFire(InputValue value) => mode = value.isPressed;
}
