using UnityEngine;
using System.Collections;

public class setNames : MonoBehaviour {
	public PhotonView pv;
	TextMesh myMesh;
	TextMesh tarMesh;
	string datName;

	// Use this for initialization
	void Start () {
		myMesh = GetComponent<TextMesh>();
		pv.RPC("getName",PhotonTargets.AllBuffered,PlayerPrefs.GetString("Username"), pv.viewID);
		myMesh.text = PlayerPrefs.GetString("Username");
	}
	
	[PunRPC]
	public void getName(string name, int datID){
		GameObject targo = PhotonView.Find(datID).gameObject;
		if(targo.GetComponent<PhotonView>().isMine){
			
		}
		else{
		tarMesh = targo.GetComponent<TextMesh>();
		tarMesh.text = name;
		}
	}

	public void callsetESG(){
		pv.RPC("setESG",PhotonTargets.AllBuffered,pv.viewID);
	}

	[PunRPC]
	public void setESG(int datID){
		GameObject targo = PhotonView.Find(datID).gameObject;
		tarMesh = targo.GetComponent<TextMesh>();
		tarMesh.text = name;
		tarMesh.color = new Vector4(71f,186f,188f,1f);
	}
}
