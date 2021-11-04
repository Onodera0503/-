using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] Image;
    public Image UI;
    public GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
    }

    public void Test()
    {
        Debug.Log("ÉeÉXÉg");
    }

    public void UICreate(Image ui)
    {
        // UIÇê∂ê¨Ç∑ÇÈ
        UI = Instantiate(ui, this.transform.localPosition, this.transform.localRotation);
        UI.transform.SetParent(this.canvas.transform, false);
        //UI.Init();

        Debug.Log("UIê∂ê¨");
    }

    public void UIDestroy()
    {
        Destroy(UI.gameObject);

        Debug.Log("UIçÌèú");
    }
}
