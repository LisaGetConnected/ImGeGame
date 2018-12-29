using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Transform playertransform;
    Vector3 size;

    VirtualAccelerometerInput vai;
    Vector3 direction;
    float micVol;
    bool jumpstat;
    float jumpspeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        vai = GetComponent<VirtualAccelerometerInput>();
        playertransform = gameObject.GetComponent<Transform>();
        micVol = MicrophoneInput.MicLoudness;
        size = gameObject.GetComponent<Collider>().bounds.size;
        jumpstat = false;
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        MoveSide();

        //Springt er schon dann soll er erst wieder springen können wenn er auf dem boden ist. Springen durch ins Microfon schreien.
        micVol = MicrophoneInput.MicLoudness;
        if (micVol > 0 && jumpstat != true)
        {
            jumpstat = true;
            StartCoroutine("Jump");
        }

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

    IEnumerator Jump()
    {
        //Spieler hüpft dann wartet er oben und dann fliegt er wieder runter.
        while (playertransform.position.y <= 4)
        {
            playertransform.position += new Vector3(0, jumpspeed, 0);
        }
        yield return new WaitForSeconds(1);
        while (playertransform.position.y >= size.y)
        {
            playertransform.position += new Vector3(0, jumpspeed, 0);
        }
        jumpstat = false;
        yield return null;
    }
    
}
