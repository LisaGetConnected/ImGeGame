using UnityEngine;

// Script that handles the rotation input into the marble game
public class RotatorScript : MonoBehaviour
{

    VirtualAccelerometerInput vai;
    Vector3 direction;
    float xRot, yRot;

    // Use this for initialization
    void Start()
    {
        vai = GetComponent<VirtualAccelerometerInput>();
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        direction = vai.GetDirection();
        xRot = direction.x * 60;
        yRot = (direction.y * 60);

        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(yRot, 0, xRot));

    }
}
