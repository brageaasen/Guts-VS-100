using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerCombat.LastAttackDirection _lastAttackDirection;

    [SerializeField] private float _strength = 16, _delay = 0.15f;
    public float maxForceMagnitude = 1f;
    public float forceDuration = 0.5f; // Duration over which force is increased
    
    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        _lastAttackDirection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().lastAttackDirection;


        // Check if sender is not the player
        if (!sender.CompareTag("Player"))
        {
            rb.AddForce(direction * _strength, ForceMode2D.Impulse);
        }
        // Check which direction to give knockback to non player object
        if (_lastAttackDirection == PlayerCombat.LastAttackDirection.FORWARD)
        {
            rb.AddForce(direction * _strength, ForceMode2D.Impulse);
        } else if (_lastAttackDirection == PlayerCombat.LastAttackDirection.UP)
        {
            direction.y = 1.2f;
            rb.AddForce(direction * _strength, ForceMode2D.Impulse);
        }
        else
        {
            direction.y = -1.2f;
            rb.AddForce(direction * _strength, ForceMode2D.Impulse);
        }

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(_delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

}