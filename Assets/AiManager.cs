using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AiManager : MonoBehaviour
{

    GameObject[] agents; 
    NavMeshAgent agent;
    Vector2 mousePosition;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("ai");
    }

    void OnLeftClick(InputValue value)
    {
        if (value.isPressed)
        {

            foreach(GameObject a in agents)
            {
                agent = a.GetComponent<NavMeshAgent>();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hit, 100f))
                {
                    agent.SetDestination(hit.point);
                } 
            }
            
        }
    }

    void OnMousePos(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }
}
