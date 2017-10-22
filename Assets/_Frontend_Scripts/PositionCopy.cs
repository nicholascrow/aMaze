using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCopy : MonoBehaviour
{

    public GameObject copyFrom;

    public Vector3 offset;


	// Use this for initialization
	void Start () {
	    if (copyFrom == null )
	    {
	        Debug.LogError("NULL COPY OBJECT");
	        Debug.Break();
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.transform.position = copyFrom.transform.position - offset;
	}
}
