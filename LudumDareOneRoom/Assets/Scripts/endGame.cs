using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "enemy"){
			if(PhotonNetwork.isMasterClient){
				PhotonNetwork.RPC(GameObject.FindGameObjectWithTag("manager").GetComponent<PhotonView>(), "Lose", PhotonTargets.All,false);
			}
			print("end game");
		}
	}
}
