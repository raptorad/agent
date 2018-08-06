using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour {
    public GameObject toCreate;
    public Transform outPos;
	public void Create()
    {
        Instantiate(toCreate, outPos.position, toCreate.transform.rotation);
    }
}
