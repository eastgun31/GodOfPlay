using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AttackRange : MonoBehaviour
{
    Transform target;
    E_unitMove e_unit;
    public UnitController parent;


    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Debug.Log("鸥百眠利");
            Vector3 dir = target.position;
            parent.Attack(dir, e_unit);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        StartCoroutine(Attack());
    //    }

    //}

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            target = col.gameObject.transform;

            e_unit = col.gameObject.GetComponent<E_unitMove>();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            e_unit = null;
            target = null;
        }
        //p_unit = null;
        Debug.Log("Box Enemy : Target lost");

    }

    IEnumerator Attack()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    StopCoroutine(Attack());
        //}

        if (target != null)
        {
            Debug.Log("鸥百眠利");
            Vector3 dir = target.position;
            parent.Attack(dir, e_unit);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(Attack());
    }

}
