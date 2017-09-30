using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chessman : MonoBehaviour                  //abstract class for chess pieces 
{
    public int CurrentX                                         //current x for the chessman in the 2d array
    {
        get;set;
    }
    public int CurrentY                                         //current y for the chessman in the 2d array
    {
        get;set;
    }
   

    public bool IsWhite;                                       //determines team of chessman
    
    public void SetPosition(int x, int y)                      //sets the current position on the board for the chessman
    {
        CurrentX = x;
        CurrentY = y;
    }
  

    public virtual bool[,] PossibleMove()                      //virtual possiblemove for the chessman
    {
        return new bool[8, 8];
    }
    
}
