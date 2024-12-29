using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRefPainting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteFunction()
    {
        UIManager.Instance.ShowNotification("SelectRefPainting");
        Debug.Log("SelectRefPainting executed.");
    }
}
