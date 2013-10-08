using UnityEngine;
using System.Collections;

public enum Direction {
	top = 0,
	right = 1,
	down = 2,
	left = 3
}

public class playerScript : MonoBehaviour {
	public int nbSteps = 12;
	public int pointSouris = 1000;
	public int pointSecond = 1000;
	public GameObject next;
	public GameObject boulette;
	public Direction direction; 
	public Vector2[] mouvements = new Vector2[4]{ 
		new Vector2(  0,  1 ), 
		new Vector2(  1,  0 ), 
		new Vector2(  0, -1 ), 
		new Vector2( -1,  0 ) 
	};
	public bool player = false;
	public Texture2D playerTexture; 
	public Texture2D zombieTexture; 
	
	public static Vector2 startTarget;
	public static bool dead = false;
	
	private Transform tr;
	private Renderer rdr;
	private Rigidbody rb;
	private Vector2 target;
	private Vector2 current = new Vector2( 4.5f, 4.5f );
	private int step = 0;
	private float duration;
	private TextMesh scoreMesh;
	private Transform deadScreen;
	private int score;
	private float center;
	

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 30;
		tr = transform;
		rb = rigidbody;
		rdr = tr.GetChild(0).renderer;
		scoreMesh = GameObject.FindGameObjectWithTag( "Score" ).GetComponent<TextMesh>();
		deadScreen = GameObject.FindGameObjectWithTag( "Dead" ).transform;
		score = 0;
		center = Screen.width / 2;
		duration = nbSteps*Time.fixedDeltaTime;
		direction = Direction.right;
		current = new Vector2( tr.position.x, tr.position.z );
		if( player )
			target = current + mouvements[ (int)direction ];
		else
			target = startTarget;
		setPlayer( player );
	}
	
	// Update is called once per frame
	void Update () {
		if( !player )
			return;
		if( dead ) {
			return;
		}
		if( Input.GetKeyDown( KeyCode.Space ) ) {
			
		}
		if( Input.GetKeyDown( KeyCode.RightArrow ) || ( Input.GetMouseButtonDown(0) && Input.mousePosition.x >= center ) ) {
			direction = (Direction)(( (int)direction + 1 + 4 ) % 4);
			target = current + mouvements[ (int)direction ];
		}
		if( Input.GetKeyDown( KeyCode.LeftArrow ) || ( Input.GetMouseButtonDown(0) && Input.mousePosition.x < center ) ) {
			direction = (Direction)(( (int)direction - 1 + 4 ) % 4);
			target = current + mouvements[ (int)direction ];
		}
		AddToScore( (int)(Time.deltaTime * pointSecond) );
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
			next.GetComponent<playerScript>().target = current;
		}
		if( player )
			target = current + mouvements[ (int)direction ];
	}
	
	public void createBoulette( GameObject boulette ) {
		if( next != null )
			next.GetComponent<playerScript>().createBoulette( boulette );
		else {
			playerScript.startTarget = Vector2.zero;
			next = (GameObject)Instantiate( this.gameObject, new Vector3( current.x, 0, current.y ), Quaternion.identity );
			next.GetComponent<playerScript>().player = false;
		}
	}
	
	
	void OnTriggerEnter( Collider other ) {
		if( !this.player )
			return;
		if( other.gameObject.tag == "Souris" ) {
			createBoulette( boulette );
			other.gameObject.transform.position = new Vector3( -500, 500, -500 );
			AddToScore( pointSouris );
		}
		if( other.gameObject.tag == "Wall" ) {
			Die();
		}
		if( other.gameObject.tag == "Player" ) {
			playerScript boule = other.gameObject.GetComponent<playerScript>();
			if( !boule.player && boule.target != Vector2.zero ){
				Die();
			}
		}
	}
	
	void Die() {
		Application.targetFrameRate = 3;
		deadScreen.position = Vector3.zero;
		dead = true;
		popSourisScript.dead = true;
		rb.velocity = Vector3.zero;
	}
	
	void AddToScore( int points ) {
		score += points;
		if( score > 999999999 )
			score = 999999999;
		int scoreTemp = score;
		int millions = Mathf.CeilToInt( scoreTemp / 1000000 );
		scoreTemp -= millions * 1000000;
		int thousands = Mathf.CeilToInt( scoreTemp / 1000 );
		scoreTemp -= thousands * 1000;
		
		string millionsStr = millions.ToString();
		while( millionsStr.Length < 3 )
			millionsStr = millionsStr.Insert(0,"0");
		string thousandsStr = thousands.ToString();
		while( thousandsStr.Length < 3 )
			thousandsStr = thousandsStr.Insert(0,"0");
		string zerosStr = scoreTemp.ToString();
		while( zerosStr.Length < 3 )
			zerosStr = zerosStr.Insert(0,"0");
		
		scoreMesh.text = millionsStr + " " + thousandsStr + " " + zerosStr;
	}
	
	void setPlayer( bool player ) {
		this.player = player;
		if( player ) {
			rdr.material.mainTexture = playerTexture;
		} else {
			rdr.material.mainTexture = zombieTexture;
		}
	}
	
}
