using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawerScript : MonoBehaviour {
	public GameObject spawnL;
	public GameObject spawnR;
	int enemy1count = 3;
	float enemy2count;
	float enemy3count;
	bool spawnReady = true;
	public GameObject enemy1;
	public GameObject enemy2;
	public int wave = 0;
	GameObject man;
	// Use this for initialization
	void Start () {
		man = GameObject.FindGameObjectWithTag("manager");
		//man.GetComponent<manager>().spawnPlayer(man.GetComponent<manager>().getSpec());
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnReady && PhotonNetwork.isMasterClient) StartCoroutine(spawn());
	}

	IEnumerator wait(float waitTime){
		yield return new WaitForSeconds(waitTime);
		enemy1count += 3;
		enemy2count = Mathf.Floor((enemy1count+1)*0.1f);
		enemy3count = Mathf.Floor((enemy1count)*0.1f);
		spawnReady = true;
	}

	IEnumerator spawn(){
		spawnReady = false;
		wave++;
		PhotonNetwork.RPC(man.GetComponent<PhotonView>(), "setWave", PhotonTargets.AllBuffered, false, wave);
		if(enemy2count >= 1) StartCoroutine(spawn2());
		if(enemy3count >= 1) StartCoroutine(spawn3());
		for(int i = 0; i < enemy1count; i++){
			GameObject en1;
			if(wave < 10)en1 = PhotonNetwork.Instantiate(enemy1.name, spawnL.transform.position, Quaternion.identity,0) as GameObject;
			else en1 = PhotonNetwork.Instantiate("enemy12", spawnL.transform.position, Quaternion.identity,0) as GameObject;
			Vector3 tempScale = en1.transform.localScale;
			tempScale.x *= -1;
			en1.transform.localScale = tempScale;
			en1.GetComponent<enemyScript>().enabled = true;
			en1.GetComponent<enemyScript>().setSpeed(wave);
			if(wave < 10)en1 = PhotonNetwork.Instantiate(enemy1.name, spawnR.transform.position, Quaternion.identity,0) as GameObject;
			else en1 = PhotonNetwork.Instantiate("enemy12", spawnR.transform.position, Quaternion.identity,0) as GameObject;
			en1.GetComponent<enemyScript>().enabled = true;
			en1.GetComponent<enemyScript>().setSpeed(wave);
			yield return new WaitForSeconds(2f);
		}


		StartCoroutine(wait(15));
	}

	IEnumerator spawn2(){
		yield return new WaitForSeconds(3);
		for(int i = 0; i < enemy2count; i++){
			GameObject en2;
			if(wave < 10)en2 = PhotonNetwork.Instantiate(enemy2.name, spawnL.transform.position, Quaternion.identity,0) as GameObject;
			else en2 = PhotonNetwork.Instantiate("enemy22", spawnL.transform.position, Quaternion.identity,0) as GameObject;
			Vector3 tempScale = en2.transform.localScale;
			tempScale.x *= -1;
			en2.transform.localScale = tempScale;
			en2.GetComponent<enemyScript>().enabled = true;
			en2.GetComponent<enemyScript>().setSpeed(wave);
			if(wave < 10) en2 = PhotonNetwork.Instantiate(enemy2.name, spawnR.transform.position, Quaternion.identity,0) as GameObject;
			else en2 = PhotonNetwork.Instantiate("enemy22", spawnR.transform.position, Quaternion.identity,0) as GameObject;
			en2.GetComponent<enemyScript>().enabled = true;
			en2.GetComponent<enemyScript>().setSpeed(wave);
			yield return new WaitForSeconds(10f);
		}
	}

	IEnumerator spawn3(){
		yield return new WaitForSeconds(11);
		for(int i = 0; i < enemy2count; i++){
			GameObject en2;
			if(wave < 10)en2 = PhotonNetwork.Instantiate("enemy3", spawnL.transform.position, Quaternion.identity,0) as GameObject;
			else en2 = PhotonNetwork.Instantiate("enemy32", spawnL.transform.position, Quaternion.identity,0) as GameObject;
			Vector3 tempScale = en2.transform.localScale;
			tempScale.x *= -1;
			en2.transform.localScale = tempScale;
			en2.GetComponent<enemyScript>().enabled = true;
			en2.GetComponent<enemyScript>().setSpeed(wave);
			if(wave < 10)en2 = PhotonNetwork.Instantiate("enemy3", spawnR.transform.position, Quaternion.identity,0) as GameObject;
			else en2 = PhotonNetwork.Instantiate("enemy32", spawnR.transform.position, Quaternion.identity,0) as GameObject;
			en2.GetComponent<enemyScript>().enabled = true;
			en2.GetComponent<enemyScript>().setSpeed(wave);
			yield return new WaitForSeconds(10f);
		}
	}

}
