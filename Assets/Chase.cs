using UnityEngine;
using System.Collections;
public class Chase : BaseAgent
{

	// Use this for initialization

	
	// Update is called once per frame
	void Update ()
	{
		agent.destination=destination.position;
	}
}

