using System.Collections;
using System.Collections.Generic;
using Scripts.Grab;
using UnityEngine;

public interface IControllerActionHandler
{
    Grab Grab { get; set; }
    Raycaster Raycaster { get; set; }

    Grab GrabHandler();
    Raycaster RaycastHandler();
}

public interface IRiftActionHandler : IControllerActionHandler
{
    RiftInputHandler Input { get; set; }

    RiftInputHandler InputHandler();
}

public interface IViveActionHandler : IControllerActionHandler
{
    ViveInputHandler Input { get; set; }

    ViveInputHandler InputHandler();

    Transform Player { get; set; }

    Shoot Shoot { get; set; }
}