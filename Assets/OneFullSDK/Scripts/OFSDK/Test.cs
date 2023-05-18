using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        OFSDK.GetInstance().InitSDK("", "");
        //调用登录
        OFSDK.GetInstance().LoginUI((success, accessToken) =>
        {
            if (success)
            {
                Debug.Log("登录成功：" + accessToken);
                OFSDK.GetInstance().IsRealName((isR) =>
                {
                    Debug.Log($"实名结果：{isR}");
                });
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AliPay()
    {
        JsonData data = new JsonData();
        data["extra"] = "AliPay";
        //支付
        OFSDK.GetInstance().Pay("1", 1, data.ToJson());
    }

    public void WeChatPay()
    {
        JsonData data = new JsonData();
        data["extra"] = "WeChatPay";
        OFSDK.GetInstance().Pay("1", 2, data.ToJson());
    }
}
