using UnityEngine;
using System.Collections;

public class MainMenu : IState {
	public GameObject Player;
	public playerScript PlayerScript;
	private GameManager GameMan;
	
	public MainMenu(  ) {
	}
	
	
	// Use this for initialization
	public void Start() {
	}
	
	// Update is called once per frame
	public void Update () {
		if( Input.GetKeyDown( KeyCode.Space ) || true ) {
			GameMan.CurrentState = new OldSnake( GameMan.Player );
		}
	}
	
	// Update is called once per GUI event
	public void OnGUI() {
	}
	
	//return id of the scene
	public int getSceneId(){
		return 1;
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
