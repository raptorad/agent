using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public GameObject selectionMarker;
    bool isSelected;
    void Start()
    {
        isSelected = false;
        selectionMarker.SetActive(false);
    }
    void setSelected(bool sel)
    {
        isSelected = sel;
        selectionMarker.SetActive(sel);
    }
}
