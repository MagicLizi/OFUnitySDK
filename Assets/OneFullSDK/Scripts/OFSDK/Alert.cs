using System;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    [SerializeField]
    Text content;


    Action callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMsg(string msg, Action cb)
    {
        gameObject.SetActive(true);
        content.text = msg;
        callback = cb;
    }

    public void Confirm()
    {
        if (callback != null)
        { 
            callback.Invoke();
            callback = null;
        }
        gameObject.SetActive(false);
    }
}
