using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Sprite[] cardSprites;
    public GameObject[] cardPannels;
    private Player player;
    private Cards[] playersHand;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
        playersHand = player.cardsHand;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(true)//colocar alguma condição aqui depois
            SpriteYourCards();
    }

    private void SpriteYourCards()
    {
        if (PassingCard.turnIndex == 0)
        {  
            for (int i = 0; i < player.cardsHeld; i++)
            {
                int naipe = (int)playersHand[i].cardNaipe;
                int numero = playersHand[i].number;
                cardPannels[i].GetComponent<SpriteRenderer>().sprite = cardSprites[(naipe - 1) * 4 + numero];
            }
        }
        
    }

    private void OnMouseDown()
    {
        
    }
}
