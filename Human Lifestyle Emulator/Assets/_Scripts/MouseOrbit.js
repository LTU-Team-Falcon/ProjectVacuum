var target : Transform;
var distance = 10.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

public var count = 0;
public var PlayerSize;



private var x = 0.0;
private var y = 0.0;

public var Players : GameObject[];

@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
		
	Players = GameObject.FindGameObjectsWithTag("Player");
	PlayerSize = Players.Length;	
	target = Players[count].transform;
		
}

function Update() {

	if (count == PlayerSize)
	{
		count = 0;	
	}

	if(Input.GetMouseButtonDown(0))
	{
		PlayerSize = Players.Length;
		Players = GameObject.FindGameObjectsWithTag("Player");
		target = Players[count].transform;	
		count++;
	}

}

function LateUpdate () {
    if (target) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
 		
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * Vector3(0.0, 0.0, -distance) + target.position;
        
        transform.rotation = rotation;
        transform.position = position;
    }
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);

}