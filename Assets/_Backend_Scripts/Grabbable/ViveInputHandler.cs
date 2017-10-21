using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveInputHandler : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;

    public OVRInput.Controller Controller;
    public IViveActionHandler ActionHandler;


    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        ActionHandler = GetComponent<IViveActionHandler>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            IndexTriggerDown();
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            IndexTriggerUp();
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            IndexTriggerHeld();
        }

    }


    public void HandTriggerDown()
    {

    }
    public void HandTriggerHeld()
    {

    }
    public void HandTriggerUp()
    {

    }

    public void IndexTriggerDown()
    {
        ActionHandler.Raycaster.BeginInteract();
    }
    public void IndexTriggerHeld()
    {

    }
    public void IndexTriggerUp()
    {
        ActionHandler.Raycaster.EndInteract();
    }

    public void ThumbStickTouchBegin(Vector3 position)
    {

    }
    public void ThumbstickTouchEnd()
    {

    }
    public void ThumbStickTouching(Vector3 position)
    {

    }

    public void ThumbStickPressBegin(Vector3 position)
    {

    }
    public void ThumbStickPressEnd()
    {

    }
    public void ThumbStickPressing(Vector3 position)
    {

    }
}