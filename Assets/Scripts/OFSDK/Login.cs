using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

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

    int curCoolDown = 60;

    private void Awake()
    {
        MobileTxt.contentType = InputField.ContentType.IntegerNumber;
        MobileTxt.characterLimit = 11;
        CodeTxt.contentType = InputField.ContentType.IntegerNumber;
        CodeTxt.characterLimit = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool IsPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^1(3[0-9]|5[0-9]|7[6-8]|8[0-9])[0-9]{8}$");
    }

    void StartSmsCoolDown()
    {
        CancelInvoke("CoolDown");
        InvokeRepeating("CoolDown", 1.0f, 1.0f);
    }

    void CoolDown()
    {
        CodeBtnTxt.text = $"{curCoolDown}s";
        curCoolDown--;
        if (curCoolDown == 0)
        {
            CodeBtnTxt.text = "����";
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
                OFSDK.GetInstance().ShowToast("������֤��ɹ���");
                StartSmsCoolDown();
            },
            (code, msg) => {
                errorText.text = msg;
            });
            OFSDK.GetInstance().GetSms(MobileTxt.text, callback);
        }
        else
        {
            OFSDK.GetInstance().ShowToast("��������ȷ���ֻ����룡");
        }
    }

    public void TryLogin()
    {
        errorText.text = string.Empty;
        if (IsPhoneNumber(MobileTxt.text) && CodeTxt.text.Length > 0)
        {
            OFAndroidCallback callback = new OFAndroidCallback();
            callback.SetCallback((data) => {
                OFSDK.GetInstance().ShowToast("��¼�ɹ���");
            },
            (code, msg) => {
                errorText.text = msg;
            });
            OFSDK.GetInstance().Login(MobileTxt.text, CodeTxt.text, callback);
        }
        else
        {
            OFSDK.GetInstance().ShowToast("��������ȷ���ֻ��������֤�룡");
        }
    }
}
