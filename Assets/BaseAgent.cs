using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class BaseAgent:MonoBehaviour
{
    protected UnityEngine.AI.NavMeshAgent agent;
    public Transform destination;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
