using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDetect : MonoBehaviour {
    public bool collided = false;
	// Use this for initialization
	void Start () {
		
	}

	public void OnTriggerStay(Collider col)
	{
        if (col.tag == "Wall")
        {
            collided = true;
            Debug.Log("col");
        }
	}
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Wall")
        {
            collided = false;
        }
    }
}
