using UnityEngine;
using System.Collections;
public class Chase : BaseAgent
{
	void Update ()
	{
		agent.destination=destination.position;
	}
}

