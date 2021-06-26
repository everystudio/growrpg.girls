using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowHUD : MonoBehaviour
{
	public Button m_btnBack;
	public Button m_btnRest;
	public Button m_btnTraining;

	public List<BtnTraining> m_btnTrainingList;
	public EventTrainingLevel OnTrainingLevel = new EventTrainingLevel();

	public List<IconStatus> m_iconStatusList;

	public SliderController m_slStamina;

	public GameObject m_trainingResultBoard;
	public GameObject m_goCover;

	#region おやすみ関連
	public Button m_btnRestCancel;
	public Button m_btnRestDecide;
	#endregion

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
		UpTrainingButton(null, 0);
	}

	public void UpTrainingButton(DataTrainingLevelParam _trainingLevel , int _iFailRate )
	{
		foreach( BtnTraining btn in m_btnTrainingList)
		{
			bool bUpFlag = false;
			if(_trainingLevel != null)
			{
				bUpFlag = btn.trainingLevel.training_type == _trainingLevel.training_type;
			}
			btn.IsUp(
				 bUpFlag,
				_iFailRate);
		}
	}

}
