using UnityEngine;
using System.Collections;

public class popSourisScript : MonoBehaviour {
	public static bool dead;
	public GameObject souris;
	public float timeCase;
	public static float timeCommon;
	public int disabled = 0;
	public float minWait = 0.5f;
	public float maxWait = 2f;
	private Transform tr;
	private Renderer rr;
	private GameObject _souris;

	// Use this for initialization
	void Start () {
		dead = false;
		tr = transform;
		rr = renderer;
		timeCase = Random.Range( minWait, maxWait );
		_souris = (GameObject)Instantiate( souris, new Vector3( -500, 500, -500 ), Quaternion.identity );
	}
	
	// Update is called once per frame
	void Update () {
		if( isDisabled() )
			return;
		if( Time.time > timeCase + timeCommon ){
			timeCase = Random.Range( minWait, maxWait );
			timeCommon = Time.time + timeCase;
			popSouris();
		}
	}
	
	void popSouris() {
		_souris.transform.position = new Vector3( tr.position.x, 0, tr.position.z );
	}
	
	void OnTriggerEnter( Collider other ) {
		if( other.gameObject.tag == "Player" || other.gameObject.tag == "Souris" ) {
			disabled++;
			//setColor();
		}
	}
	
	void OnTriggerExit( Collider other ) {
		if( other.gameObject.tag == "Player" || other.gameObject.tag == "Souris" ) {
			disabled--;
			//setColor();
			if( disabled == 0 )
				timeCase = Random.Range( minWait, maxWait );
		}
	}
	
	void setColor() {
		if( isDisabled() ) {
			rr.material.color = Color.blue;
		} else {
			rr.material.color = Color.white;
		}
	}
	
	bool isDisabled() {
		return disabled > 0 || dead;
	}
	
	
}
