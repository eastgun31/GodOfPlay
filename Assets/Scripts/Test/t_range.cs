using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_range : MonoBehaviour
{
    public Vector3 target;
    public t_move targetUnit;
    public List<Vector3> targets = new List<Vector3>();
    public List<GameObject> targetss = new List<GameObject>();
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
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //targets.Add(other.gameObject.transform.position);
            targetss.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            targetss.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (time > 5f)
            {
                time = 0;
            }

            target = other.transform.position;
            targetUnit = other.GetComponent<t_move>();
            parent.Attack(target,targetUnit);
        }
    }
}
