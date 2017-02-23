var snippets = [

// Basic transform.Translate
  {
    name: 'MoveLeft',
    code: 
`void Update () {
  transform.Translate (-0.1f, 0, 0);
}`
  },
  {
    name: 'Move Right',
    code: 
`void Update () {
  transform.Translate (0.1f, 0, 0);
}`
  },
  {
    name: 'Move Up',
    code: 
`void Update () {
  transform.Translate (0, 0.1f, 0);
}`
  },
  {
    name: 'Move Down',
    code: 
`void Update () {
  transform.Translate (0, -0.1f, 0);
}`
  },
  {
    name: 'Move Forward',
    code: 
`void Update () {
  transform.Translate (0, 0, 0.1f);
}`
  },
  {
    name: 'Move Back',
    code: 
`void Update () {
  transform.Translate (0, 0, -0.1f);
}`
  },

// transform.Translate + Variables
  {
    name: 'MoveX',
    code: 
`public float SpeedX;

void Update () {
  transform.Translate (SpeedX, 0, 0);
}`
  },
  {
    name: 'MoveHorizontal1',
    code:
`public float SpeedX;
public float SpeedZ;

void FixedUpdate () {
  transform.Translate (SpeedX * Time.fixedDeltaTime, 0, SpeedZ * Time.fixedDeltaTime);
}`
  },
  {
    name: 'MoveHorizontal2',
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
    name: 'MoveWithKeyboard',
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
    name: 'MoveWithKeyboard2',
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
    name: 'CollisionTester',
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
    title_en: 'Count collisions',
    name: 'CollisionCounter',
    code:
`public int collisionCount; 
 
void OnCollisionEnter(Collision other) {
  ++collisionCount;
}
 
void OnCollisionExit(Collision other) {
  --collisionCount;
}`
  },
  {
    name: 'KeyboardMove',
    title_en: 'Keyboard movement + jumping',
    code:
`public float speed = 3;
public float jumpStrength = 9;
public int colliderCount;
 
void Update() {
  var body = GetComponent<Rigidbody> ();
  var v = body.velocity; 
  v.x = Input.GetAxis ("Horizontal") * speed;
  v.z = Input.GetAxis ("Vertical") * speed;
 
  if (Input.GetKeyDown (KeyCode.Space) && colliderCount > 0) {
    v.y = jumpStrength;
  }
 
  body.velocity = v;
}
 
void OnCollisionEnter(Collision other) {
  ++collisionCount;
}
 
void OnCollisionExit(Collision other) {
  --collisionCount;
}`
  },
  {
    name: 'PlayerFeet',
    code:
`public Player player;
 
void OnTriggerEnter(Collider collider) {
  ++player.groundColliders;
}
 
void OnTriggerExit(Collider collider) {
  --player.groundColliders;
}`
  },
  {
    name: 'PlayerColliderTest',
    title_en: 'Only do something when colliding with player',
    code:
`void OnTriggerEnter(Collider other) {
  var player = other.gameObject.GetComponent<Player>();
  if (player != null) {
    print (player.name);
  }
}`
  },
  {
    name: 'TimerTest',
    code:
`public float secondsSinceStart;

void Update () {
  secondsSinceStart += Time.deltaTime;
}`
  },
  {
    name: 'SpeedPickup',
    code:
`public float speedFactor = 2;

void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player picked it up!
    triggerPlayer.speed *= speedFactor;
    Destroy (gameObject);
  }
}`
  },
  {
    name: 'SlowTrap',
    code:
`public float speedFactor = 0.5f;

void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player entered!
    triggerPlayer.speed *= speedFactor;
  }
}

void OnTriggerExit(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player exited!
    triggerPlayer.speed /= speedFactor;
  }
}`
  },
  {
    name: 'DeathTrap',
    code:
`void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player entered! (reset scene)
    print("You lose! :(");
    Scene scene = SceneManager.GetActiveScene(); 
    SceneManager.LoadScene(scene.name);
  }
}`
  },
  {
    name: 'ShowOnEnter',
    title_en: 'Only show a GameObject when player enters trigger',
    code:
`// 確認通知
  public GameObject confirmNotice;
  Player player;

  void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player entered
    player = triggerPlayer;
    confirmNotice.SetActive (true);
  }
}
 
void OnTriggerExit(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player left
    confirmNotice.SetActive (false);
    player = null;
  }
}`
  },
  {
    name: 'Shooter',
    code:
`public GameObject bulletPrefab;
public Transform bulletStartPlace;
public float shootDelay = 0.5f;

void Start () {
  InvokeRepeating ("Shoot", shootDelay, shootDelay);
}

void Shoot() {
  Instantiate (bulletPrefab, bulletStartPlace.position, bulletStartPlace.rotation);
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
  collider.isTrigger = true;  // isTrigger = false makes no sense here
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
    name: 'MoveToClickPoint',
    tags: ['navmesh'],
    code:
`NavMeshAgent agent;

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
}`
  },
  {
    name: 'Bomb',
    code:
`public float radius = 10.0F;
public float power = 100.0F;

void OnCollisionEnter() {
  Explode ();
  Destroy (gameObject);
}

void Explode() {
  Vector3 explosionPos = transform.position;
  Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
  foreach (Collider hit in colliders) {
    Rigidbody rb = hit.GetComponent<Rigidbody>();

    if (rb != null)
      rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

  }
}`
  },
  {
    name: 'WallSpawner',
    code:
`public int Nx = 10;
public int Ny = 10;
public float CubeSize = 0.5f;
public Material[] Materials = new Material[0];
private float Gap = 0.01f;

void Start () {
  if (transform.childCount == 0) {
    CreateBricks ();
  }
}

public void CreateBricks() {
  for (var j = 0; j < Ny; ++j) {
    var m = j % Materials.Length;
    for (var i = 0; i < Nx; ++i) {
      var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
      cube.transform.SetParent(transform, false);
      cube.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);

      var x = i * (CubeSize + Gap) + Gap;
      var y = j * (CubeSize + Gap) + Gap;

      cube.transform.localPosition = new Vector3 (x, y, 0);
      cube.AddComponent<Rigidbody>();
      cube.AddComponent<BoxCollider>();

      cube.GetComponent<MeshRenderer> ().material = Materials[m];
      m = (m + 1) % Materials.Length;
    }
  }
}`
  },
  {
    name: 'NoGravityZone',
    code:
`Collider coll;
void Start() {
    coll = GetComponent<Collider>();
    coll.isTrigger = true;
}
void OnTriggerEnter(Collider other) {
    if (other.attachedRigidbody)
        other.attachedRigidbody.useGravity = false;
    
}
void OnTriggerExit(Collider other) {
    if (other.attachedRigidbody)
        other.attachedRigidbody.useGravity = true;
    
}`
  },
  {
    name: 'SpeedPickupWConfirmation',
    code:
`// player speed multiplier when picked up
public float speedFactor = 2;
// 確認通知
public GameObject confirmNotice;

