using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dechet : Interactable
{

    public int Health = 5;

    private void OnEnable()
    {
        gameObject.AddElementTocollect();
    }

    private void OnDestroy()
    {
        gameObject.RemoveElementTocollect();
    }

    public override void Interact(Liquidateur li)
    {
        li.stock += 5;

        Destroy(gameObject);
    }
}
