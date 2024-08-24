using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingCard : MonoBehaviour
{
    private static GameObject[] goblinList;
    public GameObject[] anchorList;
    public GameObject goblinExamples;
    //private GameObject passingCard;
    public static int turnIndex;
    private float timer;
    private static bool passAnim;
    
    void Start()
    {
       //passingCard = transform.GetChild(0).gameObject;
        passAnim = false;
        turnIndex = 0;
        timer = 0;

        goblinList = new GameObject[4];
        goblinList[0] = goblinExamples.transform.GetChild(0).gameObject;
        goblinList[1] = goblinExamples.transform.GetChild(1).gameObject;
        goblinList[2] = goblinExamples.transform.GetChild(2).gameObject;
        goblinList[3] = goblinExamples.transform.GetChild(3).gameObject;

        //anchorList[]
        //0 = right anchor
        //1 = right goblin
        //2 = middle goblin
        //3 = left goblin
        //4 = left anchor
    }

    // Update is called once per frame
    void Update()
    {
        if (passAnim)
        {
            timer += Time.deltaTime*1.5f;
            gameObject.transform.position = Vector2.Lerp(anchorList[turnIndex].transform.position, anchorList[turnIndex + 1].transform.position, timer);
            
            if (gameObject.transform.position.x == anchorList[turnIndex + 1].transform.position.x)
            {
                timer = 0;
                turnIndex++;
                passAnim = false;

                if (turnIndex >= 4)
                    turnIndex = 0;
            }
        }
    }

    public static void PassCard(Cards c, int i) //c para carta que será passada adiante; i para posição dentro da cardsHand na qual a carta passada estava, para ser substituida
    {
        passAnim = true;

        Character nextGoblin = goblinList[turnIndex + 1].GetComponent<Character>();
        Character actualGoblin = goblinList[turnIndex].GetComponent<Character>();
        
        for(int j = 0; j <4; j++)
            Debug.Log(actualGoblin.cardsHand[j].cardNaipe + " de "+ actualGoblin.cardsHand[j].cardNaipe + ", ");

        nextGoblin.cardsHand[4] = c;

        actualGoblin.cardsHand[i] = actualGoblin.cardsHand[4];
        actualGoblin.cardsHand[4] = null;
        actualGoblin.cardsHeld--;
        nextGoblin.cardsHeld++;

        Debug.Log("Nova mão:");
        for (int j = 0; j < 4; j++)
            Debug.Log(actualGoblin.cardsHand[j].number + " de " + actualGoblin.cardsHand[j].cardNaipe + ", ");

    }

    //jogador termina o turno, Pass card
    //animação da carta passada ativa
    //quando terminar
}
