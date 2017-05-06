using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOtherAgent : MonoBehaviour {


    List<GameObject> otherAgents;
    public BaseAgent owner;
    public int foundAgents = 0;
    void Start()
    {
        otherAgents = new List<GameObject>();
    }
    GameObject FindNearestAgent()
    {
        Vector3 pos = transform.position;
        float minMagnitude = 9999999;
        GameObject nearest=null;
        foreach (GameObject g in otherAgents)
        {
           float mag=Vector3.SqrMagnitude(g.transform.position - pos);
           if(mag < minMagnitude)
           {
                minMagnitude = mag;
                nearest = g;
           }
        }
        return nearest;
    }
	void Update () {
        GameObject nearest = FindNearestAgent();
        if (nearest)
        {
            owner.destination = nearest.transform;
        }
        else
        {
            owner.destination = null;
        }
	}
	void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject==owner.gameObject)
        {
            return;
        }
        UnityEngine.AI.NavMeshAgent other_agent =collision.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(other_agent)
        {
            otherAgents.Add(other_agent.gameObject);
            ++foundAgents;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        otherAgents.Remove(collisionInfo.gameObject);
    }
}
