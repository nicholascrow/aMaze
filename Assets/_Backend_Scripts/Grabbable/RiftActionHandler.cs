using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Scripts.Grab;
using UnityEngine;

public class RiftActionHandler : MonoBehaviour,IRiftActionHandler
{
    public RiftInputHandler Input { get; set; }
    public Grab Grab { get; set; }
    public Raycaster Raycaster { get; set; }

    // Use this for initialization
    void Start () {
		SpawnControllerElements();
	}

    void SpawnControllerElements()
    {
        Input = gameObject.AddComponent<RiftInputHandler>();
        Grab = gameObject.AddComponent<Grab>();
        Raycaster = gameObject.AddComponent<Raycaster>();
    }

  
    public RiftInputHandler InputHandler()
    {
        return Input;
    }


    public Grab GrabHandler()
    {
        return Grab;
    }

    public Raycaster RaycastHandler()
    {
        return Raycaster;
    }
}   