using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//public
	public GameObject Player;
	
	//private
	private bool loaded = false;
	
	private IState _CurrentState;
	public IState CurrentState {
		get {
			return _CurrentState;
		}
		set {
			_CurrentState = value;
			GameObject.DontDestroyOnLoad( gameObject );
			Application.LoadLevel( _CurrentState.getSceneId() );
			loaded = true;
		}
	}

	// Use this for initialization
	void Start () {
		CurrentState = new MainMenu(  );
	}
	
	// Update is called once per frame
	void Update () {
		if( loaded ) {
			loaded = false;
			CurrentState.SetGameManager( this );
			CurrentState.Start();
			return;
		}
		if( CurrentState != null ) {
			CurrentState.Update();
		}
	}
	
    void OnGUI() {
		if( CurrentState != null ) {
			CurrentState.OnGUI();
		}
    }
}
