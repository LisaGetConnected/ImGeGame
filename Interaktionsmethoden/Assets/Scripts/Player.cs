using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private Text lifetex;


    [SerializeField]
    private Light licht;

    private int life = 3;

    Transform playertransform;
    Vector3 size;

    VirtualAccelerometerInput vai;
    Vector3 direction;
    private float micVol;
    private bool blinkstat = false;
    private bool lichtstat = false;
    private int lichtprog = 0;


    // Use this for initialization
    void Start()
    {
        //Einrichtung der instrumente
        vai = GetComponent<VirtualAccelerometerInput>();
        playertransform = gameObject.GetComponent<Transform>();
        micVol = MicrophoneInput.MicLoudness;

        //Spieler auf den boden setzen
        size = gameObject.GetComponent<Collider>().bounds.size;
        playertransform.position = new Vector3(0, size.y/2, -4);

        //Leben setzen in der Anzeige
        lifetex.text = "Leben: " + life;
        //leveltex.text = "Level: " + level;
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        MoveSide();

        //licht sanft an und aus 
        //Updaten der lautstärke
        micVol = MicrophoneInput.MicLoudness;

        if (micVol > 0.2f && lichtstat == false)
        {
            lichtstat = true;
            lichtprog = 1;
        }
        if (lichtstat == true && lichtprog == 1)
        {
            licht.range +=  2 * Time.deltaTime;
            if(licht.range >= 20)
            {
                lichtprog = 2;
            }
        }
        else if(lichtstat == true && lichtprog == -1)
        {
            licht.range -= 2 * Time.deltaTime;
            if(licht.range <= 13)
            {
                lichtstat = false;
                lichtprog = 0;
            }
        } else if( lichtprog == 2 && lichtstat == true)
        {
            StartCoroutine(LichtAn());
        }
        
        

    }

    IEnumerator LichtAn()
    {
        yield return new WaitForSeconds(5);
        lichtprog = -1;

    }

    void MoveSide()
    {
        direction = vai.GetDirection();
        //Der Spieler soll sich nicht weiter als zwischen -4 und 4 bewegen weil er sonst aus dem bildschirm fliegt.
        //Debug.Log(direction.x + " Wendewert");
        float playerpos = playertransform.position.x;
        if (playerpos < 4 && playerpos > -4)
        {
            if (!float.IsNaN(direction.x))
            {
                playertransform.position += new Vector3(direction.x, 0, 0);
            }
        }
        if (playerpos >= 4 && direction.x < 0)
        {
            playertransform.position += new Vector3(direction.x, 0, 0);
        }
        else if (playerpos <= -4 && direction.x >= 0)
        {
            playertransform.position += new Vector3(direction.x, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            life -= 1;
            lifetex.text = "Leben: " + life;
            if(life <= 0)
            {
                SceneManager.LoadScene("Main");
            }
            Vibration.Vibrate(20);
            Destroy(collision.gameObject);
            if(blinkstat == false)
            {
                blinkstat = true;
                StartCoroutine(Blink());
            }
        }
    }

    IEnumerator Blink()
    {
        for(int i = 0; i < 3; ++i)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        blinkstat = false;
    }
}
