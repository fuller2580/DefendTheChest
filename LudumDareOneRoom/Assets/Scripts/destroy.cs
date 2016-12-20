using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {
	public PhotonView myView;
	// Use this for initialization
	void Start () {
		if(myView == null)myView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	[PunRPC]
	public void killMe(){
		if(myView.isMine)PhotonNetwork.Destroy(this.gameObject);
	}
}
