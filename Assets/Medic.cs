using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : Moveable
{
    private void OnEnable()
    {
        GameManager.Medics.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Medics.Remove(this);
        GameManager.RequireAssistance.Remove(gameObject);
    }

    private void FixedUpdate()
    {

        if (GameManager.RequireAssistance.Count > 0)
        {
            if (target == null || target.CompareTag("Hopital"))
            {
                FindTarget();
            }
        }

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

        //else
        //{
        //    if (target.CompareTag("Hopital"))
        //    {

        //        if (Vector3.Distance(transform.position, GameManager.Destination.position) <= 1)
        //        {
        //            target = null;
        //        }
        //        else
        //        {
        //            Move();
        //        }

        //    }
        //    else
        //    {
        //        target = GameManager.Hopital;
        //    }
        //}
        UpdateRadiation();
    }


    protected override void FindTarget()
    {
        if (GameManager.RequireAssistance.Count <= 0)
        {
            Debug.LogError("Aucun personnes a soigner");
            state = State.revient;
            return;
        }
        target = Tools.FindNearestObject(GameManager.RequireAssistance, gameObject)?.transform;
    }


    protected override IEnumerator Interact()
    {
        Debug.Log(target, target.gameObject);
        //yield return new WaitForSeconds(.2f);
        yield return null;
        Liquidateur obj = target.GetComponent<Liquidateur>();
        if (obj)
            obj.Heal();
        target = GameManager.Hopital;
    }


}
