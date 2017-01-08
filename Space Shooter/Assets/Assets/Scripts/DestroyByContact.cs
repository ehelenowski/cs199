using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject player_explosion;

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag ("Boundary")) {
			Instantiate (explosion, transform.position, transform.rotation);
			if (other.CompareTag ("Player")) {
				Instantiate (player_explosion, transform.position, transform.rotation);
			}
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
