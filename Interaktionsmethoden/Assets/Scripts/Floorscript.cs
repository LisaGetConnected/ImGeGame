using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floorscript : MonoBehaviour
{
    public GameObject Floorone;
    public GameObject Floortwo;
    private Transform Floor1;
    private Transform Floor2;
    public float speed;

    // Use this for initialization
    void Start () {
		Floor1 = Floorone.GetComponent<Transform>();
        Floor2 = Floortwo.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //der Boden wird verschoben damit ein Gefühl von vorwerts kommen entsteht.
        Floor1.position += new Vector3(0,0, -speed);
        Floor2.position += new Vector3(0,0, -speed);

        // Wenn der Boden zu weit hinten verschoben ist dann wird er wieder nach vorne gesetzt. Um ein endloses gefühl zu erzeugen.
        if (Floor1.position.z < -30)
        {
            Floor1.position += new Vector3(0, 0, 80);
        }
        if (Floor2.position.z < -30)
        {
            Floor2.position += new Vector3(0, 0, 80);
        }
    }
}
