using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{

    [SerializeField]
    Text displayText;
    NavMeshAgent agent;
    Vector2 mousePosition;
    Vector3 initialpos;
    RaycastHit hit;
    bool found;
    bool resetting;
    bool won;



    GameObject[] copList;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        copList = GameObject.FindGameObjectsWithTag("Coppa");
        initialpos = agent.transform.position;
        displayText.gameObject.SetActive(false);
        won = false;
        
        
    }

    private void Update()
    {
        foreach (GameObject cop in copList)
        {

            if (cop.GetComponent<GuardAi>().GetFound() == true && !resetting)
            {
                found = true;
                agent.SetDestination(transform.position);
                resetting = true;
                Reset();
                Debug.Log("Looped");
                break;

            }
            
        }

        
    }

    void OnLeftClick(InputValue value)
    {
        if (value.isPressed)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hit, 100f) && !found)
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void OnMousePos(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }

    private void Reset()
    {
        
        StartCoroutine("WaitForResetMoid"); 
        
    }

    IEnumerator WaitForResetMoid()
    {
        displayText.gameObject.SetActive(true);
        if (won) { displayText.text = "You win!"; }
        if (found) { displayText.text = "You lose"; }
        yield return new WaitForSeconds(2);
        agent.transform.position = initialpos;
        agent.SetDestination(initialpos);
        foreach (GameObject cop in copList)
        {
            cop.GetComponent<GuardAi>().SetFound(false);
            cop.GetComponent<GuardAi>().GetBoi().transform.position = cop.GetComponent<GuardAi>().GetInitialPos();
            //cop.GetComponent<GuardAi>().GetBoi().transform.localEulerAngles = new Vector3(0, 1, 0);
            cop.transform.rotation = cop.GetComponent<GuardAi>().GetInitialRot();
            cop.GetComponent<GuardAi>().SetGoingTo(true);
           
        }
        found = false;
        resetting = false;
        won = false;
        displayText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            won = true;
            StartCoroutine("WaitForResetMoid");
        }
    }

    public bool GetFound()
    {
        return found;
    }
}
