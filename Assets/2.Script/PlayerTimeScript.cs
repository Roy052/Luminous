using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeScript : MonoBehaviour {
	public int playtime = 0;


	// Use this for initialization
	void Start () {
		StartCoroutine ("Playtimer");

	}

	private IEnumerator Playtimer(){
		while (true) {
			yield return new WaitForSeconds (1);
			playtime += 1;
		}
	}
		
	void OnGUI(){
		GUI.Label(new Rect (50,50,400,70),""+playtime.ToString() + " Seconds");
	}
}
