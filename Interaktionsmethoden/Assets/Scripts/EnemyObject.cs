using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour {

    Transform enemytransform;

    [SerializeField]
    private float speed;

    // Use this for initialization
    void Start () {
		enemytransform = gameObject.GetComponent<Transform>();
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
	}
}
