using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dechet : Interactable
{

    public int quantity = 5;
    public bool isTargeted = false;

    private void Awake()
    {
        quantity = Random.Range(4, 10);
    }

    public void EnableToggle()
    {
        isTargeted = true;
        gameObject.RemoveElementTocollect();
    }

    public override void Interact(Liquidateur li)
    {
        li.stock += quantity;

        if (li.stock > li.stockMax)
        {
            int surplus = li.stockMax - li.stockMax;
            quantity = surplus;
        }
        else
        {
            quantity = 0;
        }

    }


    public override void EndInteract()
    {
        isTargeted = false;

        if (quantity <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.AddElementTocollect();
        }
    }





    private void OnEnable()
    {
        gameObject.AddElementTocollect();
    }

    private void OnDestroy()
    {
        gameObject.RemoveElementTocollect();
    }


}
