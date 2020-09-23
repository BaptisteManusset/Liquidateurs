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
        idle
    }

    public State state = State.wait;


    public bool downed = false;

    public int stock;



    private void OnEnable()
    {
        GameManager.Liquidateurs.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Liquidateurs.Remove(this);
    }
    private void Start()
    {
        FindTarget();
    }
    private void FixedUpdate()
    {
        if (state == State.down) return;

        if (target)
        {
            float dist = Vector3.Distance(target.position, transform.position);
            if (dist > 1)
            {
                Move();
            }
            else
            {
                Debug.Log("arrivé a destination");
                Interactable interactable = target.GetComponent<Interactable>();
                interactable.Interact(this);
                target = null;
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

    private void KO()
    {
        state = State.down;
        GetComponent<Renderer>().material.color = Color.green;


        GameManager.ResetGame();
    }

    void FindTarget()
    {
        if (GameManager.ElementsToCollect.Count <= 0)
        {
            Debug.LogError("Aucun collectables disponibles");
            target = GameManager.Stock;
            state = State.idle;

            GameManager.ResetGame();
            return;
        }
        target = Tools.FindNearestObject(GameManager.ElementsToCollect, gameObject)[0].transform;
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
