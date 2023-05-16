using System;
using UnityEngine;
using UnityEngine.UI;

public class OFCanvas : MonoBehaviour
{
    [SerializeField]
    public Login login;

    [SerializeField]
    public RNVer rnver;

    [SerializeField]
    Alert alert;

    ScreenOrientation curOrientation;

    Canvas canvas;

    CanvasScaler CanvasScaler;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        CanvasScaler = GetComponent<CanvasScaler>();
        RefreshOrientation();
    }

    void RefreshOrientation()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (curOrientation != Screen.orientation)
        {
            curOrientation = Screen.orientation;
            if (curOrientation == ScreenOrientation.LandscapeLeft || curOrientation == ScreenOrientation.LandscapeRight)
            {
                CanvasScaler.referenceResolution = new Vector2 (1920, 1080);
            }
            else if (curOrientation == ScreenOrientation.Portrait || curOrientation == ScreenOrientation.PortraitUpsideDown)
            {
                CanvasScaler.referenceResolution = new Vector2(1080, 1920);
            }
        }
#endif
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RefreshOrientation();
    }

    public void StartLogin(Action<bool, string> loginCallback = null)
    {
        login.loginCallback = loginCallback;
        login.gameObject.SetActive(true);
    }

    public void ShowNotice(string notice, Action callback = null)
    {
        alert.ShowMsg(notice, callback);
    }
}
