using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int maxMonster;
    public List<string> sInventory;
    public List<string> sDeck;

    public List<Dice> inventory;
    public List<Dice> deck;

    public bool SetHighScore(int score)
    {
        if (score > maxMonster)
        {
            maxMonster = score;
            return true;
        }

        return false;
    }
}
