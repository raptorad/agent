using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ChaseUntilSee : BaseAgent
{

	public Transform eyes;
    public UnityEvent seeEvent;
    public UnityEvent cantSeeEvent;
    public LayerMask mask;
	// Use this for initialization
    protected void Start()
    {
        base.Start();
        if (seeEvent == null)
            seeEvent = new UnityEvent();
        if (cantSeeEvent == null)
            cantSeeEvent = new UnityEvent();
    }
	void FixedUpdate()
	{
		if(CanSee())
		{
            seeEvent.Invoke();
			agent.Stop();
		}
		else
		{
            cantSeeEvent.Invoke();
			agent.destination=destination.position;
			agent.Resume();
		}
	}
	bool CanSee()
	{
		RaycastHit hit;
		Vector3 dir=destination.position-eyes.position;
		if (Physics.Raycast(eyes.position,dir.normalized, out hit,Mathf.Infinity,mask.value))
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