using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private GameObject Enemy1;
    [SerializeField]
    private GameObject Enemy2;
    [SerializeField]
    private float spawnTime;

    private float spawnTimeFromNow;

    GameObject[] Enemys = new GameObject[3];

    // Use this for initialization
    void Start () {
        Enemys[1] = Enemy1;
        Enemys[2] = Enemy2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimeFromNow <= 0)
        {
            CreateObject();
            spawnTimeFromNow = spawnTime;
        }
        spawnTimeFromNow -= Time.deltaTime;

    }

    private void CreateObject()
    {
        //Es wird ein Random Object als Hindernis ausgewählt und dieses wird dann hinten im Spiel erzeugt.
        int digit = Random.Range(1, 3);
        GameObject Enemy = Enemys[digit];
        float size = Enemy.GetComponent<Renderer>().bounds.size.y;
        Vector3 position = new Vector3(Random.Range(-5.0f, 5.0f), size / 2, 25);
        Instantiate(Enemy, position, Quaternion.identity);
    }
}
