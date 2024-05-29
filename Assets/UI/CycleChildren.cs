using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleChildren : MonoBehaviour
{
    int childIndex;
    int transType;

    public Button next, prev;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            child.GetComponent<Animator>().SetFloat("type", transType);
        }
    }

    public void Next()
    {
        if (isMoving)
        {
            return;
        }
        transType = 0;

        if(childIndex == transform.childCount - 1)
        {
            childIndex = 0;
        }
        else
        {
            childIndex++;
        }

        foreach(Transform child in this.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.GetComponent<Animator>().SetTrigger("Exit");
            }
        }

        transform.GetChild(childIndex).gameObject.SetActive(true);

        DisableButtons();
    }

    public void Previous()
    {
        if (isMoving)
        {
            return;
        }
        transType = 1;

        if (childIndex == 0)
        {
            childIndex = this.transform.childCount - 1;
        }
        else
        {
            childIndex--;
        }

        foreach (Transform child in this.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.GetComponent<Animator>().SetTrigger("Exit");
            }
        }

        transform.GetChild(childIndex).gameObject.SetActive(true);

        DisableButtons();
    }

    public void DisableButtons()
    {
        isMoving = true;
    }

    public void EnableButtons()
    {
        isMoving = false;
    }
}
