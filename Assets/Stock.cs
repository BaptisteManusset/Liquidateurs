using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : Interactable
{

    public int stock = 0;
    public override void Interact(Liquidateur li)
    {


        stock += li.stock;
        li.stock = 0;
    }
}
