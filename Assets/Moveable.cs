using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    #region State
    public enum State
    {
        revient,
        vachercher,
        malade,
        attent,
        interagit
    }

    public State state = State.attent;
    #endregion

    [SerializeField] protected float iradiation = 0;

    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float maxRadiation = 2;

    


    protected TextMeshPro text;
    protected ParticleSystem particle;



    #region init
    protected virtual void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
    }
    protected virtual void Start()
    {
        FindTarget();
    }
    #endregion


    protected virtual void FindTarget()
    {
        Debug.Log(gameObject.name + ": find Target");
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



    protected void OnDrawGizmos()
    {
        if (target == null) return;

        Gizmos.DrawLine(transform.position, target.position);
    }


    protected void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Radioactif"))
        {
            if (state == State.malade) return;

            iradiation += 0.1f * Time.deltaTime;
        }
    }


    protected virtual IEnumerator Interact()
    {
        yield return null;
    }



    #region radiation
    [ContextMenu("Je suis Ko")]
    protected virtual void KO()
    {
        state = State.malade;
        GetComponent<Renderer>().material.color = Color.green;

    }

    [ContextMenu("Je suis guéri")]
    public virtual void Heal()
    {
        Debug.Log("Heal");
        state = State.attent;
        GetComponent<Renderer>().material.color = Color.white;
        iradiation = 0;
        GameManager.RequireAssistance.Remove(gameObject);
    }

    protected virtual void UpdateRadiation()
    {
        if (iradiation >= maxRadiation)
        {
            KO();
        }
        var main = GetComponent<ParticleSystem>().main;

        main.startSize = iradiation / 2;

        if(text)
            text.text = Math.Round(iradiation, 2, MidpointRounding.AwayFromZero).ToString();

    }

    #endregion
}
