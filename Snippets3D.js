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
    name: 'RollingCube',
    title_en: 'Let a 1x1x1 sized cube roll!',
    code:
`public float speed = 2;

public Transform left, right, forward, back, allRotators;


Quaternion rollRotation = Quaternion.Euler(new Vector3 (0, 0, 90));
float progress;
Quaternion startRotatorRotation, endRotatorRotation;

public bool IsRolling {
  get { return transform.parent != null; }
}

void Reset () {
  // create all rotators (make sure, their x-axis is pointing toward the center of the cube)
  allRotators = new GameObject ("AllRotators").transform;
  allRotators.position = transform.position;

  left = CreateRotator ("Left", Vector3.left * 0.5f, Quaternion.identity);
  right = CreateRotator ("Right", Vector3.right * 0.5f, Quaternion.Euler(new Vector3(0, 180, 0)));
  back = CreateRotator ("Back", Vector3.back * 0.5f, Quaternion.Euler(new Vector3(0, 270, 0)));
  forward = CreateRotator ("Forward", Vector3.forward * 0.5f, Quaternion.Euler(new Vector3(0, 90, 0)));
}

Transform CreateRotator(string name, Vector3 pos, Quaternion rotation) {
  var go = new GameObject(name);
  pos += Vector3.down * 0.5f;

  var newTransform = go.transform;
  newTransform.SetParent(transform);
  newTransform.localPosition = pos;
  newTransform.rotation = rotation;
  newTransform.SetParent(allRotators);

  return newTransform;
}

void FixedUpdate () {
  if (!IsRolling) {
    // not rolling -> We can start rolling
    var horizontal = Input.GetAxis ("Horizontal");
    var vertical = Input.GetAxis ("Vertical");
    if (horizontal < 0) {
      StartRoll (left);
    }
    else if (horizontal > 0) {
      StartRoll (right);
    }
    else if (vertical < 0) {
      StartRoll (back);
    }
    else if (vertical > 0) {
      StartRoll (forward);
    }
  } else {
    // keep on rolling!
    UpdateRoll();
  }
}


void StartRoll(Transform rotator) {
  // add cube to rotator
  transform.SetParent (rotator);

  // set start and end rotation
  startRotatorRotation = rotator.rotation;
  endRotatorRotation = rotator.rotation * rollRotation;
}


void UpdateRoll() {
  // update progress (let progress go from 0 to 1 in 1/speed seconds)
  progress = Mathf.Min(1, progress + speed * Time.deltaTime);

  // interpolate rotation between start and end rotations
  var progressTransform = transform.parent.transform;
  progressTransform.rotation = Quaternion.Lerp(startRotatorRotation, endRotatorRotation, progress);

  if (progress >= 1) {
    // finished rolling
    EndRoll();
  }
}


void EndRoll() {
  // move all rotators
  allRotators.position = transform.position;

  // reset progress & rotator's rotation
  var rotator = transform.parent;
  rotator.rotation = startRotatorRotation;
  progress = 0;


  // remove from parent
  transform.SetParent (null);
}`
  },
  {
    name: 'Elevator',
	  title_en: '',
		note: 'Quite advanced!',
    code: 
`/// <summary>
/// The distance between two floors
/// </summary>
public Vector3 FloorDistance = Vector3.up;
public float Speed = 1.0f;
public int Floor = 0;
public int MaxFloor = 1;
public Transform moveTransform;

private float tTotal;
private bool isMoving;
private float moveDirection;


// Use this for initialization
void Start () {
  moveTransform = moveTransform ?? transform;
}

// Update is called once per frame
void Update () {
  if (isMoving) {
    // elevator is moving
    MoveElevator();
  }
}

void MoveElevator() {
  var v = moveDirection * FloorDistance.normalized * Speed;
  var t = Time.deltaTime;
  var tMax = FloorDistance.magnitude / Speed;
  t = Mathf.Min (t, tMax - tTotal);
  moveTransform.Translate(v * t);
  tTotal += t;
  print (tTotal);

  if (tTotal >= tMax) {
    // we arrived on floor
    isMoving = false;
    tTotal = 0;
    Floor += (int)moveDirection;
    print (string.Format("elevator arrived on floor {0}!", Floor));
  }
}

/// <summary>
/// Start moving up one floor
/// </summary>
public void StartMoveUp() {
  if (isMoving)
    return;

  isMoving = true;
  moveDirection = 1;
}

/// <summary>
/// Start moving down one floor
/// </summary>
public void StartMoveDown() {
  if (isMoving)
    return;

  isMoving = true;
  moveDirection = -1;
}

/// <summary>
/// Tell the elevator to move up or down
/// </summary>
public void CallElevator() {
  if (isMoving)
    return;

  print ("elevator starts moving!");

  // start moving
  if (Floor < MaxFloor) {
    StartMoveUp ();
  }
  else {
    StartMoveDown ();
  }

}`
  },
  {
    name: 'ElevatorButton',
	  title_en: 'A Button for the Elevator!',
    code: 
`public Elevator Elevator;

void OnTriggerEnter(Collider collider) {
  var go = collider.gameObject;
  var player = go.GetComponent<Player> ();

  if (player != null && Elevator != null) {
    Elevator.CallElevator ();
  }
}`
  },
  {
    name: 'MoveWithTarget',
	  title_en: 'First add empty object (a "mount") exactly onto the target object. Then add this component to the "mount" object. Finally, add any objects you want it to follow (e.g. camera) as children to "mount".',
    code: 
`public Rigidbody target;
public float turnSpeed = 8;

Vector3 offset;
Vector3 targetDirection;

void Start () {
	offset = transform.position - target.transform.position;
	targetDirection = target.transform.forward;
}

void FixedUpdate () {
	if (target != null) {
		transform.position = target.transform.position + offset;
		var v = target.velocity;
		v.y = 0;
		if (v.sqrMagnitude > 0) {
			// cannot normalize zero vectors
			targetDirection = v;
			targetDirection.Normalize ();
		}
		transform.forward = Vector3.Slerp(transform.forward, targetDirection, Time.deltaTime * turnSpeed);
	}
}`
  },
  {
    name: 'Healthbar',
	  title_en: '',
		refs: [{
			name: 'Online step-by-step Healthbar tutorial',
			href: 'https://www.youtube.com/watch?v=1lrkgdENfqM&list=PLX-uZVK_0K_402gTvjaP5mIE8p5PFI1HD'
		}],
    code: 
`public Player unit;
public Color goodColor;
public Color badColor;

Image image;

void Reset() {
	goodColor = Color.Lerp(Color.green, new Color(0,255,0,0), 0.6f);
	badColor = Color.Lerp(Color.red, new Color(255,0,0,0), 0.6f);
}

void Start() {
	image = GetComponent<Image> ();
	if (image == null) {
		var sprite = GetComponent<Sprite> ();
	}
}

void Update() {
	var ratio = unit.Health / unit.MaxHealth;

	// set color
	var color = Color.Lerp(badColor, goodColor, ratio);
	image.color = color;

	// set size
	image.fillAmount = ratio;
}`
  },
  {
    name: '',
	title_en: '',
    code: 
``
  },
  {
    name: '',
	title_en: '',
    code: 
``
  },
  {
    name: '',
	title_en: '',
    code: 
``
  },
];
