using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AttackRange : MonoBehaviour
{
    public Vector3 target;
    public List<GameObject> targets = new List<GameObject>();

    //Transform target;
    public E_unitMove e_unit;
    public UnitController parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                target = targets[i].transform.position;
                e_unit = targets[i].GetComponent<E_unitMove>();
                if (e_unit.ehealth > 0 && parent.uhealth > 0)
                {
                    parent.Attack(target, e_unit);
                }
                if (e_unit.ehealth <= 0)
                {
                    e_unit = null;
                    targets.Remove(targets[i]);
                }
            }
        }

        //if (target != null)
        //{
        //    Debug.Log("Å¸°ÙÃßÀû");
        //    Vector3 dir = target.position;
        //    parent.Attack(dir, e_unit);
        //}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targets.Add(col.gameObject);
        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("Enemy"))
    //    {
    //        target = col.gameObject.transform;

    //        e_unit = col.gameObject.GetComponent<E_unitMove>();
    //    }
    //}

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targets.Remove(col.gameObject);
            e_unit = null;
        }
    }

}
