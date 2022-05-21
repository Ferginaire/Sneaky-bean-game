using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    [SerializeField]
    float upperLim;
    [SerializeField]
    float lowerLim;
    [SerializeField]
    float speed;
    bool goingTo;
    // Start is called before the first frame update
    void Start()
    {
        goingTo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > upperLim)
        {
            goingTo = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, upperLim);

        }
        if (transform.position.z < lowerLim)
        {
            goingTo = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerLim);
        }
        
        if (goingTo)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        }
    }
}
