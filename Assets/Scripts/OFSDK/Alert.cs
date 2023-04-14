using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    [SerializeField]
    Text content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMsg(string msg)
    {
        gameObject.SetActive(true);
        content.text = msg;
    }

    public void Confirm()
    {
        gameObject.SetActive(false);
    }
}
