using UnityEngine;
using System.Collections;

public class bouletteScript : MonoBehaviour {
	public static bool dead;
	public int nbSteps = 12;
	private int step = 0;
	private float duration;
	public Direction direction; 
	public Transform tr;
	public Rigidbody rb;
	public Vector2[] mouvements = new Vector2[4]{ 
		new Vector2(  0,  1 ), 
		new Vector2(  1,  0 ), 
		new Vector2(  0, -1 ), 
		new Vector2( -1,  0 ) 
	};
	public static Vector2 startTarget;
	public Vector2 target;
	Vector2 current = new Vector2( 4.5f, 4.5f );
	GameObject next = null;

	// Use this for initialization
	void Start () {
		dead = false;
		tr = transform;
		rb = rigidbody;
		duration = nbSteps*Time.fixedDeltaTime;
		current = new Vector2( tr.position.x, tr.position.z );
		target = startTarget;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate() {
		if( dead ){
			rb.velocity = Vector3.zero;
			return;
		}
		if( target == Vector2.zero )
			return;
		step = ( step + 1 ) % nbSteps;
		if( step != 0 )
			return;
		rb.velocity = new Vector3( (target.x-current.x)/duration, 0, (target.y-current.y)/duration );
		current = target;
		if( next != null ){
			next.GetComponent<bouletteScript>().target = current;
		}
	}
	
	public void createBoulette( GameObject boulette ) {
		if( next != null )
			next.GetComponent<bouletteScript>().createBoulette( boulette );
		else {
			bouletteScript.startTarget = Vector2.zero;
			next = (GameObject)Instantiate( boulette, new Vector3( current.x, 0, current.y ), Quaternion.identity );
		}
	}
}
