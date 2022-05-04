using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbutton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
			StartCoroutine(GameStart());
	}

	private void OnLevelWasLoaded(int level) {
		GameObject.Find("_GameManeger").GetComponent<Fading>().Fade(-1);
	}

	IEnumerator GameStart() {

		float fadeTime = GameObject.Find("_GameManager").GetComponent<Fading>().Fade(3);
		yield return new WaitForSeconds(fadeTime);

		SceneManager.LoadScene(1);
	}
}
