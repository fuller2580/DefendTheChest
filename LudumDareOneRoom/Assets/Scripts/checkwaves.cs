using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class checkwaves : MonoBehaviour {

	// Use this for initialization
	void Start () {
		manager man = GameObject.FindGameObjectWithTag("manager").GetComponent<manager>();
		if(man.getWaves() > 0) this.gameObject.GetComponent<Text>().text = "You lasted for "+man.getWaves()+" waves!!";
		else this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
