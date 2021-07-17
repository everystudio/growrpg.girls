using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UnityEventGameObject : UnityEvent<GameObject> { }

public class ParamListener : MonoBehaviour
{
    public TextMeshProUGUI m_txt;

    public UnityEventGameObject EventGameObject = new UnityEventGameObject();
    public Slider slider;
    void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            EventGameObject.Invoke(this.gameObject);
            m_txt.text = ((int)value).ToString();
        });
    }
}
