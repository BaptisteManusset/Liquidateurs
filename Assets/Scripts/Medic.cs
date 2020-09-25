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
        if (state == State.malade || state == State.interagit) return;

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
                if (state != State.interagit)
                    StartCoroutine(nameof(Interact));
            }
        }
        UpdateRadiation();
    }


    protected override void FindTarget()
    {
        if (GameManager.RequireAssistance.Count <= 0)
        {
            Debug.LogError("Aucun personnes a soigner");
            state = State.revient;
            animator.SetTrigger("idle");
            return;
        }
        target = Tools.FindNearestObject(GameManager.RequireAssistance, gameObject)?.transform;
        animator.SetTrigger("walk");

    }


    protected override IEnumerator Interact()
    {
        state = State.interagit;
        animator.SetTrigger("soigne");
        yield return new WaitForSeconds(1f);
        Liquidateur obj = target.GetComponent<Liquidateur>();
        if (obj)
        {
            obj.Heal();

            Debug.Log($"{gameObject.name} soigne { obj.name}", gameObject);
            target = GameManager.Hopital;
        }
        state = State.revient;

    }


}
