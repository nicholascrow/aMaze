using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public delegate void Teleported();
    public static Teleported justTeleported;

    public delegate void ValidTeleportLocation(Vector3 location);
    public static ValidTeleportLocation pointingToValidLocation;

    public delegate void InvalidTeleportLocation(Vector3 location);
    public static InvalidTeleportLocation pointingToInvalidLocation;

    //the controller using this script
    public OVRInput.Controller controller;

    public OVRInput.Axis1D button;

    //the oculus game object
    public GameObject user;

    RaycastHit r;

    //so we dont jump around a lot
    bool isTiming = false;

    // Update is called once per frame
    void Update()
    {
        if (((OVRInput.GetConnectedControllers() & controller)) != controller)
        {
            print("Controller not connected.");
            return;
        }
        //draw ray to show where we raycast
        if(Physics.Raycast(transform.position, transform.forward, out r) && r.collider.name.Contains("floor") )
        {
         //   GetComponent<LineRenderer>().enabled = true;
            GetComponent<BezierWithParticles>().o1.transform.position = this.transform.position;
            GetComponent<BezierWithParticles>().o2.transform.position = Vector3.Lerp(this.transform.position, r.point, .7f) + Vector3.up * .5f;
            GetComponent<BezierWithParticles>().o3.transform.position = r.point;
            wireDrawVector = r.point;
            if (pointingToValidLocation != null) pointingToValidLocation(r.point);
        }
        else
        {
           // GetComponent<LineRenderer>().enabled = false;
            if (pointingToInvalidLocation != null && r.collider != null) pointingToInvalidLocation(r.point);
        }

        if (OVRInput.Get(button ,controller) > .6 && !isTiming) //fix controller mask?
        {

            isTiming = true;
            if (Physics.Raycast(transform.position, transform.forward, out r)
                && r.collider.name.Contains("floor")
               /* && Physics.SphereCastAll(new Ray(r.transform.position, Vector3.forward), 1f, 0f).Length == 1*/)
            {
                print("teleporting");
                Vector3 pos = r.point;
                pos.y = user.transform.position.y;
                user.transform.position = pos;
                user.transform.rotation = Quaternion.Euler(new Vector3(0, user.transform.rotation.eulerAngles.y, 0));
                //user.transform.rotation = Quaternion.identity;
                if (justTeleported != null)
                {
                    justTeleported();
                }
                StartCoroutine(Timer(.3f));
            }
            else
            {
                isTiming = false;
            }
        }
    }
    Vector3 wireDrawVector = Vector3.zero;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(wireDrawVector, 1f);
    }

    IEnumerator Timer(float time)
    {
        for(float i = 0; i < time; i += Time.deltaTime)
        {
            yield return null;
        }
        isTiming = false;

    }
}
