using UnityEngine;
using System.Collections;



public class ChaseUntilSee : BaseAgent
{

	public Transform eyes;

	// Use this for initialization

	void FixedUpdate()
	{
		if(CanSee())
		{
			agent.Stop();
		}
		else
		{
			agent.destination=destination.position;
			agent.Resume();
		}
	}
	bool CanSee()
	{
		RaycastHit hit;
		Vector3 dir=destination.position-eyes.position;
		if (Physics.Raycast(eyes.position,dir.normalized, out hit))
		{
			if(hit.collider.gameObject == destination.gameObject)
			{
				Debug.DrawLine(eyes.position, hit.point, Color.blue);
				return true;
			}
			return false;
		}
		return false;
	}

}