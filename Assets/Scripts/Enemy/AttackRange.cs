using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Vector3 target;
    public List<GameObject> targets = new List<GameObject>();

    //Transform target;
    public UnitController p_unit;
    public E_unitMove parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<E_unitMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                target = targets[i].transform.position;
                p_unit = targets[i].GetComponent<UnitController>();
                if (p_unit.uhealth > 0)
                {
                    parent.Attakc(target, p_unit);
                }
                else if(p_unit.uhealth <= 0)
                {
                    targets.Remove(targets[i]);
                }
            }
        }

        //if (target != null)
        //{
        //    Debug.Log("Ÿ������");
        //    Vector3 dir = target.position;
        //    parent.Attakc(dir, p_unit);
        //}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            targets.Add(col.gameObject);
            //if(parent.targetUnit == null)
            //{
            //    target = null;
            //    p_unit = null;
            //}
        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        target = col.gameObject.transform;
    //        Debug.Log("Box Enemy : Target found");

    //        p_unit = col.gameObject.GetComponent<UnitController>();
    //    }
    //}

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            targets.Remove(col.gameObject);
            //target = null;
            //p_unit = null;
        }
        //Debug.Log("Box Enemy : Target lost");
    }

}
