using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float _strength = 16, _delay = 0.15f;
    public UnityEvent OnBegin, OnDone;


    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * _strength, ForceMode2D.Impulse);

        Vector2 up = new(0, 1.5f);
        rb.AddForce(up * _strength, ForceMode2D.Impulse);

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(_delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}