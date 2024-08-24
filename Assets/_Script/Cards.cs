using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards 
{
    public enum Naipe
    {
        Espada =1,
        Castelo =2,
        Cerveja =3,
        Dinheiro =4,
        Coringa =1
    };
    public int number;
    public Naipe cardNaipe;
    public string cardSpritePath;
    public Cards(int n, Naipe p, string s )
    {
        number = n;
        cardNaipe = p;
        cardSpritePath = s;
    }
}
