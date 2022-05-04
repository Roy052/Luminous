using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour{

	public float mass = 3.5f;
	private GameObject player;
	public Vector2 totalExternalForce;

	// Use this for initialization
	void Start(){
		player = GameObject.Find("Player");

	}

	// Update is called once per frame
	void Update(){

	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            ESCMenu.instance.playEffs("blackHole");
    }

    private void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			ComputeBlackhole();
		}
	}

	void ComputeBlackhole(){
		int i = 0;
		
		float distance = Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position);

		Vector2 centerVec = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);

		centerVec /= centerVec.magnitude;
		centerVec /= (distance * distance);
		centerVec *= mass;
		
		i++;
		float temp = player.GetComponent<PlayerCtrl>().goPos.z;

		player.GetComponent<PlayerCtrl>().goPos += (Vector3)centerVec;
		player.GetComponent<PlayerCtrl>().goPos.z = temp;
	}
}