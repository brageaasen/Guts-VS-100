using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private FoxBehaviour behaviour;

    void Start()
    {
        behaviour = enemy.GetComponent<FoxBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (enemy != null)
            {
                if (behaviour.isSleeping)
                    behaviour.WakeUp();
            }
        }
    }
}
