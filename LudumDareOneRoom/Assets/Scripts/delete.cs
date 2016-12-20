using UnityEngine;
using System.Collections;

public class delete : MonoBehaviour {
	public float wt = 0f;
	// Use this for initialization
	void Start () {
		StartCoroutine(end());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator end(){
		yield return new WaitForSeconds(wt);
		PhotonNetwork.Destroy(this.gameObject);
	}
}
