using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public BaseAgent baseAgent;
    public Transform hole;
    public GameObject bullet;
    public float shootInterval=0.5f;
    float shootTime=0;
    bool fireOpened;
    public LayerMask mask;
    void Start () {
		
	}
	public void OpenFire()
    {
        fireOpened = true;
    }
    public void CloseFire()
    {
        fireOpened = false;
    }
	void Update () {
        if(fireOpened)
        {
            transform.LookAt(baseAgent.destination);
            Shoot();
        }
        
	}
    void Shoot()
    {
        if(Time.time>shootTime && CanSee(baseAgent.destination.gameObject))
        {
            shootTime = Time.time + shootInterval;
            Instantiate(bullet, hole.transform.position, hole.transform.rotation);
        }
    }
    bool CanSee(GameObject tar)
    {
        RaycastHit hit;
        Vector3 dir = tar.transform.position - transform.position;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, Mathf.Infinity, mask.value))
        {
            if (hit.collider.gameObject == tar)
            {
                Debug.DrawLine(transform.position, hit.point, Color.blue);
                return true;
            }
            return false;
        }
        return false;
    }
}
