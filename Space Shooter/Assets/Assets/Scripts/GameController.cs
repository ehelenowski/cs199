using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text highScoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private static int highScore = 0;
	private int hazardCount;
	private bool over;
	private bool restart;

	void Start() {
		over = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		hazardCount = 10;
		UpdateScore ();
		highScoreText.color = Color.white;
		StartCoroutine (SpawnWaves ()) ;
	}

	void Update() {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		while (!over) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				if (over) {
					restartText.text = "Press 'R' for Restart";
					restart = true;
				}

				yield return new WaitForSeconds (Random.value);
			}
			yield return new WaitForSeconds (waveWait);
			hazardCount *= 2;
		}
		restartText.text = "Press 'R' for Restart";
		restart = true;
	}

	public void AddScore(int points) {
		score += points;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
		if (score > highScore) {
			highScore = score;
			highScoreText.color = Color.green;
		}
		highScoreText.text = "High Score: " + highScore;
	}

	public void GameOver() {
		gameOverText.text = "Game Over!";
		over = true;
	}
}
