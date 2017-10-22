using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavSomewhere : MonoBehaviour
{
    public float timeChange;
    public Transform changeLater;

    public Transform[] goal;
    private NavMeshAgent agent;


    private int hits = 0;
    private int position = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal[position].position;

        if (changeLater != null)
        {
            StartCoroutine(TimeToChange(timeChange));
        }
    }

    void Update()
    {
        agent.destination = goal[position % goal.Length].position;
        if (hits > 4)
        {
            agent.isStopped = true;
            GameObject g = Instantiate(deathspawn);
            g.transform.position = this.transform.position;
            //play death sound
            Destroy(this.gameObject);
        }
        if (agent.remainingDistance < 1.5f)
        {
            agent.destination = goal[position++ % goal.Length].position;
        }
    }

    public GameObject deathspawn;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name == "bullet")
        {
            hits++;
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator TimeToChange(float time)
    {
        yield return new WaitForSeconds(time);
        agent.destination = changeLater.transform.position;
    }
}
