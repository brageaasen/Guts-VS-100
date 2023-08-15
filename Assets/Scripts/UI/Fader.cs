using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fader : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private string fadeText;

    private float fadeTime;
    private bool fadedIn;
    public bool canFade = true;

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.text.CrossFadeAlpha(0, 0.0f, false);
        this.fadeTime = 0f;
        this.fadedIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (ray.LookingAt() == this.tag && !fadedIn && canFade)
        {
            // Text to be displayed
            //if (ray.LookingAtGameObject().GetComponent<Fader>() != null)
            //    ray.LookingAtGameObject().GetComponent<Fader>().text.text = this.fadeText;

            FadeIn();
        }
        //else if (!(ray.LookingAt() == this.tag) && fadedIn)
            FadeOut();
    }

    public void FadeIn()
    {
        this.fadedIn = true;
        this.text.CrossFadeAlpha(1, 0.5f, false);
        this.fadeTime += Time.deltaTime;

        if (this.text.color.a == 1 && this.fadeTime > 1.5f)
        {
            this.fadeTime = 0f;
        }
    }

    public void FadeOut()
    {
        this.fadedIn = false;
        this.text.CrossFadeAlpha(0, 0.5f, false);
        this.fadeTime += Time.deltaTime;

        if (this.text.color.a == 0 && this.fadeTime > 1.5f)
        {
            this.fadeTime = 0f;
        }
    }
}
