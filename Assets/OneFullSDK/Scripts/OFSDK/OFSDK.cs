using System;
using UnityEngine;

public class OFSDK 
{
    private static OFSDK _instance;

    AndroidJavaObject _apiMangerObj;

    AndroidJavaObject _utilObj;

    AndroidJavaObject _androidContext;

    AndroidJavaObject _currentActivity;

    OFCanvas _ofCanvas;

    OFSDKManager _manager;

    public static OFSDK GetInstance()
    {
        if (_instance == null)
        { 
            _instance = new OFSDK();
        }
        return _instance;
    }

    public OFSDK()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj = new AndroidJavaObject("com.onefull.unitysdk.ApiManager");
        _utilObj = new AndroidJavaObject("com.onefull.unitysdk.Util");
        _androidContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        _currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }

    public void InitSDK(string appId, string appSecrect)
    {
        GameObject OFSDKManager = GameObject.Instantiate(Resources.Load("OFSDK/OFSDKManager")) as GameObject;
        OFSDKManager.name = "OFSDK";
        GameObject.DontDestroyOnLoad(OFSDKManager);
        _manager = OFSDKManager.GetComponent<OFSDKManager>();
        _ofCanvas = _manager.canvas;

#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("init", appId, appSecrect, 1, _androidContext, _currentActivity);
#elif UNITY_EDITOR

#endif
    }

    public void LoginUI(Action<bool, string> lcb = null)
    {
        _manager.canvas.gameObject.SetActive(true);
        _ofCanvas.StartLogin(lcb);
    }

    public void GetSms(string mobile, OFAndroidCallback callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("getSMS", mobile ,callback);
#elif UNITY_EDITOR

#endif
    }

    public void Login(string mobile, string smsCode, OFAndroidCallback callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("login", mobile, smsCode ,callback);
#elif UNITY_EDITOR

#endif
    }

    public void VerifyRealName(string idNo, string name, OFAndroidCallback callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("verifyRealName", name, idNo ,callback);
#elif UNITY_EDITOR

#endif
    }


    public void Logout(OFAndroidCallback callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("logout" ,callback);
#elif UNITY_EDITOR

#endif
    }

    public void IsRealName(Action<bool> real)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        OFAndroidCallback callback = new OFAndroidCallback();
        callback.SetCallback(
                    (data)=> { 
                        real.Invoke(true);
                    }, 
                    (code , msg)=> { 
                        real.Invoke(false);
                    });
        _apiMangerObj.Call("isRealName" ,callback);
#elif UNITY_EDITOR

#endif
    }

    public void ShowToast(string msg)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
            _currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                Toast.CallStatic<AndroidJavaObject>("makeText", _currentActivity, msg, Toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
            }));
#elif UNITY_EDITOR

#endif
    }

    public void Pay(string pid)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        OFAndroidCallback callback = new OFAndroidCallback();
        callback.SetCallback((data)=> { 

        }, 
        (code , msg)=> { 

        });
        _apiMangerObj.Call("pay", pid ,callback);
#elif UNITY_EDITOR

#endif
    }
}
