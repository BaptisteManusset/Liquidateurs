using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Liquidateur : Moveable
{




    public int stock;
    private void OnEnable()
    {
        GameManager.Liquidateurs.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Liquidateurs.Remove(this);
        GameManager.RequireAssistance.Remove(gameObject);
    }

    private void FixedUpdate()
    {
        if (state == State.malade || state == State.interagit) return;

        if (target)
        {
            float dist = Vector3.Distance(target.position, transform.position);
            if (dist > 1)
            {
                Move();
            }
            else
            {
                StartCoroutine(nameof(Interact));
            }
        }
        else
        {
            if (stock > 0)
            {
                state = State.revient;
                target = GameManager.Destination;

            }
            else
            {
                FindTarget();
            }
        }

        UpdateRadiation();
    }



    protected override IEnumerator Interact()
    {
        state = State.interagit;
        Interactable interactable = target.GetComponent<Interactable>();
        interactable.Interact(this);

        target = null;
        yield return new WaitForSeconds(.2f);
        state = State.vachercher;
        interactable.EndInteract();
    }




    protected override void FindTarget()
    {
        if (GameManager.ElementsToCollect.Count <= 0)
        {
            //Debug.LogError("Aucun collectables disponibles");
            target = GameManager.Destination;
            state = State.attent;
            return;
        }
        target = Tools.FindNearestObject(GameManager.ElementsToCollect, gameObject).transform;
        target.GetComponent<Dechet>().EnableToggle();
        state = State.vachercher;
    }

    override protected void KO()
    {
        GameManager.IneedAMedic(gameObject);
        base.KO();
    }


    override public void Heal()
    {
        GameManager.IdontNeedAMedic(gameObject);
        base.Heal();
    }
}
