using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour {

	public static ESCMenu instance = null;

	private bool isMusicButton = true;
	private bool isEffsButton = true;
	private bool isMenuOn = false;
	private bool isOnGame = false;

	private int effsSourceIdx = 0;
	private int effsListSize = 20;

	/// 이미지, 오디오 소스
	public AudioClip reflectionSound;
	public AudioClip blackHoleSound;
	public AudioClip GameOverSound;
	public AudioClip turnOnLanternSound;

	public Sprite musicOnSprite;
	public Sprite musicOffSprite;

	public Sprite effsOnSprite;
	public Sprite effsOffSprite;
	////////////////////////////////

	/// UI 구성요소 이미지 컴포넌트
	public Image musicButtonImage;
	public Image effsButtonImage;
	/////////////////////////////

	/// Components
	public AudioSource BGMSource;
	public List<AudioSource> effsSources;
	/// </summary>

	private void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		AudioSource temp;
		for (int i = 0; i < effsListSize; i++) {
			temp = gameObject.AddComponent<AudioSource>();
			effsSources.Add(temp);
		}
	}

	public void musicButtonClicked() {
		if (isMusicButton == true) {
			isMusicButton = false;
			BGMSource.Stop();
			musicButtonImage.sprite = musicOffSprite;
			return;
		}

		isMusicButton = true;
		BGMSource.Play();
		musicButtonImage.sprite = musicOnSprite;
		return;
	}

	public void effsButtonClicked() {
		if (isEffsButton == true) {
			isEffsButton = false;
			effsButtonImage.sprite = effsOffSprite;
			return;
		}

		isEffsButton = true;
		effsButtonImage.sprite = effsOnSprite;
		return;
	}

	public void keepPlayingButtonClicked() {
		turnOffThisMenu();
	}

	public void goBackToMenuButtonClicked() {
		StartCoroutine(callStartmenu());
	}

	public void playEffs(string name) {
		if (isEffsButton == false)
			return;

		effsSourceIdx = (effsSourceIdx + 1) % effsListSize;

		if (string.Equals(name, "mirror"))
			effsSources[effsSourceIdx].clip = reflectionSound;
		else if (string.Equals(name, "blackHole"))
			effsSources[effsSourceIdx].clip = blackHoleSound;
		else if (string.Equals(name, "lantern"))
			effsSources[effsSourceIdx].clip = turnOnLanternSound;
		else if (string.Equals(name, "gameOver"))
			effsSources[effsSourceIdx].clip = GameOverSound;
		else
			Debug.Log("Effect Sound name Error. Check sent name");
		effsSources[effsSourceIdx].Play();
	}

	private void turnOffThisMenu() {
		int i;
		for (i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
		isMenuOn = false;
		Time.timeScale = 1;
	}

	private void turnOnThisMenu() {
		int i;
		for (i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}
		isMenuOn = true;
		Time.timeScale = 0;
	}

	private void escPushed() {
		if (isMenuOn == true)
			turnOffThisMenu();
		else
			turnOnThisMenu();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)
			&& SceneManager.GetActiveScene().buildIndex != 0     //시작메뉴에서의 호출 방지
			&& SceneManager.GetActiveScene().buildIndex != 1)    //스테이지선택 메뉴에서의 호출 방지
			escPushed();
	}

	IEnumerator callStartmenu() {
		turnOffThisMenu();
		//BGMSource.Stop();
		float fadeTime = GameObject.Find("_GameManager").GetComponent<Fading>().Fade(3);
		yield return new WaitForSeconds(fadeTime);

		SceneManager.LoadScene(0);

	}
}