using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    private float length, startposX, startposY = 0f;
    public GameObject cam;
    public float parralaxEffect;
    public bool continueParralax = true;
    public int distanceWhenCreate = 5;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;   
    }

    void FixedUpdate()
    {
        if (continueParralax)
        {
            float temp = (cam.transform.position.x * (1 - parralaxEffect));
            float distance = (cam.transform.position.x * parralaxEffect);

            transform.position = new Vector3(startposX + distance, startposY, transform.position.z);

            //if (temp + distanceWhenCreate > startpos + length)
            if (temp > startposX + length)
            {
                startposX += length;
            }
            //else if (temp - distanceWhenCreate < startpos - length)
            else if (temp < startposX - length)
            {
                startposX -= length;
            }
        }
    }
}