using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierWithParticles : MonoBehaviour {

    //these are the objects which the curve is shaped like (remove this)
    public GameObject o1, o2, o3;

    //set checking var to false
    bool isChecking = false;

    public int resolution = 10000;

    private ParticleSystem.Particle[] points;



    private void Start()
    {
        o1 = new GameObject("o1");
        o1.transform.SetParent(this.transform);
        o1.transform.position = this.transform.position;

        o2 = new GameObject("o2");
        o2.transform.SetParent(this.transform);
        o2.transform.position = this.transform.position + this.transform.forward * 1;

        o3 = new GameObject("o3");
        o3.transform.SetParent(this.transform);
        o3.transform.position = this.transform.position + this.transform.forward * 1 - this.transform.up * 1;

        points = new ParticleSystem.Particle[resolution]; 
        //set the curve
        Curve(o1.transform.position, o2.transform.position, o3.transform.position);
    }


    void Curve(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        int place = 0;
        for (float i = 0; i < 1; i += .001f)
        {
            points[place].position = GetPoint(i, p0, p1, p2);
            points[place].startColor = new Color(GetPoint(i, p0, p1, p2).x, GetPoint(i, p0, p1, p2).y, GetPoint(i, p0, p1, p2).z);
            points[place].startSize = .01f;
            place++;
            //  l.SetPosition(place++, GetPoint(i, p0, p1, p2));
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
        if (!isChecking)
            StartCoroutine(CheckCurve());

    }

    IEnumerator CheckCurve()
    {
        isChecking = true;
        Vector3 check1 = o1.transform.position;
        Vector3 check2 = o2.transform.position;
        Vector3 check3 = o3.transform.position;
        //Vector3 parPos = this.transform.parent.transform.position;
        yield return null;
        while (ValidXYZ(check1, o1.transform.position) && ValidXYZ(check2, o2.transform.position) && ValidXYZ(check3, o3.transform.position) )//&& this.transform.parent.transform.position == parPos)
        {
            // print("waiting");
            yield return null;
        }
        // print("updating");
        isChecking = false;
        Curve(o1.transform.position, o2.transform.position, o3.transform.position);

    }

    bool ValidXYZ(Vector3 first, Vector3 second)
    {
        if (!(first.x == second.x))
            return false;
        if (!(first.y == second.y))
            return false;
        if (!(first.z == second.z))
            return false;

        return true;
    }

    public Vector3 GetPoint(float time, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (Mathf.Pow((1 - time), 2) * p0) + (2 * (1 - time) * time * p1) + (Mathf.Pow(time, 2) * p2);
    }
}
