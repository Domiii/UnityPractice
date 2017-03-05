# UnityPractice

## Random Snippet TODO list

### Basic Stuff

* Scene refresh + Scene change
* GameManager -> Score counting
* Playing with physical buttons

### UI

* Light switch
* Quiz UI


### Colliders, Meshes + Materials

* Getting + working with a Collider programmatically
* Working with bounding box
* Getting + working with a Mesh programmatically
* Modifying a mesh
* Programmatic terrain


### Bigger Experiments + Tutorials

* How to do a rule-based AI?



### Some missing scripts


// TODO: LevelManager

// PlaySoundOnClick.cs

using UnityEngine;
using System.Collections;

public class PlaySoundOnClick : MonoBehaviour {
    void OnMouseDown() {
        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null) {
            audioSource.Play();
        }
    }
}

// ####################################
// MoveToDestination.cs
// ####################################

using UnityEngine;
using System.Collections;
 
public class MoveToDestinationAction : AIAction {
  public Vector3 destination;
}

/// <summary>
/// Move to destination, don't let anything distract you from that.
/// </summary>
[RequireComponent(typeof(NavMeshMover))]
public class MoveToDestination : AIStrategy<MoveToDestinationAction> {
  NavMeshMover mover;

  void Awake () {
    mover = GetComponent<NavMeshMover> ();
  }

  #region Public
  public Vector3 CurrentDestination {
    get { 
      return mover.CurrentDestination;
    }
  }

  public override void StartBehavior(MoveToDestinationAction action) {
    mover.CurrentDestination = action.destination;
    mover.StopMovingAtDestination = true;
  }
  #endregion

  void Update () {
    if (mover.HasArrived) {
      StopStrategy ();
    }
  }

  /// <summary>
  /// Called when finished moving.
  /// </summary>
  protected override void OnStop() {
    mover.StopMove ();
  }
}

// ####################################
// More
// ####################################

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
``
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
    name: 'ColorOnCollision',
    title_en: '',
    code: 
`Renderer ownRenderer;

void Start() {
  ownRenderer = GetComponent<Renderer> ();
}

void OnCollisionEnter(Collision other) {
  if (other.gameObject.GetComponent<Player>() != null) {
    // color when colliding with player!
    var otherMaterial = other.gameObject.GetComponent<Renderer> ().material;
    ownRenderer.material.Lerp (ownRenderer.material, otherMaterial, 0.5f);
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
];

