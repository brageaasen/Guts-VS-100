using Pathfinding;
using UnityEngine;

public class OcclusionDetectorInsta : MonoBehaviour
{
    public ParticleSystem bushPS;
    private AudioManager audioManager;
    GameObject player;
    PlayerMovement playerMovement;
    GameObject owl;
    AIPath owlAI;
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindWithTag("Player");
        owl = GameObject.Find("Owl");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        if (owl != null)
        {
            owlAI = owl.GetComponent<AIPath>();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            audioManager.Play("Bush");
            playerMovement.behindForeGround = true;
            CreateBushPS();

            if (owlAI != null)
            {
                owlAI.enabled = false;
            }
            owl.GetComponentInChildren<MeleeEnemy>().enabled = false;
            playerMovement.Stealth();
            MakeSpritesTransparent();
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {

            if (owlAI != null)
            {
                owlAI.enabled = false;
            }
            owl.GetComponentInChildren<MeleeEnemy>().enabled = false;
            playerMovement.Stealth();
            MakeSpritesTransparent();
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            //audioManager.Play("Bush");
            playerMovement.behindForeGround = false;
            CreateBushPS();

            if (owlAI != null)
            {
                owlAI.enabled = true;
            }
            owl.GetComponentInChildren<MeleeEnemy>().enabled = true;
            playerMovement.UnStealth();
            MakeSpritesSolid();
        }
    }

    void MakeSpritesTransparent()
    {
        SpriteRenderer spriteRendererObject = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererPlayer = player.GetComponent<SpriteRenderer>();
        if (spriteRendererObject != null)
        {
            spriteRendererObject.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        if (spriteRendererPlayer != null)
        {
            spriteRendererPlayer.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
        }
    }

    void MakeSpritesSolid()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererPlayer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        if (spriteRendererPlayer != null)
        {
            spriteRendererPlayer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    void CreateBushPS()
	{
		bushPS.Play();
	}
}