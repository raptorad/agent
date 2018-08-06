using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Command
{
    public Transform destination;
    public Vector3 position;
    public bool looped;
    public override string ToString()
    {
        string ret="";
        if (destination == null)
        {
            ret += "destination: NULL";
        }
        else
        {
            ret += "destination: " + destination.name;
        }
        ret += ", ";
        ret += "lopped: " + looped.ToString();
        ret += ", ";
        ret += "position: " + position.ToString();
        return ret;
    }

}
