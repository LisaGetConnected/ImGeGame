using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyObject : MonoBehaviour {

    Transform enemytransform;

    [SerializeField]
    private float speed;

    private GameObject obj;

    // Use this for initialization
    void Start () {
		enemytransform = gameObject.GetComponent<Transform>();

        obj = GameObject.Find("Floor1");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(-22 >= enemytransform.position.z)
        {
            Destroy(gameObject);
        } else
        {
            enemytransform.position += new Vector3(0, 0, -speed);
        }

        Floorscript scr = (Floorscript) obj.GetComponent(typeof(Floorscript));
        speed = scr.GetSpeed();
    }
}
