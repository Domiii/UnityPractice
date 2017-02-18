var snippets = [

// Basic transform.Translate
  {
    title_en: 'Move Left',
    code: 
`void Update () {
  transform.Translate (-0.1f, 0, 0);
}`
  },
  {
    title_en: 'Move Right',
    code: 
`void Update () {
  transform.Translate (0.1f, 0, 0);
}`
  },
  {
    title_en: 'Move Up',
    code: 
`void Update () {
  transform.Translate (0, 0.1f, 0);
}`
  },
  {
    title_en: 'Move Down',
    code: 
`void Update () {
  transform.Translate (0, -0.1f, 0);
}`
  },
  {
    title_en: 'Move Forward',
    code: 
`void Update () {
  transform.Translate (0, 0, 0.1f);
}`
  },
  {
    title_en: 'Move Back',
    code: 
`void Update () {
  transform.Translate (0, 0, -0.1f);
}`
  },

// transform.Translate + Variables
  {
    title_en: 'MoveX',
    code: 
`public float SpeedX;

void Update () {
  transform.Translate (SpeedX, 0, 0);
}`
  },
  {
    title_en: 'MoveHorizontalSimple1',
    code: 
`public float SpeedX;
public float SpeedZ;

void Update () {
  transform.Translate (SpeedX, 0, SpeedZ);
}`
  },
  {
    title_en: 'MoveHorizontalAccurate1',
    code:
`public float SpeedX;
public float SpeedZ;

void FixedUpdate () {
  transform.Translate (SpeedX * Time.fixedDeltaTime, 0, SpeedZ * Time.fixedDeltaTime);
}`
  },
  {
    title_en: 'MoveHorizontalSimple2',
    code:
`public float SpeedX, SpeedZ;
Rigidbody body;

void Start() {
  body = GetComponent<Rigidbody> ();
}

void Update () {
  body.velocity = new Vector3(SpeedX, body.velocity.y, SpeedZ);
}`
  },
  {
    title_en: 'MoveHorizontalAccurate2',
    code:
`public float SpeedX, SpeedZ;
Rigidbody body;

void Start() {
  body = GetComponent<Rigidbody> ();
}

void FixedUpdate () {
  body.velocity = new Vector3(SpeedX, body.velocity.y, SpeedZ);
}`
  },
  {
    title_en: 'MoveWithKeyboard',
    code:
`public float Speed = 2;
Rigidbody body;

void Start() {
  body = GetComponent<Rigidbody> ();
}

void FixedUpdate () {
  // check where we are moving
  var moveX = Input.GetAxisRaw("Horizontal");
  var moveZ = Input.GetAxisRaw("Vertical");

  // compute horizontal velocity
  var move = new Vector3 (moveX, 0, moveZ);
  move.Normalize ();
  move *= Speed;

  // keep vertical speed
  move.y = body.velocity.y;

  body.velocity = move;
}`
  },
  {
    title_en: 'MoveWithKeyboard2',
    code:
`public float ForwardSpeed = 2;
public float RotationSpeed = 180;
Rigidbody body;

void Start() {
  body = GetComponent<Rigidbody> ();
}

void FixedUpdate () {
  // check where we are moving
  var rotation = Input.GetAxisRaw("Horizontal");
  var forward = Input.GetAxisRaw("Vertical");

  // rotate
  transform.Rotate(Vector3.up, RotationSpeed * rotation * Time.fixedDeltaTime);

  // compute forward velocity
  var move = forward * transform.forward;
  move.y = 0;
  move.Normalize ();
  move *= ForwardSpeed;

  // keep vertical speed
  move.y = body.velocity.y;

  body.velocity = move;
}`
  },
  
  
  
  
  
  {
    title_en: 'Test any 2D collisions',
    code:
`void OnCollisionEnter2D(Collision2D other) {
  print ("Enter: " + other.gameObject.name);
}
 
void OnCollisionExit2D(Collision2D other) {
  print ("Exit: " + other.gameObject.name);
}`
  },
  {
    title_en: 'Test any 3D collisions',
    code:
`void OnCollisionEnter(Collision other) {
  print ("Enter: " + other.gameObject.name);
}
 
void OnCollisionExit(Collision other) {
  print ("Exit: " + other.gameObject.name);
}`
  },
  {
    title_en: 'Count 2D collisions',
    name: 'CollisionCounter2D',
    code:
`public int collisionCount; 
 
void OnCollisionEnter2D(Collision2D other) {
  ++collisionCount;
}
 
void OnCollisionExit2D(Collision2D other) {
  --collisionCount;
}`
  },
  {
    title_en: '2D keyboard movement + jumping',
    refs: ['CollisionCounter2D'],
    code:
`public float speed = 3;
public float jumpStrength = 9;
public int colliderCount;
 
void Update() {
  var body = GetComponent<Rigidbody2D> ();
  var v = body.velocity; 
  v.x = Input.GetAxis ("Horizontal") * speed;
 
  if (Input.GetKeyDown (KeyCode.Space) && colliderCount > 0) {
    v.y = jumpStrength;
  }
 
  body.velocity = v;
}`
  },
  {
    name: 'PlayerFeet2D',
    code:
`public Player player;
 
void OnTriggerEnter2D(Collider2D collider) {
  ++player.groundColliders;
}
 
void OnTriggerExit2D(Collider2D collider) {
  --player.groundColliders;
}`
  },
  {
    name: 'Complete2DKeyboardMovement',
    refs: ['PlayerFeet2D']
    note: 'You still need to add the code for counting the `groundColliders`.',
    code:
`public float speed = 3;
public float jumpStrength = 9;
public int groundColliders;
 
void Update() {
  var body = GetComponent<Rigidbody2D> ();
  var v = body.velocity;
 
  v.x = Input.GetAxis ("Horizontal") * speed;
 
  if (Input.GetKeyDown (KeyCode.Space) && groundColliders > 0) {
    v.y = jumpStrength;
  }
 
  body.velocity = v;
 
  //  face current walking direction
  if (v.x != 0) {
    var scale = transform.localScale;
    scale.x = Mathf.Sign (v.x) * Mathf.Abs(scale.x);
    transform.localScale = scale;
  }
}`
  },
  {
    name: 'PlayerColliderTest2D',
    title_en: 'Only do something when colliding with player',
    code:
`void OnTriggerEnter2D(Collider2D other) {
  var player = other.gameObject.GetComponentInParent<Player>();
  if (player != null) {
    print (player.name);
  }
}`
  },
  {
    name: 'TimeTest',
    code:
`public class Test : MonoBehaviour {
  public float secondsSinceStart;
 
  void Update () {
    secondsSinceStart += Time.deltaTime;
  }
}`
  },
  {
    title: 'CameraMoverX',
    code:
`public float xDirection;
Player player;
 
void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.gameObject.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    player = triggerPlayer;
  }
}
 
void OnTriggerExit2D(Collider2D other) {
  var triggerPlayer = other.gameObject.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    player = null;
  }
}
 
void FixedUpdate() {
  if (player != null) {
    var xSpeed = xDirection * player.speed;
    Camera.main.transform.Translate (xSpeed *  Time.fixedDeltaTime, 0, 0);
  }
}`
  },
  {
    title_en: 'SpeedPickup2D',
    code:
`public float speedFactor = 2;

void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player picked it up! 
    triggerPlayer.speed *= speedFactor;
    Destroy (gameObject);
  }
}`
  },
  {
    title_en: 'SlowTrap',
    code:
`public float speedFactor = 0.5f;

void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered!
    triggerPlayer.speed *= speedFactor;
  }
}

void OnTriggerExit2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player exited!
    triggerPlayer.speed /= speedFactor;
  }
}`
  },
  {
    name: 'DeathTrap',
    code:
`void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered! (reset scene)
    print("You lose! :(");
    Scene scene = SceneManager.GetActiveScene(); 
    SceneManager.LoadScene(scene.name);
  }
}
`
  },
  {
    title_en: 'Show note when player enters trigger',
    code:
`void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered
    player = triggerPlayer;
    confirmNotice.SetActive (true);
  }
}
 
void OnTriggerExit2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player left
    confirmNotice.SetActive (false);
    player = null;
  }
}`
  },
  {
    title_en: 'Shooter',
    code:
`public GameObject bulletPrefab;
public Transform target;
public float shootDelay = 0.5f;

void Start () {
  InvokeRepeating ("Shoot", shootDelay, shootDelay);
}

void Shoot() {
  Instantiate (bulletPrefab, target.position, target.rotation);
}`
  },
  {
    name: 'RainMaker',
    code:
`public Rigidbody rainDropPrefab;
public float rainDropLifeTime = 2;
Collider collider;

void Start () {
	collider = GetComponent<Collider> ();
	collider.isTrigger = true;	// isTrigger = false makes no sense here
}

void Update () {
	DropOne ();
}

void DropOne() {
	var min = collider.bounds.min;
	var max = collider.bounds.max;
	var pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	var drop = Instantiate (rainDropPrefab, pos, Quaternion.identity);
	Destroy (drop.gameObject, rainDropLifeTime);
}`
  },
  {
    title_en: 'MoveToClickPoint',
    tags: ['navmesh'],
    code:
`using UnityEngine;
 
public class MoveToClickPoint : MonoBehaviour {
    NavMeshAgent agent;

    void Start() {
       agent = GetComponent<NavMeshAgent>();
    }
 
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
           RaycastHit hit;
           if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.destination = hit.point;
            }
        }
    }
}`
  },
  {
    title_en: '',
    code:
``
  },
  {
    title_en: '',
    code:
``
  },
  {
    title_en: '',
    code:
``
  },
];
