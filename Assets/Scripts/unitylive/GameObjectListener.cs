using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectListener : MonoBehaviour
{
    public ParamListener paramListener;
    void Start()
    {
        if (paramListener != null)
        {
            paramListener.EventGameObject.AddListener((value) =>
            {
                Debug.Log(value.name);
            });
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
