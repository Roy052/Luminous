using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int currentStage = 0;
	public int currentLight = 0;
	public int lightCount = 0;
	public GameObject player;
	public Text resultText;
	public bool gameClear = false;
	public bool isSfxMute = false;
	public AudioClip sfx;
	public int playtime;
	// Use this for initialization
	void Start () {
		if (currentStage < 0)
			return;
		resultText.text = "";
		switch(currentStage) {
		case 3:
			resultText.text = "Trust me";
			break;
		case 4:
			resultText.text = "One way";
			break;
		case 5:
			resultText.text = "Scared?";
			break;
		case 6:
			resultText.text = "Lucky?";
			break;
		case 7:
			resultText.text = "Fly me";
			break;
		case 8:
			resultText.text = "Labotory?";
			break;
		case 16:
			resultText.text = "EXIT?";
			break;
		case 17:
			resultText.text = "You Escaped";
			break;
		}
	}

    // Update is called once per frame
    void Update () {
		/*if (currentStage == -1){
			if (Input.GetMouseButton(0)) {
				StartCoroutine(this.ToNextStage(1.0f));
			}
		}*/

		if (currentStage > 0) {
			if (currentStage == 17) {
				if (Input.GetMouseButton (0))
					Success ();
			}
			else if (currentLight == lightCount) {
				gameClear = true;
				GameObject.FindGameObjectWithTag ("Player").SetActive (false);
				Success ();
			} 

		}
	}

	void GameOver(){
        ESCMenu.instance.playEffs("gameOver");
        switch (currentStage) {
		case 3:
			resultText.text = "Why?";
			break;
		case 4:
			resultText.text = "No Other Way";
			break;
		case 5:
			resultText.text = "Scared!";
			break;
		case 6:
			resultText.text = "Unlucky";
			break;
		case 7:
			resultText.text = "Fallen";
			break;
		case 8:
			resultText.text = "Trapped";
			break;
		case 16:
			resultText.text = "Why?";
			break;
		default:
			resultText.text = "Game Over";
			break;
		}
		Destroy(player);
		//GameObject.Find("Player").SetActive(false);
		currentLight = 0;
		StartCoroutine(this.ToCurrentStage());
	}

	public void GameRestart(){
		resultText.text = "Again";
		Destroy (player);
		currentLight = 0;
		StartCoroutine(this.ToCurrentStage());
	}

	void Success(){
		switch(currentStage) {
		case 3:
			resultText.text = "See?";
			break;
		case 6: 
			resultText.text = "Lucky";
			break;
		case 7:
			resultText.text = "To the moon";
			break;
		case 8:
			resultText.text = "Escaped";
			break;
		default :
			resultText.text = "Game Clear";
			break;
		}
		//GameObject.Find("Player").SetActive(false);
		currentLight = 0;
		StartCoroutine(this.ToNextStage(1.5f));
	}

	IEnumerator ToCurrentStage(){
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(currentStage + 1);

	}

	IEnumerator ToNextStage(float delay){
		yield return new WaitForSeconds(delay);
		if (currentStage == 17) {
			SceneManager.LoadScene (0);
		} else 
			SceneManager.LoadScene (currentStage + 2);
	}
		

	public void PlaySfx(Vector3 pos, AudioClip sfx){
		if (isSfxMute)
			return;
		GameObject soundObj = new GameObject ("sfx");
		soundObj.transform.position = pos;
		AudioSource audioSource = soundObj.AddComponent<AudioSource> ();
		audioSource.clip = sfx;
		audioSource.minDistance = 10.0f;
		audioSource.maxDistance = 40.0f;
		audioSource.volume = 1.0f;
		audioSource.Play ();

	}
}
