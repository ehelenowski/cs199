using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject player_explosion;
	private  GameController gameController;
	public int scoreVal;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag ("Boundary")) {
			Instantiate (explosion, transform.position, transform.rotation);
			if (other.CompareTag ("Player")) {
				Instantiate (player_explosion, other.transform.position, other.transform.rotation);
				gameController.GameOver ();
			}
			gameController.AddScore (scoreVal);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
