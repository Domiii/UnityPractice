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
