using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLocation : MonoBehaviour,IInteractableObject
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public IInteractionObject Owner { get; set; }
    public void BeginInteractObject(IControllerActionHandler handler)
    {
        StartCoroutine(TimedTeleport());
    }

    private bool TimedTeleportComplete = false;
    private IEnumerator TimedTeleport()
    {
        yield return new WaitForSeconds(.1f);
        TimedTeleportComplete = true;
    }


    public void EndInteractObject(IControllerActionHandler handler)
    {
        StopCoroutine(TimedTeleport());
        if (TimedTeleportComplete)
        {
            RaycastHit? h = handler.Raycaster.TryRaycast(-1);
            if (h != null
                && h.Value.transform.GetComponent<IInteractableObject>() != null
                && h.Value.transform.GetComponent<IInteractableObject>().AsTeleportLocation() != null)
            {
                handler.AsViveActionHandler().Player.transform.position = h.Value.point;
                TimedTeleportComplete = false;
            }

        }
    }
}
