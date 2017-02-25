var snippets2d = [
  {
    title_en: 'Test any 2D collisions',
  tags: ['2d'],
    code:
`void OnCollisionEnter2D(Collision2D other) {
  print ("Enter: " + other.gameObject.name);
}
 
void OnCollisionExit2D(Collision2D other) {
  print ("Exit: " + other.gameObject.name);
}`
  },
  {
    title_en: 'Count 2D collisions',
    name: 'CollisionCounter2D',
  tags: ['2d'],
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
  tags: ['2d'],
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
  tags: ['2d'],
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
    name: 'CompleteKeyboardMovement2D',
    refs: ['PlayerFeet2D'],
  tags: ['2d'],
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
  tags: ['2d'],
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
    title: 'CameraMoverX2D',
  tags: ['2d'],
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
  tags: ['2d'],
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
    title_en: 'SlowTrap2D',
  tags: ['2d'],
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
    title_en: 'SlowTrap',
  tags: ['2d'],
    code:
`public float speedFactor = 0.5f;

void OnTriggerEnter(Collider other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered!
    triggerPlayer.speed *= speedFactor;
  }
}

void OnTriggerExit(Collider other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player exited!
    triggerPlayer.speed /= speedFactor;
  }
}`
  },
  {
    name: 'DeathTrap2D',
  tags: ['2d'],
    code:
`void OnTriggerEnter2D(Collider2D other) {
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered! (reset scene)
    print("You lose! :(");
    Scene scene = SceneManager.GetActiveScene(); 
    SceneManager.LoadScene(scene.name);
  }
}`
  },
  {
    name: 'ShowOnEnter2D',
    title_en: 'Show note when player enters trigger',
  tags: ['2d'],
    code:
`// 確認通知
  public GameObject confirmNotice;
  Player player;

  void OnTriggerEnter2D(Collider2D other) {
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
    name: 'NoGravity2D',
    code:
`public Collider coll;
void Start() {
    coll = GetComponent<Collider>();
    coll.isTrigger = true;
}
void OnTriggerEnter2D(Collider2D other) {
    if (other.attachedRigidbody)
        other.attachedRigidbody.useGravity = false;
    
}
void OnTriggerExit2D(Collider2D other) {
    if (other.attachedRigidbody)
        other.attachedRigidbody.useGravity = true;
    
}`
  },
  {
    name: 'SpeedPickup2D',
  tags: ['2d'],
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

void OnTriggerEnter2D(Collider2D other) {
  // maybe you have to change GetComponentInParent to GetComponent
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null) {
    // player entered the PickUp
    player = triggerPlayer;
    confirmNotice.SetActive (true);
  }
}

void OnTriggerExit2D(Collider2D other) {
  // maybe you have to change GetComponentInParent to GetComponent
  var triggerPlayer = other.GetComponentInParent<Player> ();
  if (triggerPlayer != null && triggerPlayer == player) {
    confirmNotice.SetActive (false);
    player = null;
  }
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
  }
];
