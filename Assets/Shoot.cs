using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootGun(GameObject gunGameObject)
    {
    BulletPoint p = gunGameObject.GetComponentInChildren<BulletPoint>();
    GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.position = p.transform.position;
        var rb = bullet.AddComponent<Rigidbody>();
        bullet.transform.localScale = Vector3.one * .015f;
        bullet.name = "bullet";
       // Debug.DrawRay(p.transform.position, gunGameObject.transform.forward, Color.yellow, 100f);
        rb.AddForce(-gunGameObject.transform.right * 30,ForceMode.Impulse);
    }
}
