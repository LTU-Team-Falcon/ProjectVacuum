#pragma strict

var FlashingLight : Light;
FlashingLight.enabled = false;


function FixedUpdate () {

var RandomNumber = Random.value;

	if (RandomNumber<=.95){
	FlashingLight.enabled=true;

	}
	else FlashingLight.enabled=false;

}