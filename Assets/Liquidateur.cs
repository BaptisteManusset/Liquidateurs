using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Liquidateur : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5;

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
    private void Update()
    {
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
                target = GameManager.Stock;
            }
            else
            {
                FindTarget();

            }
        }
    }

    void FindTarget()
    {
        if (GameManager.ElementsToCollect.Count <= 0)
        {
            Debug.LogError("Aucun collectables disponibles");
            return;
        }
        target = Tools.FindNearestObject(GameManager.ElementsToCollect, gameObject)[0].transform;
        Debug.Log(target, target);
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
}