Player player;

void Update() {
  if (player != null && Input.GetKeyDown(KeyCode.E)) {
    player.speed *= speedFactor;
    Destroy (gameObject);
  }
}

void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null) {
    // player entered the PickUp
    player = triggerPlayer;
    confirmNotice.SetActive (true);
  }
}

void OnTriggerExit(Collider other) {
  var triggerPlayer = other.GetComponent<Player> ();
  if (triggerPlayer != null && triggerPlayer == player) {
    confirmNotice.SetActive (false);
    player = null;
  }
}`
  },
  {
    name: 'Sun',
    title_en: '',
    note: 'Increase Y value of AxisOfRotation to reduce the angle, increase X to increase it.',
    code:
`/// <summary>
/// Virtual time of day from 0 = 00:00 to 1 = 00:00 on the next day
/// </summary>
public float VirtualTimeOfDay;
public float RealSecondsPerDay = 10;
public Vector3 AxisOfRotation = new Vector3 (2, 1, 0);

void Start ()
{
  // get current time of day
  var systemTimeOfDay = System.DateTime.Now.TimeOfDay;
  VirtualTimeOfDay = (float)systemTimeOfDay.TotalDays;

  AxisOfRotation.Normalize (); // normalize axis
}

void Update ()
{
  // advance time of day
  var timeDiff = Time.deltaTime;
  var virtualTimeDiff = timeDiff / RealSecondsPerDay;
  VirtualTimeOfDay += virtualTimeDiff;
  VirtualTimeOfDay = Mathf.Repeat (VirtualTimeOfDay, 1);

  // update rotation
  transform.localRotation = Quaternion.AngleAxis (360 * VirtualTimeOfDay, AxisOfRotation);
}`
  },
  {
    name: 'HaloPickup',
    title_en: 'Hovers over player\'s head when picked up.',
    code:
`public Player player;
public Vector3 relativePosition = new Vector3(0, 1, 0);
public Vector3 bobbingRadius = new Vector3(1, 0.4f, 1);
public float bobbingSpeed = 0.5f;

Vector2 startPos;

public bool IsEquipped {
	get {
		return player != null;
	}
}

void Start() {
	startPos = transform.position;
}

void Update() {
	if (IsEquipped) {
		Hover ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			Unequip ();
		}
	}
}

void Hover() {
	var pos = player.transform.position + relativePosition;
	var theta = Time.time * 2 * Mathf.PI * bobbingSpeed;
	pos.x += Mathf.Sin(theta) * bobbingRadius.x;
	pos.z += Mathf.Cos(theta) * bobbingRadius.z;
	pos.y += Mathf.Sin(0.5f * theta) * bobbingRadius.y;
	transform.position = pos;
}

void OnTriggerEnter(Collider other) {
	if (IsEquipped) {
		// don't do anything when already equipped
		return;
	}

	var triggerPlayer = other.GetComponentInParent<Player> ();
	if (triggerPlayer != null) {
		Equip (triggerPlayer);
	}
}

void Equip(Player triggerPlayer) {
	player = triggerPlayer;
	transform.localScale /= 2;
}

/// <summary>
/// Player dropped (放下) the halo
/// </summary>
void Unequip() {
	transform.localScale *= 2;
	player = null;
	transform.position = startPos;
}`
  },
  {
    name: '',
    title_en: '',
    code:
``
  }
];
