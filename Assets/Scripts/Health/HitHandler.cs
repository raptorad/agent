using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HitHandler : MonoBehaviour {

    public UnityEvent hitEvent;
	// Use this for initialization
	void Start () {
        if (hitEvent == null)
            hitEvent = new UnityEvent();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
        hitEvent.Invoke();
    }

}
