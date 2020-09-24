using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dechet : Interactable
{

    public int quantity = 5;
    public bool isTargeted = false;
    TextMeshPro text;

    private void Awake()
    {
        quantity = Random.Range(4, 10);
        text = GetComponentInChildren<TextMeshPro>();

        text.text = quantity.ToString();

    }

    public void EnableToggle()
    {
        isTargeted = true;
        gameObject.RemoveElementTocollect();
    }

    public override void Interact(Liquidateur li)
    {
        li.stock += quantity;

        text.text = quantity.ToString();

        quantity = 0;
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
