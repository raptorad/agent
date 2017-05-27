using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {
    public float speed = 10;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    
    void Update () {
		
	}
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
