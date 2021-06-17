using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderController : MonoBehaviour
{
	private Slider m_slider;
	private void Awake()
	{
		m_slider = GetComponent<Slider>();
	}
	public void Init(float _fValue , float _fMax)
	{
		m_slider.maxValue = _fMax;
		SetValue(_fValue);
	}
	public void SetValue( float _fValue)
	{
		m_slider.value = _fValue;
	}
}
