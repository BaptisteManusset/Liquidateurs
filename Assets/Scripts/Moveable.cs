using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//classe de base qui controle gere le deplacement et la logique commune aux liquidateurs et aux medecins
public class Moveable : MonoBehaviour
{
    //state machine
    #region State
    public enum State
    {
        revient,
        vachercher,
        malade,
        attent,
        interagit
    }
    [Header("Etat")]
    public State state = State.attent;
    #endregion


    [Header("Radiation")]
    [SerializeField] protected float iradiation = 0;
    [SerializeField] protected float maxRadiation = 2;

    [Header("Movement")]
    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 5;




    protected TextMeshPro text;
    protected ParticleSystem particle;
    protected Animator animator;



    #region init
    protected virtual void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
        animator = GetComponentInChildren<Animator>();
    }
    protected virtual void Start()
    {
        FindTarget();
    }
    #endregion


    protected virtual void FindTarget()
    {
        animator.SetBool("idle", false);
    }


    protected virtual void Move()
    {
        if (target == null)
        {
            Debug.LogError("Missing Target");
            return;
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.LookAt(target.position);
    }


    //affiche des informations de debug
    protected void OnDrawGizmos()
    {
        if (target == null) return;

        Gizmos.DrawLine(transform.position, target.position);
    }


    protected void TakeRadiation()
    {
        List<GameObject> list = Tools.FindNearestsObjects(GameManager.ElementsToCollect, gameObject);

        foreach (GameObject item in list)
        {
            if (item.CompareTag("Radioactif"))
            {
                float dist = Vector3.Distance(item.transform.position, transform.position);
                if (dist <= 5)
                    iradiation += 0.1f * dist * Time.deltaTime * GameManager.instance.radiationMultiplier;
                else break;
            }
        }


    }

    //protected void OnTriggerStay(Collider other)
    //{

    //    if (other.gameObject.CompareTag("Radioactif"))
    //    {
    //        if (state == State.malade) return;
    //        iradiation += 0.3f * Time.deltaTime;
    //    }
    //}


    protected virtual IEnumerator Interact()
    {
        yield return null;
    }



    #region radiation
    [ContextMenu("Je suis Ko")]
    protected virtual void KO()
    {
        state = State.malade;
        animator.SetTrigger("dead");

    }

    //le personnage est guérie
    [ContextMenu("Je suis guéri")]
    public virtual void Heal()
    {
        state = State.attent;
        GetComponent<Renderer>().material.color = Color.white;
        iradiation = 0;


        animator.SetTrigger("walk");
    }

    //met a jours les informations concernant les radiations et change l'état du personnag si besoin
    protected virtual void UpdateRadiation()
    {
        TakeRadiation();


        if (iradiation >= maxRadiation)
        {
            KO();
        }
        var main = GetComponent<ParticleSystem>().main;

        main.startSize = iradiation / 2;

        if (text)
            text.text = Math.Round(iradiation, 2, MidpointRounding.AwayFromZero).ToString();

    }

    #endregion
}
