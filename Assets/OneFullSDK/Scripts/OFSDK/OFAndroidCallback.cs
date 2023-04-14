using System;
using UnityEngine;

public class OFAndroidCallback : AndroidJavaProxy
{

    Action<string> sCallback;
    Action<int, string> fCallback;

    public OFAndroidCallback() : base("com.onefull.unitysdk.SDKApiCallback") { }

    public void OnSuccess(String dataStr)
    {
        sCallback.Invoke(dataStr);
    }

    public void OnFailed(int code, String msg)
    { 
        fCallback.Invoke(code, msg);
    }

    public void SetCallback(Action<String> successCallback, Action<int,String> failedCallback)
    {
        sCallback = (dataStr) => {
            OFLoom.QueueOnMainThread((param)=> {
                successCallback.Invoke(dataStr);
            });
        };
        fCallback = (code, msg) => {
            OFLoom.QueueOnMainThread((param) =>
            {
                failedCallback.Invoke(code, msg);
            });
        };
    }
}
