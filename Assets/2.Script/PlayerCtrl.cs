using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour {

    public GameObject room;
    public GameObject ob_light;
	private GameObject _GM;

	public float speed = 10.0f;

	private float playerH = 0.0f;
	private float playerV = 0.0f;
    
	private bool isFlying;
	private bool moving;

	private Vector3 playerVec;
	
	public Vector3 goPos;
	private Vector3 mousePos;

	private Vector3 newVec;

    // Use this for initialization
    void Start () {
		isFlying = false;
		moving = true;
        ob_light.SetActive(false);
		_GM = GameObject.Find("_GameManager");
	}


	// Update is called once per frame
	void Update () {
		playerH = Input.GetAxis ("Horizontal");
		playerV = Input.GetAxis ("Vertical");

		playerVec = (Vector3.up * playerV) + (Vector3.right * playerH);

        if (Input.GetMouseButtonDown(0) && !isFlying) {
            moving = false;
            Destroy(room);
        }

		if (Input.GetMouseButtonUp (0)) isFlying = true;

		if (moving && !isFlying) transform.Translate (playerVec * speed * Time.deltaTime);

		if (isFlying) transform.Translate (goPos.normalized * speed * Time.deltaTime);

		if (!moving && !isFlying)
			MouseCheck();

	}

	void MouseCheck(){
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = transform.position.z;

		goPos = mousePos - transform.position;
	}

    void OnCollisionEnter2D(Collision2D coll){

		switch (coll.gameObject.tag) {
		    case "Mirror":
                ESCMenu.instance.playEffs("mirror");
			    MirrorReflect (coll);
			    break;

		    case "BlackWall":
				GameObject.Find("_GameManager").SendMessage("GameOver");
				break;
		}
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "Lantern") {
            ESCMenu.instance.playEffs("lantern");
			if (coll.GetComponent<lantern>().lanterncoll == false) {
				newVec = coll.gameObject.transform.position;
				newVec.z -= 0.5f;
				_GM.GetComponent<GameManager> ().currentLight++;
				ob_light.SetActive (true);
				Instantiate (ob_light, newVec, coll.gameObject.transform.rotation);
				coll.GetComponent<lantern> ().lanterncoll = true;
			}
        }
    }

   /* private void OnBecameInvisible(){
		Debug.Log("aaa");
        if(_GM.GetComponent<GameManager>().gameClear == false) {
			_GM.SendMessage("GameOver");
        }
		//else{
		//	_GM.SendMessage("Success");
		//}
    }*/

    void MirrorReflect(Collision2D coll){
		float mirrorAngle = Mathf.Deg2Rad * coll.transform.eulerAngles.z;

		float xNorDirection = Mathf.Cos(mirrorAngle + (90 * Mathf.Deg2Rad));
		float yNorDirection = Mathf.Sin(mirrorAngle + (90 * Mathf.Deg2Rad));

		Vector2 normalVector = new Vector2(xNorDirection, yNorDirection); 

		Vector2 outVector = Vector2.Reflect (goPos, normalVector);
		goPos.x = outVector.x;
		goPos.y = outVector.y;
	}
}
