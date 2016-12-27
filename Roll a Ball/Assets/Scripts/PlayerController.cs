using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	private int count; 
	private bool allowJump;
	public float speedBoost;
	public Text countText; 
	public Text winText;
	public Text actionText;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText (); 
		winText.text = "";
		actionText.text = " ";
		allowJump = true;
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

		if (Input.GetKey (KeyCode.Space) && allowJump) {
			rb.AddForce (new Vector3 (0, 300, 0));
			allowJump = false;
		}
	}

	//Called when our unity object first touches a trigger collider
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		} else if (other.gameObject.CompareTag ("Speed Up")) {
			StartCoroutine (DisplaySpeedBoost ());
			rb.velocity *= speedBoost;
			allowJump = true;
		} else if (other.gameObject.CompareTag ("Stop Pill")) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.rotation = Quaternion.identity;
			other.gameObject.SetActive (false);
		} else if (other.gameObject.CompareTag ("Warp")) {
			transform.position = new Vector3 (-transform.position.x, transform.position.y, -transform.position.z);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground")){
			allowJump = true;
		}
	}

	private IEnumerator DisplaySpeedBoost(){
		lock (actionText) {
			actionText.text = "Speed Boost!";
			yield return new WaitForSeconds (2.5f);
			actionText.text = "";
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 17) {
			winText.text = "You Win!"; 
			gameObject.SetActive (false);
		}
	} 
}