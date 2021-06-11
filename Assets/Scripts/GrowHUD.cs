using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowHUD : MonoBehaviour
{
	public Button m_btnBack;
	public Button m_btnTraining;

	public List<BtnTraining> m_btnTrainingList;
	public EventTrainingLevel OnTrainingLevel = new EventTrainingLevel();

	public List<IconStatus> m_iconStatusList;

	private void Awake()
	{
		foreach(BtnTraining btnTraining in m_btnTrainingList)
		{
			btnTraining.OnTrainingLevel.AddListener((value) =>
			{
				OnTrainingLevel.Invoke(value);
			});
		}
	}

	public void UpdateTrainingList()
	{
		foreach( BtnTraining btn in m_btnTrainingList)
		{
			btn.ShowUpdate();
		}
	}

	public void UpTrainingButton(TrainingLevel _trainingLevel)
	{
		foreach( BtnTraining btn in m_btnTrainingList)
		{
			btn.IsUp(btn.trainingLevel.training_type == _trainingLevel.training_type);
		}
	}

}
