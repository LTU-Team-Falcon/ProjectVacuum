﻿#pragma strict
 
// this is the thing we're gravitationally attracted to
var attractor : FauxGravityAttractor;

// are we touching the surface?
var grounded : int;

function Start () {
    rigidbody.WakeUp();
    rigidbody.useGravity = false;
}

 

// obviously this is crude since we might want to be able to stand on (and jump off) random objects
// should probably filter based on tag in future
function OnCollisionEnter (c : Collision) {
    if( c.gameObject.layer == 10 ){
        grounded ++;
    }
}

function OnCollisionExit (c : Collision) {
    if( c.gameObject.layer == 10 && grounded > 0 ){
        grounded --;
    }
}

function FixedUpdate () {
    if(attractor){
        attractor.Attract(this);
    }
}


@script RequireComponent(Rigidbody)