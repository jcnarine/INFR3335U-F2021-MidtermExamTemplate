using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
	{

	public Vector3 PlayerVelocity;
	public float playerSpeed, turnSmoothTime;
	public Transform Camera;
	public TextMeshProUGUI coinTally; 


	private CharacterController controller;
	private Animator m_Animator;
	private float turnSmoothVelocity = 1;
	private int coinsRemaining=10;

	// Start is called before the first frame update
	void Start()
		{
		m_Animator = gameObject.GetComponent<Animator>();
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
			m_Animator.SetInteger("AnimatorState", 1);
			transform.rotation = Quaternion.Euler(0, targetAngle, 0f);

			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

			PlayerVelocity = moveDir.normalized * Time.deltaTime * playerSpeed;

			controller.Move(PlayerVelocity);

			}
		else{
			m_Animator.SetInteger("AnimatorState", 0);
			}
		}


	private void OnTriggerEnter(Collider other)
		{
		if (other.gameObject.CompareTag("Coin"))
			{
			Destroy(other.gameObject);
			coinsRemaining--;
			if (coinsRemaining == 0)
				{
				SceneManager.LoadScene("End");

				}
			coinTally.text = "Coins Remaining: "+coinsRemaining;
			}
		}
	}
