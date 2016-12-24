using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	private int count; 
	public Text countText; 
	public Text winText;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText (); 
		winText.text = "";
	}
	
	// Update is called once per frame, before the frame is rendered
	void Update () {
		
	} 

	// FixedUpdate is called before any physics calculation, where all physics code goes
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical); 

		rb.AddForce (movement * speed);
	}

	//Called when our unity object first touches a trigger collider
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!"; 
		}
	} 
}