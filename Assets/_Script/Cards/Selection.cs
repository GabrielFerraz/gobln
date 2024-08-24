using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;
    

    [SerializeField]
    public int pannelNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (PassingCard.turnIndex == 0)
            PassingCard.PassCard(playerScript.cardsHand[pannelNumber], pannelNumber);
    }
}
