using UnityEngine;
using System.Collections;

public class setSpec : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void spec(int sp){
		GameObject man = GameObject.FindGameObjectWithTag("manager");
		man.GetComponent<manager>().setSpec(sp);
	}
}
