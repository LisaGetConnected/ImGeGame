using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{

    public static float MicLoudness;

    bool _isInitialized;
    private AudioSource _AudioSource;




    //mic initialization
    void InitMic()
    {

        // Your code
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.clip = Microphone.Start(null, true, 10, 44100);
        _AudioSource.Play();

    }



    float RMS()
    {
        float result = 0; 
        int currentMicPos;
        float[] waveData = new float[128];

        if (_isInitialized)
        {
            currentMicPos = Microphone.GetPosition(null)  - 128;
            if (currentMicPos >= 0)
            {
                _AudioSource.clip.GetData(waveData, currentMicPos);
            }
        }
        for(int i = 0; i < 128; ++i)
        {
            result = result + (waveData[i] * waveData[i]);
        }
        result = Mathf.Sqrt(result * 1 / 128);
        return result;
    }

    void Update()
    {
        MicLoudness = RMS();
    }

    void StopMicrophone()
    {
        Microphone.End(null);
    }

    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }
    
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }
    
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}