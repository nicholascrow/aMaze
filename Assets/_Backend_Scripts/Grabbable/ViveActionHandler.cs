﻿using Scripts.Grab;
using UnityEngine;

public class ViveActionHandler : MonoBehaviour, IViveActionHandler
{
    public ViveInputHandler Input { get; set; }
    public Grab Grab { get; set; }
    public Raycaster Raycaster { get; set; }

    // Use this for initialization
    void Start()
    {
        SpawnControllerElements();
    }

    void SpawnControllerElements()
    {
        Input = gameObject.AddComponent<ViveInputHandler>();
        Grab = gameObject.AddComponent<Grab>();
        Raycaster = gameObject.AddComponent<Raycaster>();
    }


    public ViveInputHandler InputHandler()
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