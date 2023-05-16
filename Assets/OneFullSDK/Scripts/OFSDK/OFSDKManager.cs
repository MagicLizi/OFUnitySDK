using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OFSDKManager : MonoBehaviour
{
    public OFCanvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckCanPlay", 0, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        CancelInvoke("CheckCanPlay");
    }

    void CheckCanPlay()
    {
        OFAndroidCallback callback = new OFAndroidCallback();
        callback.SetCallback((data) => {
            
        },
        (code, msg) => {
            if (code == 1013)
            {
                OFLoom.QueueOnMainThread((p) =>
                {
                    canvas.ShowNotice("未成年游戏时间限制", () =>
                    {
                        Application.Quit();
                    });
                });
            }
        });
        OFSDK.GetInstance().ChackCanPlay(callback);
    }
}
