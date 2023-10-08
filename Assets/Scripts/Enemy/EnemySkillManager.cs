using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillManager : MonoBehaviour
{
    public static EnemySkillManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public GameObject enemySkills;

    public bool e_Zeus_S ;
    public bool e_Poseidon_S;
    public bool e_Hades_S;

    public bool e_Hephaestus_S;
    public bool e_Artemis_S;
    public bool e_Ares_S;

    public bool e_Hera_S;
    public bool e_Apollo_S;
    public bool e_Athena_S;
    public bool e_Aphrodite_S;

    public bool e_Hermes_S;
    public bool e_Hestia_S;
    public bool e_Dionysus_S;
    public bool e_Demeter_S;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
