using LitJson;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RNVer : MonoBehaviour
{
    public InputField IdNoInput;


    public InputField NameInput;

    [SerializeField]
    OFCanvas ofCanvas;

    RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IdNoInput.isFocused)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 200);
        }

        if (NameInput.isFocused)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 250);
        }

        if (!IdNoInput.isFocused && !NameInput.isFocused)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0);
        }
    }

    public Action<bool, string> loginCallback;
    public void Confirm()
    {
        if (TestCardId(IdNoInput.text) && NameInput.text.Length > 0)
        {
            OFAndroidCallback callback = new OFAndroidCallback();
            callback.SetCallback((data) => {
                OFSDK.GetInstance().ShowToast("实名验证成功！");
                if (loginCallback != null)
                {
                    JsonData jd = JsonMapper.ToObject(data);
                    loginCallback(true, jd["access_token"].ToString());
                }
                ofCanvas.login.gameObject.SetActive(false);
                ofCanvas.gameObject.SetActive(false);
            },
            (code, msg) => {
                OFSDK.GetInstance().ShowToast(msg);
                if (loginCallback != null)
                {
                    loginCallback(false, "");
                }
            });
            OFSDK.GetInstance().VerifyRealName(IdNoInput.text, NameInput.text, callback);
        }
        else
        {
            OFSDK.GetInstance().ShowToast("请输入正确的身份证号码和姓名！");
        }
    }

    public bool TestCardId(string cardId)
    {
        string pattern = @"^\d{17}(?:\d|X)$";
        string birth = cardId.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        DateTime time = new DateTime();
        int[] arr_weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        string[] id_last = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            sum += arr_weight[i] * int.Parse(cardId[i].ToString());
        }
        int result = sum % 11;

        if (Regex.IsMatch(cardId, pattern))
        {
            if (DateTime.TryParse(birth, out time))
            {
                if (id_last[result] == cardId[17].ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
