using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBelow : MonoBehaviour
{
    private Ray r;

    void Start()
    {
       r= new Ray(this.transform.position, -this.transform.forward);
    }

    public static bool done = false;
    public void DoCheck()
    {
        r.origin = this.transform.position;
        r.direction=-this.transform.forward;
        if (!done)
        {
            RaycastHit[] h = Physics.SphereCastAll(r, .001f, 100.0f, 1 << LayerMask.NameToLayer("InteractibleObject"));
          //  Debug.DrawLine(r.origin,r.origin + r.direction * 100f, Color.green);
            int count = 0;
            print(h.Length);
            foreach (var v in h)
            {
                if (v.transform.name.Contains("Pumpkin"))
                {
                    count++;
                }
                else
                {
                    print(v.transform.name);
                }

                if (count >= 4)
                {
                    done = true;
                    FINISHED();
                }
            }
        }
    }

    void FINISHED()
    {
        print("FINISHED!!!");
    }
}
