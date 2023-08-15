using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackUI : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI parryText;

    // Start is called before the first frame update
    void Start()
    {
    //    this.player = PlayerManager.instance.player;
    }

    // Update is called once per frame
    void Update() // Change canAttack/canParry UI to colors showing its availability
    {
        //if (player.GetComponent<CharacterCombat>().canAttack)
            this.attackText.color = new Color(1, 1, 1, 1);
        //else
            this.attackText.color = new Color(1, 0, 0, 1);
        
        //if (player.GetComponent<CharacterCombat>().canParry)
            this.parryText.color = new Color(1, 1, 1, 1);
        //else if (player.GetComponent<Character>().isParrying)
            this.parryText.color = new Color(0, 0, 1, 1);
        //else
            this.parryText.color = new Color(1, 0, 0, 1);
    }
}
