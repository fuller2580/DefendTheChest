using UnityEngine;
using System.Collections;

public class spawnRain : MonoBehaviour {
	public GameObject rain;
	Rigidbody2D rig;
	public float xforce;
	public float yforce;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();
		rig.AddForce(new Vector2(xforce,yforce));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "ground" || col.gameObject.tag == "enemy"){
			spawn();
		}
	}
	void spawn(){
		Vector3 tempLoc = transform.position;
		tempLoc.y += 1.5f;
		PhotonNetwork.Instantiate(rain.name, tempLoc, Quaternion.Euler(-90,0,0),0);
		PhotonNetwork.Destroy(this.gameObject);
			
	}
}
