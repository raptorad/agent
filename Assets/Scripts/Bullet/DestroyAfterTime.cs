using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterTime : MonoBehaviour {

    public float lifeTime=0.5f;
    public float dyingTime = 0f;
    public UnityEvent dieEvent;
    float dieTime;
	// Use this for initialization
	void Start () {
        dieTime = lifeTime + Time.time;
        if (dieEvent == null)
            dieEvent = new UnityEvent();
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time>dieTime)
        {
            dieEvent.Invoke();
            Destroy(gameObject,dyingTime);
        }
	}
}
