using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
	{

	public Vector3 PlayerVelocity;
	public float playerSpeed, turnSmoothTime;
	public Transform Camera;


	private CharacterController controller;
	private float turnSmoothVelocity= 1;

	// Start is called before the first frame update
	void Start()
		{
		controller = gameObject.GetComponent<CharacterController>();
		}

	// Update is called once per frame
	void Update()
		{
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

		if (move.magnitude >= 0.1f)
			{

			float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

			transform.rotation = Quaternion.Euler(0, targetAngle, 0f);

			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

			PlayerVelocity = moveDir.normalized * Time.deltaTime * playerSpeed;

			controller.Move(PlayerVelocity);

			}
		}
	}
