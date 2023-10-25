using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float gold = 50;
    public int population;
    int random;
    float spawnCool = 0;

    public GameObject[] unitController;

    public GameObject[] e_unit;

    public int upgradeLV = 0;
    string upgradecheck = "UpgradeCheck";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Upgold", 1.0f, 1.0f); //1초 후에 1초마다

        StartCoroutine(upgradecheck);
    }

    // Update is called once per frame
    void Update()
    {
        population = GameManager.instance.e_population;

        if (unitController[0].transform.childCount <= 10)
        {
            RandomSpawn(0);
        }
        //else if (unitController[1].transform.position == gameObject.transform.position && unitController[1].transform.childCount <= 10)
        //{
        //    RandomSpawn(1);
        //}
        //else if (unitController[2].transform.position == gameObject.transform.position && unitController[2].transform.childCount <= 10)
        //{
        //    RandomSpawn(2);
        //}

    }

    private void Upgold()
    {
        gold += 2; //재화 2씩 증가
    }

    void RandomSpawn(int i)
    {
        spawnCool += Time.deltaTime;

        if (spawnCool > 1f)
        {
            if (population <= 30 && upgradeLV == 0)
            {
                random = Random.Range(0, 4);

                if (random == 0 && gold >= 10)
                {
                    gold -= 10;
                    SpawnUnit(random, i);
                }
                if (random == 1 && gold >= 10)
                {
                    gold -= 10;
                    SpawnUnit(random, i);
                }
                if (random == 2 && gold >= 16)
                {
                    gold -= 16;
                    SpawnUnit(random, i);
                }
                if (random == 3 && gold >= 30)
                {
                    gold -= 30;
                    SpawnUnit(random, i);
                }
            }

            if (population <= 30 && upgradeLV == 1)
            {
                random = Random.Range(4, 8);

                if (random == 4 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 5 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 6 && gold >= 8)
                {
                    gold -= 8;
                    SpawnUnit(random, i);
                }
                if (random == 7 && gold >= 15)
                {
                    gold -= 15;
                    SpawnUnit(random, i);
                }
            }

            if (population <= 30 && upgradeLV == 2)
            {
                random = Random.Range(8, 12);

                if (random == 8 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 9 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 10 && gold >= 8)
                {
                    gold -= 8;
                    SpawnUnit(random, i);
                }
                if (random == 11 && gold >= 15)
                {
                    gold -= 15;
                    SpawnUnit(random, i);
                }
            }



            spawnCool = 0;
        }
    }

    void SpawnUnit(int i, int j)
    {
        GameObject enemy = Instantiate(e_unit[i], this.transform.position, Quaternion.identity);
        enemy.transform.parent = unitController[j].transform;
        GameManager.instance.e_population++;
    }

    IEnumerator UpgradeCheck()
    {
        if (upgradeLV == 2)
        {
            StopCoroutine(upgradecheck);
        }

        if (gold >= 35 && upgradeLV == 0)
        {
            gold -= 35;
            E_Upgrade();
        }

        if (gold >= 60 && upgradeLV == 1)
        {
            gold -= 60;
            E_Upgrade();
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(upgradecheck);
    }

    void E_Upgrade()
    {
        upgradeLV++;
    }

    public void Aphrodite_Warrior(Vector3 spawnPoint, Vector3 pointPosition)
    {
        GameObject newObject = Instantiate(e_unit[0], spawnPoint, Quaternion.identity);
        E_unitMove e_newunit = newObject.GetComponent<E_unitMove>();
        e_newunit.lastDesti = pointPosition;
        GameManager.instance.e_population++;
    }
    public void Aphrodite_Shield(Vector3 spawnPoint, Vector3 pointPosition)
    {
        GameObject newObject = Instantiate(e_unit[1], spawnPoint, Quaternion.identity);
        E_unitMove e_newunit = newObject.GetComponent<E_unitMove>();
        e_newunit.lastDesti = pointPosition;
        GameManager.instance.e_population++;
    }
    public void Aphrodite_Archer(Vector3 spawnPoint, Vector3 pointPosition)
    {
        GameObject newObject = Instantiate(e_unit[2], spawnPoint, Quaternion.identity);
        E_unitMove e_newunit = newObject.GetComponent<E_unitMove>();
        e_newunit.lastDesti = pointPosition;
        GameManager.instance.e_population++;
    }
    public void Aphrodite_HorseMan(Vector3 spawnPoint, Vector3 pointPosition)
    {
        GameObject newObject = Instantiate(e_unit[3], spawnPoint, Quaternion.identity);
        E_unitMove e_newunit = newObject.GetComponent<E_unitMove>();
        e_newunit.lastDesti = pointPosition;
        GameManager.instance.e_population++;
    }

}
