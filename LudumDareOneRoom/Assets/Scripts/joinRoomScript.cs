using UnityEngine;
using System.Collections;

public class joinRoomScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void joinRoom(){
		GameObject man = GameObject.FindGameObjectWithTag("manager");
		man.GetComponent<network>().checkingRooms = true;
		GameObject.FindGameObjectWithTag("titleImage").SetActive(false);
	}
}
