using UnityEngine;

public interface IState  {
	//return id of the scene
	int getSceneId();

	// Use this for initialization
	void Start ();
	
	// Update is called once per frame
	void Update ();
	
	// Update is called once per GUI event
	void OnGUI();
	
	// assigne the player prefab
	void SetPlayer ( playerScript player );
	
	// assigne the manager
	void SetGameManager ( GameManager gameMan );
}
