using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RunAway: BaseAgent {
	
	public float runDistance=1;

	void Update () {
        if (!destination) return;
		Vector3 offset = destination.position - transform.position;
		float sqrLen = offset.sqrMagnitude;
		if (sqrLen < runDistance * runDistance)
		{
			var heading = -destination.position + transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance; // This is now the normalized direction.

			agent.destination=transform.position+direction*runDistance;
		}
	}
}
