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
        
    }

    private bool TimedTeleportComplete = false;
    private IEnumerator TimedTeleport()
    {
        yield return new WaitForSeconds(1f);

    }


    public void EndInteractObject(IControllerActionHandler handler)
    {
        if (TimedTeleportComplete)
        {
            RaycastHit? h = handler.Raycaster.TryRaycast(-1 << LayerMask.NameToLayer("Teleportable"));
            if (h != null && h.Value.transform.GetComponent<TeleportLocation>())
                handler.AsViveActionHandler().Player.transform.position = h.Value.point;
        }
    }
}
