using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_range : MonoBehaviour
{
    public Vector3 target;
    public List<GameObject> targets = new List<GameObject>();

    public t_move targetUnit;
    public t_Emove parent;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<t_Emove>();
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        if (targets != null)
        {
            for(int i =0; i< targets.Count; i++)
            {
                target = targets[i].transform.position;
                targetUnit = targets[i].GetComponent<t_move>();
                if(targetUnit.health > 0)
                {
                    parent.Attack(target, targetUnit);
                }
                else
                {
                    targets.Remove(targets[i]);
                }
            }
        }
        else
        {
            Debug.Log("Àû¾øÀ½");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //targets.Add(other.gameObject.transform.position);
            targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.Remove(other.gameObject);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        time += Time.deltaTime;

    //        if (time > 1f)
    //        {
    //            time = 0;
    //            target = other.transform.position;
    //            targetUnit = other.GetComponent<t_move>();
    //        }
    //            parent.Attack(target,targetUnit);

    //    }
    //}
}
