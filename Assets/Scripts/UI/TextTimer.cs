using UnityEngine;
using UnityEngine.UI;

public class TextTimer : MonoBehaviour
{
    public Text text;  //Add reference to UI Text here via the inspector
    public float timeToAppear = 4f;
    private float timeWhenDisappear;
    
    //Call to enable the text, which also sets the timer
    public void EnableText()
    {
        text.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }
    
    //We check every frame if the timer has expired and the text should disappear
    void Update()
    {
        if (text.enabled && (Time.time >= timeWhenDisappear))
        {
            text.enabled = false;
            Destroy(gameObject);
        }
    }
}
