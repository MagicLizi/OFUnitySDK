using UnityEngine;
using UnityEngine.Networking.Types;

public class OFSDK 
{
    private static OFSDK _instance;

    AndroidJavaObject _apiMangerObj;

    AndroidJavaObject _utilObj;

    AndroidJavaObject _androidContext;

    OFCanvas _ofCanvas;

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
        _apiMangerObj = new AndroidJavaObject("com.onefull.unitysdk.ApiManager");
        _utilObj = new AndroidJavaObject("com.onefull.unitysdk.Util");
        _androidContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void InitSDK(string appId, string appSecrect)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _apiMangerObj.Call("init", appId, appSecrect, 1, _androidContext);
#elif UNITY_EDITOR

#endif
    }

    public void LoginUI()
    {
        GameObject canvasObj = GameObject.Instantiate(Resources.Load("OFSDK/OFCanvas")) as GameObject;
        _ofCanvas = canvasObj.GetComponent<OFCanvas>();
        _ofCanvas.name = "OFSDK";
        _ofCanvas.StartLogin();
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

    public void ShowToast(string msg)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _utilObj.Call("ShowToast", _androidContext, msg);
#elif UNITY_EDITOR

#endif
    }
}
