using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EventTrainingLevel : UnityEvent<TrainingLevel>
{
	public EventTrainingLevel(){}
}

public class BtnTraining : MonoBehaviour
{
	public EventTrainingLevel OnTrainingLevel = new EventTrainingLevel();
	[SerializeField] private TrainingLevel m_trainingLevel;
	public TrainingLevel trainingLevel { get { return m_trainingLevel; } }
	[HideInInspector] private Button m_btn;
	[HideInInspector] public TextMeshProUGUI m_txtLevel;
	[HideInInspector] public TextMeshProUGUI m_txtTrainingMenu;
	private Animator m_animator;
	private TextMeshProUGUI m_txtFailRate;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_btn = GetComponent<Button>();
		m_btn.onClick.AddListener(() =>
		{
			OnTrainingLevel.Invoke(m_trainingLevel);
		});
		m_txtLevel = transform.Find("txtLevel").GetComponent<TextMeshProUGUI>();
		m_txtTrainingMenu = transform.Find("txtTrainingMenu").GetComponent<TextMeshProUGUI>();
		m_txtFailRate = transform.Find("imgFukidashi/txtFailRate").GetComponent<TextMeshProUGUI>();
	}
	public void ShowUpdate()
	{
		m_txtLevel.text = $"Level.{m_trainingLevel.level}";
		//Debug.Log(m_trainingLevel.training_type);
		MasterTrainingParam param = DataManager.Instance.masterTraining.list.Find(p => 
		p.training_type == m_trainingLevel.training_type &&
		p.training_level == m_trainingLevel.level);

		m_txtTrainingMenu.text = param.training_name;
	}
	public void IsUp(bool _bFlag, int _iFailRate)
	{
		m_animator.SetBool("isUp", _bFlag);
		if (_bFlag)
		{
			m_txtFailRate.text = $"{_iFailRate}%";
		}
	}
}
