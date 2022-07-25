using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUIManager : MonoBehaviour
{
    [SerializeField]private GameObject[] uiArray;

    [SerializeField]private GameObject startingUI;
    private Stack<GameObject> _uiStack;


    private void Awake()
    {
        for(int i = 0; i < uiArray.Length; i++)
        {
            GameObject currentUI = uiArray[i];
            currentUI.gameObject.SetActive(false);
        }
        _uiStack = new Stack<GameObject>();

        Open(startingUI);
    }
    
    private void Update() {
        Debug.Log("UI Stack size : " + _uiStack.Count);
    }
    public void Open(GameObject instance)
    {
        if(instance == null)
        {
            return;
        }
        
        GameObject previous;
        if(_uiStack.TryPeek(out previous))
        {
            if(previous == instance)
            {
                return;
            }
            previous.gameObject.SetActive(false);
        }
        
        instance.gameObject.SetActive(true);
        _uiStack.Push(instance);
    }

    public void Close()
    {
        GameObject currentUI;
        
        if(!_uiStack.TryPop(out currentUI))
        {
            Debug.Log("No UI is active right now.");
            return;
        }
        currentUI.gameObject.SetActive(false);

        GameObject previous;
        if(_uiStack.TryPeek(out previous))
        {
            Debug.Log("Error : No previous UI to open.");
            return;
        }

        previous.gameObject.SetActive(true);
    }


}
