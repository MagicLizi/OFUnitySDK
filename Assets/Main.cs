using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public Text t;
    AndroidJavaObject jo;

    // Start is called before the first frame update
    void Start()
    {
        OFSDK.GetInstance().InitSDK("", "");
        OFSDK.GetInstance().LoginUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnClick() 
    {

    }
}
