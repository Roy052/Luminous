using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

	int stage = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(stage != 0) {
			StartCoroutine(ToStage(1.5f, stage));
		}
	}

	public void StartStage(int stage) {
		StartCoroutine(ToStage(1.0f, stage + 1));
	}

	IEnumerator ToStage(float delay, int stage) {
		float fadeTime = GameObject.Find("_GameManager").GetComponent<Fading>().Fade(3);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene(stage);
	}
}
