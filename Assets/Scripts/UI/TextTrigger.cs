using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] GameObject textTutorial;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (textTutorial != null)
            {
                textTutorial.GetComponent<TextTimer>().EnableText();
            }
        }
    }
}
