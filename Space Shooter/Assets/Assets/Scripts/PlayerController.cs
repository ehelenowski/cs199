using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn_mid;
	public Transform shotSpawn_left;
	public Transform shotSpawn_right;
	public float fireRate;

	private float nextFire = -1f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update() {
		if ((Input.GetButton ("Fire1") || Input.GetKey(KeyCode.Space))  && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn_mid.position, shotSpawn_mid.rotation); 
			Instantiate (shot, shotSpawn_left.position, Quaternion.Euler (shotSpawn_left.rotation.x, -40f, shotSpawn_left.rotation.z)); 
			Instantiate (shot, shotSpawn_right.position, Quaternion.Euler (shotSpawn_right.rotation.x, 40f, shotSpawn_right.rotation.z));
			gameObject.GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb.velocity = new Vector3 (moveHorizontal, 0f, moveVertical) * speed;

		rb.position = new Vector3 (Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax) , 
								   0f, 
								   Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
		rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
	}
}
