using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private Rigidbody rb;
	private int count; 
	private bool allowJump;
	private bool over;
	public float speedBoost;
	public float speed;
	public Text winText;
	public Text countText;
	public GameObject player;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		winText.text = "";
		allowJump = true;
	}

	// Update is called once per frame, before the frame is rendered
	void Update () {
	} 

	private IEnumerator restart(){
		lock (countText) {
			for (int i = 5; i > 0; i--) {
				countText.text = "Restarting Game in "+ i + " seconds...";
				yield return new WaitForSeconds (1f);
			}
			SceneManager.LoadSceneAsync ("MiniGame");
		}
	}

	// FixedUpdate is called before any physics calculation, where all physics code goes
	void FixedUpdate() {
		Vector3 movement = player.transform.position - transform.position;
		movement.Normalize ();
		rb.AddForce (movement * speed);
	}

	//Called when our unity object first touches a trigger collider
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Speed Up")) {
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
		if (other.gameObject.CompareTag ("Ground")) {
			allowJump = true;
		} else if (other.gameObject.CompareTag ("Player")) {
			winText.text = "You Lose!";
			other.gameObject.SetActive (false);
			StartCoroutine (restart ());
		} else if (other.gameObject.CompareTag ("Wall") && allowJump) {
			rb.AddForce (new Vector3 (0, 300, 0));
			allowJump = false;
		}
	}
}

