using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour {

    public LayerMask mask;

    void Update() {
		if (Input.GetMouseButtonDown(0) && !EventHandler.onButton) {
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,mask.value)) {
				transform.position = hit.point;
			}
		}
	}

}
