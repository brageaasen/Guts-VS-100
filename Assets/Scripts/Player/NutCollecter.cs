using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NutCollecter : MonoBehaviour
{
    public float nuts = 0f;
    public TextMeshProUGUI textNuts;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Nut")
        {
            audioManager.Play("CollectNut");
            IncrementNuts();
            Destroy(other.gameObject);
        }
    }

    public void IncrementNuts()
    {
        nuts++;
        textNuts.text = nuts.ToString();
    }
    public void DecrementNuts()
    {
        if (nuts != 0)
        {
            nuts--;
            textNuts.text = nuts.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
