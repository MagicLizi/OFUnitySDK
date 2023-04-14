using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OFSDK.GetInstance().InitSDK("", "");
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

    public void Pay()
    {
        OFSDK.GetInstance().Pay("1");
    }
}
