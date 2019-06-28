using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x <=4.5)
        {
            transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(0, 0, Time.deltaTime);
        }
	}
    void moveinx()
    {

    }
}
