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

    [SerializeField]
    private Text leveltex;

    [SerializeField]
    private float levelup;

    public float speed;
    private int level = 1;
    private float timelevelup = 0;



    // Use this for initialization
    void Start () {
		Floor1 = Floorone.GetComponent<Transform>();
        Floor2 = Floortwo.GetComponent<Transform>();
        leveltex.text = "Level: " + level;
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

        //level hochschalten
        timelevelup += Time.deltaTime;
        if (levelup <= timelevelup)
        {
            level += 1;
            leveltex.text = "Level: " + level;
            timelevelup = 0;
            speed += 0.01f; 
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetLevel()
    {
        return level;
    }
}
