using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amount : MonoBehaviour
{
    public int amount; // âºÉfÅ[É^
    public bool Choice;

    // Start is called before the first frame update
    void Start()
    {
        Choice = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickAmount()
    {
        Choice = true;
    }
}
