using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
using System.Threading;
using Unity.VisualScripting;

public class Login : MonoBehaviour
{
    [SerializeField]
    InputField MobileTxt;

    [SerializeField]
    InputField CodeTxt;

    [SerializeField]
    Toggle Tog;

    [SerializeField]
    Alert alert;

    [SerializeField]
    Text errorText;

    [SerializeField]
    Text CodeBtnTxt;

    [SerializeField]
    OFCanvas ofCanvas;

    int curCoolDown = 60;

    RectTransform rt;

    private void Awake()
    {
        MobileTxt.contentType = InputField.ContentType.IntegerNumber;
        MobileTxt.characterLimit = 11;
        CodeTxt.contentType = InputField.ContentType.IntegerNumber;
        CodeTxt.characterLimit = 4;
        rt = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileTxt.isFocused)
        {
           rt.anchoredPosition = new Vector2 (rt.anchoredPosition.x, 200);
        }

        if (CodeTxt.isFocused)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 250);
        }

        if (!MobileTxt.isFocused && !CodeTxt.isFocused)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0);
        }
    }

    public static bool IsPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^1(3[0-9]|5[0-9]|7[6-8]|8[0-9])[0-9]{8}$");
    }

    void StartSmsCoolDown()
    {
        CancelInvoke("CoolDown");
        InvokeRepeating("CoolDown", 0, 1.0f);
    }

    void CoolDown()
    {
        CodeBtnTxt.text = $"{curCoolDown}s";
        curCoolDown--;
        if (curCoolDown == 0)
        {
            CodeBtnTxt.text = "登录";
            CancelInvoke("CoolDown");
        }
    }

    public void GetSmsCode()
    {
        errorText.text = string.Empty;
        if (IsPhoneNumber(MobileTxt.text))
        {
            OFAndroidCallback callback = new OFAndroidCallback();
            callback.SetCallback((data) => {
                OFSDK.GetInstance().ShowToast("获取验证码成功！");
                StartSmsCoolDown();
            },
            (code, msg) => {
                errorText.text = msg;
            });
            OFSDK.GetInstance().GetSms(MobileTxt.text, callback);
        }
        else
        {
            OFSDK.GetInstance().ShowToast("请输入正确的手机号码！");
        }
    }

    public Action<bool, string> loginCallback;

    public void TryLogin()
    {
        errorText.text = string.Empty;
        if (Tog.isOn)
        {
            if (IsPhoneNumber(MobileTxt.text) && CodeTxt.text.Length > 0)
            {
                OFAndroidCallback callback = new OFAndroidCallback();
                callback.SetCallback((data) =>
                {
                    OFSDK.GetInstance().ShowToast("登录成功！");
                    CancelInvoke("CoolDown");
                    if (loginCallback != null)
                    {
                        JsonData jd = JsonMapper.ToObject(data);
                        loginCallback(true, jd["access_token"].ToString());
                    }
                    ofCanvas.gameObject.SetActive(false);
                },
                (code, msg) =>
                {
                    errorText.text = msg;
                    if (code == 1004)
                    {
                        ofCanvas.login.gameObject.SetActive(false);
                        ofCanvas.rnver.loginCallback = loginCallback;
                        ofCanvas.rnver.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (loginCallback != null)
                        {
                            loginCallback(false, "");
                        }
                    }
                });
                OFSDK.GetInstance().Login(MobileTxt.text, CodeTxt.text, callback);
            }
            else
            {
                OFSDK.GetInstance().ShowToast("请输入正确的手机号码和验证码！");
            }
        }
        else
        {
            OFSDK.GetInstance().ShowToast("请勾选条款！");
        }
        
    }
}
