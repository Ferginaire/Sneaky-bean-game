using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAi : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject boi;
    [SerializeField]
    GameObject eyeMid;
    [SerializeField]
    GameObject eyeLeft;
    [SerializeField]
    GameObject eyeRight;
    [SerializeField]
    float viewDist;
    [SerializeField]
    GameObject start;
    [SerializeField]
    GameObject destination;
    [SerializeField]
    float fovAngle;

    Vector3 initialPos;
    Quaternion initialRot;
    bool found = false;
    bool goingTo = true;
    NavMeshAgent agent;
    RaycastHit hit1;
    RaycastHit hit2;
    RaycastHit hit3;
    // Start is called before the first frame update
    private void Start()
    {
        agent = boi.GetComponent<NavMeshAgent>();
        initialPos = boi.transform.position;
        initialRot = boi.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        eyeLeft.transform.localEulerAngles = new Vector3(0, -fovAngle, 0);
        eyeRight.transform.localEulerAngles = new Vector3(0, fovAngle, 0);
        if (found == false)
        {
            CheckForTurn();

            SetDestination();
        }
        else // if found
        {
            agent.SetDestination(boi.transform.position);
            //agent.isStopped = true;
        }

        DrawRays();

        CastRays();

    }

    void CheckForTurn()
    {
        if (boi.transform.position.z == destination.transform.position.z)
        {
            goingTo = false;
        }
        else if (boi.transform.position.z == start.transform.position.z)
        {
            goingTo = true;
        }
    }

    void SetDestination()
    {
        if (goingTo)
        {
            agent.SetDestination(destination.transform.position);
        }
        else
        {
            agent.SetDestination(start.transform.position);
        }
    }

    void DrawRays()
    {
        Debug.DrawRay(eyeMid.transform.position, eyeMid.transform.forward * viewDist, Color.red, 0.1f);
        Debug.DrawRay(eyeLeft.transform.position, eyeLeft.transform.forward * viewDist, Color.red, 0.1f);
        Debug.DrawRay(eyeRight.transform.position, eyeRight.transform.forward * viewDist, Color.red, 0.1f);
    }

    void CastRays()
    {
        if ((Physics.Raycast(eyeLeft.transform.position, eyeLeft.transform.forward, out hit1, viewDist)) && !found)
        {
            if (hit1.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Gotcha");
                found = true;
            }
        }
        if ((Physics.Raycast(eyeMid.transform.position, eyeMid.transform.forward, out hit2, viewDist)) && !found)
        {
            if (hit2.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Gotcha");
                found = true;
            }
        }

        if ((Physics.Raycast(eyeRight.transform.position, eyeRight.transform.forward, out hit3, viewDist)) && !found)
        {
            if (hit3.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Gotcha");
                found = true;
            }
        }


    }
    public bool GetFound()
    {
        return found;
    }

    public void SetFound(bool value)
    {
        found = value;
    }

    public Vector3 GetInitialPos()
    {
        return initialPos;
    }

    public GameObject GetBoi()
    {
        return boi;
    }

    public Quaternion GetInitialRot()
    {
        return initialRot;
    }

    public void SetGoingTo(bool value)
    {
        goingTo = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.transform.CompareTag("Player"))
        {
            if (player.GetComponent<AiController>().GetFound() != true)
            {
                found = true;
            }
            
            Vector3 direction = player.transform.position - boi.transform.position;
            boi.transform.rotation = Quaternion.Slerp(boi.transform.rotation, Quaternion.LookRotation(direction), 1f);
            //boi.transform.localEulerAngles = direction;
        }

    }
}
