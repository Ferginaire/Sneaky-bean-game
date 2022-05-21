using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWall : MonoBehaviour
{
    [SerializeField]
    float upperLim;
    [SerializeField]
    float lowerLim;
    [SerializeField]
    float speed;
    bool bigging;
    // Start is called before the first frame update
    void Start()
    {
        bigging = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x > upperLim)
        {
            bigging = false;
            transform.localScale = new Vector3(upperLim, transform.localScale.y, transform.localScale.z);

        }
        if (transform.localScale.x < lowerLim)
        {
            bigging = true;
            transform.localScale = new Vector3(lowerLim, transform.localScale.y, transform.localScale.z);
        }

        if (bigging)
        {
            transform.localScale = new Vector3(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x - speed * Time.deltaTime, transform.localScale.y, transform.localScale.z);
        }
    }
}
