using UnityEngine;
using System.Collections;

public class OldSnake : IState {
	public GameObject Player;
	public playerScript PlayerScript;
	private GameManager GameMan;
	
	public OldSnake( GameObject player ) {
		this.Player = player;
	}

	// Use this for initialization
	public void Start () {
		this.Player = (GameObject)GameObject.Instantiate( this.Player, new Vector3( 4.5f, 0, 4.5f ), Quaternion.identity );
		PlayerScript = this.Player.GetComponent<playerScript>();
		PlayerScript.player = true;
		playerScript.dead = false;
	}
	
	// Update is called once per frame
	public void Update () {
		if( playerScript.dead ) {
			if( Input.GetMouseButtonDown(0) || Input.GetKeyDown( KeyCode.Space ) )
				GameMan.CurrentState = new OldSnake( GameMan.Player );
			return;
		}
		if(  Input.GetKeyDown( KeyCode.Escape ) ){
			GameMan.CurrentState = new MainMenu();
		}
	}
	
	// Update is called once per GUI event
	public void OnGUI() {
	}
	
	//return id of the scene
	public int getSceneId(){
		return 2;
	}
	
	// assign the playerscript
	public void SetPlayer( playerScript player ){
		this.PlayerScript = player;
	}
	
	// assign the playerscript
	public void SetGameManager( GameManager gameMan ){
		this.GameMan = gameMan;
	}
}
