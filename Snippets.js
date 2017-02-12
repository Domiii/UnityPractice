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
    title_en: 'MoveYZ',
    code: `
public float SpeedY;
public float SpeedZ;

void Update () {
  transform.Translate (0, SpeedY, SpeedZ);
}`
  },
  {
    title_en: 'VelocityControl',
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
