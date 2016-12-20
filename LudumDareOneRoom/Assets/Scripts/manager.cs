using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour {
	int spec = 1;
	public GameObject playerfire;
	public GameObject playerIce;
	int waves = 0;
	[HideInInspector]
	public bool canSpawn = false;
	GameObject spawner;
	public GameObject title;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(canSpawn)startLevel();
	}

	public void setSpec(int s){
		spec = s;
	}
	public int getSpec(){
		return spec;
	}

	public void startGame(){
		SceneManager.LoadScene("SurviveGame");
	}

	public void spawnPlayer(int sp){
		switch(sp){
		case 1:
			GameObject pf = PhotonNetwork.Instantiate(playerfire.name, new Vector3(0,-4.5f,0),Quaternion.identity,0) as GameObject;
			pf.GetComponent<player>().enabled = true;
			break;
		case 2:
			GameObject pI = PhotonNetwork.Instantiate(playerIce.name, new Vector3(0,-4.5f,0),Quaternion.identity,0)as GameObject;
			pI.GetComponent<player>().enabled = true;
			break;
		case 3:
			GameObject pW = PhotonNetwork.Instantiate("playerWarrior",new Vector3(0,-4.5f,0),Quaternion.identity,0) as GameObject;
			pW.GetComponent<player>().enabled = true;
			break;
		default:
			break;
		}
	}
	public int getWaves(){
		return waves;
	}
	[PunRPC]
	public void setWave(int w){
		GameObject waveOB = GameObject.FindGameObjectWithTag("lvl");
		waveOB.GetComponent<Text>().text = w.ToString();
		waves = w;
	}

	public void endGame(){
		if(PhotonNetwork.isMasterClient){
			PhotonNetwork.RPC(this.gameObject.GetComponent<PhotonView>(),"Lose",PhotonTargets.All,false);
		}
	}

	[PunRPC]
	public void Lose(){
		if(!PhotonNetwork.isMasterClient){
			PhotonNetwork.LeaveRoom();
			SceneManager.LoadScene("title");
			title.SetActive(true);
			spec = 1;
		}
		else{
			StartCoroutine(reset());
		}
	}

	IEnumerator reset(){
		yield return new WaitForSeconds(1);
		waves = 0;
		SceneManager.LoadScene("SurviveGame");
		canSpawn = true;
	}

	void startLevel(){
		if(PhotonNetwork.playerList.Length > 1){
			canSpawn = false;
			spawner = PhotonNetwork.Instantiate("spawner",Vector3.zero, Quaternion.identity,0) as GameObject;
		}
	}
}
