using UnityEngine;

public class VirtualAccelerometerInput : MonoBehaviour
{


    private Vector3 direction = Vector3.zero;
    private Vector3 sum;
    private float deltatime;

    private static float AccelerometerUpdateInterval = 1.0f / 60.0f;
    private static float LowPassKernelWidthInSeconds = 0.5f;
    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable 
    private Vector3 lowPassValue = Vector3.zero;

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void Start()
    {
        lowPassValue = Input.acceleration;
    }

    public Vector3 LowPassFilterAccelerometer(Vector3 a, Vector3 b, float t)
    {
        lowPassValue = Vector3.Lerp(a, b, t);
        return lowPassValue;
    }

    // Update is called once per frame
    void Update()
    {
        sum = new Vector3(0, 0, 0);
        deltatime = 0;
        for(int i = 0; i < Input.accelerationEvents.Length; ++i)
        {
            sum += Input.accelerationEvents[i].acceleration * Input.accelerationEvents[i].deltaTime;
            deltatime += Input.accelerationEvents[i].deltaTime;
        }
        //Average of sum
        sum = sum * (1 / deltatime);
        //normalize under certain conditions
        sum = (sum.magnitude > 1) ? sum.normalized : sum;

        direction = LowPassFilterAccelerometer(lowPassValue, sum, LowPassFilterFactor);
    }

}
