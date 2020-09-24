using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Liquidateur : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5;
    [SerializeField] float iradiation = 0;

    ParticleSystem particle;

    public enum State
    {
        wait = 0,
        ramene,
        vachercher,
        down,
        idle,
        interact
    }

    public State state = State.wait;

    public int stock;



    private void OnEnable()
    {
        GameManager.Liquidateurs.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Liquidateurs.Remove(this);
        GameManager.RequireAssistance.Remove(this);
    }
    private void Start()
    {
        FindTarget();
    }
    private void FixedUpdate()
    {
        if (state == State.down || state == State.interact) return;

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
            if (stock >= 10)
            {
                state = State.ramene;
                target = GameManager.Stock;

            }
            else
            {
                FindTarget();
            }
        }



        if (iradiation >= 2)
        {
            KO();
        }
        ParticleSystem particle = GetComponent<ParticleSystem>();
        var main = particle.main;

        main.startSize = iradiation / 2;
    }

    private IEnumerator Interact()
    {
        state = State.interact;
        Interactable interactable = target.GetComponent<Interactable>();
        interactable.Interact(this);
        target = null;
        yield return new WaitForSeconds(.2f);
        state = State.vachercher;
        interactable.EndInteract();

    }

    [ContextMenu("Je suis Ko")]
    private void KO()
    {
        state = State.down;
        GetComponent<Renderer>().material.color = Color.green;

        GameManager.IneedAMedic(this);
        //GameManager.ResetGame();
    }

    [ContextMenu("Je suis guéri")]
    public void Heal()
    {
        GameManager.IdontNeedAMedic(this);

        state = State.idle;
        GetComponent<Renderer>().material.color = Color.white;
        iradiation = 0;
    }


    void FindTarget()
    {
        if (GameManager.ElementsToCollect.Count <= 0)
        {
            Debug.LogError("Aucun collectables disponibles");
            target = GameManager.Stock;
            state = State.idle;

            //GameManager.ResetGame();
            return;
        }
        target = Tools.FindNearestObject(GameManager.ElementsToCollect, gameObject)[0].transform;
        target.GetComponent<Dechet>().EnableToggle();
        state = State.vachercher;
    }




    private void Move()
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


    private void OnDrawGizmos()
    {
        if (target == null) return;

        Gizmos.DrawLine(transform.position, target.position);
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Radioactif"))
        {
            if (state == State.down) return;

            iradiation += 0.1f * Time.deltaTime;
        }
    }
}
