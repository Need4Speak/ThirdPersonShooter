using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    public GameObject particle = null; //接触点
    protected NavMeshAgent agent;
    protected Animator animator;

    protected Object particleClone;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        animator = GetComponent<Animator>();
        particleClone = null;
    }

    protected void SetDestination()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit))
        {
            if(particleClone != null)
            {
                GameObject.Destroy(particleClone);
                particleClone = null;
            }

            Quaternion quaternion = new Quaternion();
            quaternion.SetLookRotation(hit.normal, Vector3.forward);
            particleClone = Instantiate(particle, hit.point, quaternion);

            agent.destination = hit.point;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SetDestination();
        }
    }
}
