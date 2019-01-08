using UnityEngine;
using System.Collections;

public static class Vibration
{
    #if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    #else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long milliseconds)
    {
        if (IsAndroid())
        {
            vibrator.Call("vibrate", milliseconds);
        } else
        {
            Handheld.Vibrate();
        }
        
    }
    public static void Vibrate(long[] pattern, int repeat)
    {
        if (IsAndroid())
        {
            vibrator.Call("vibrate", pattern, repeat);
        } else
        {
            Handheld.Vibrate();
        }
    }
    public static void Cancel()
    {
        if (IsAndroid())
        {
            vibrator.Call("cancel");
        } else
        {
            Handheld.Vibrate();
        }
    }
    private static bool IsAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return true;
        } else
        {
            return false;
        }
    }
}