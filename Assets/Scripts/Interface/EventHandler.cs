using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {
    public static bool onButton = false;
	// Use this for initialization
	void Start () {
        onButton = false;
    }
	public void mouseEnter()
    {
        onButton = true;
    }
    public void mouseLeave()
    {
        onButton = false;
    }
}
