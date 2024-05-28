using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableChar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOff()
    {
        GetComponentInParent<CycleChildren>().EnableButtons();
        this.gameObject.SetActive(false);
        this.GetComponentInChildren<Image>().color = new Color(this.GetComponentInChildren<Image>().color.r, this.GetComponentInChildren<Image>().color.g, this.GetComponentInChildren<Image>().color.b, 1);
    }

    private void DisableUI()
    {
        this.gameObject.SetActive(false);
    }
}
