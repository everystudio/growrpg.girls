using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using anogamelib;
using System.Reflection;

public class GrowMain : StateMachineBase<GrowMain>
{
	public UnityEvent e = new UnityEvent();
	[SerializeField] GrowHUD m_hudGrow;
	public DataUnitTrainingParam unitTrainingParam
	{
		get
		{
			return DataManager.Instance.unitTrainingParam;
		}
	}

	private void Awake()
	{
		SetState(new GrowMain.Standby(this));
	}

	private int CalcFailRate(TrainingLevel _level, int _iStamina)
	{
		int iFailRate = 0;
		if (_iStamina < _level.fail_start)
		{
			iFailRate = (int)Mathf.Lerp(99, 0, (float)_iStamina / (float)_level.fail_start);
		}
		return iFailRate;
	}

	private class Standby : StateBase<GrowMain>
	{
		public Standby(GrowMain _machine) : base(_machine)
		{
		}
		public override void OnUpdateState()
		{
			base.OnUpdateState();
			machine.SetState(new GrowMain.TopMenu(machine));
		}
	}


	private class TopMenu : StateBase<GrowMain>
	{
		public TopMenu(GrowMain _machine) : base(_machine)
		{
		}
		public override void OnEnterState()
		{
			UIAssistant.Instance.ShowPage("main");

			foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
			{
				icon.ShowUp(0);
				icon.SetParam(machine.unitTrainingParam);
			}
			machine.m_hudGrow.m_btnTraining.onClick.AddListener(() =>
			{
				machine.SetState(new GrowMain.TrainingList(machine));
			});

			// スタミナ(体力)の表示設定
			machine.m_hudGrow.m_slStamina.Init(
				DataManager.Instance.unitTrainingParam.stamina,
				DataManager.Instance.unitTrainingParam.stamina_max
				);

		}
		public override void OnExitState()
		{
			machine.m_hudGrow.m_btnTraining.onClick.RemoveAllListeners();
		}

	}

	private class TrainingList : StateBase<GrowMain>
	{
		private TrainingLevel m_selectTrainingLevel;
		public TrainingList(GrowMain _machine) : base(_machine)
		{
		}

		public override void OnEnterState()
		{
			m_selectTrainingLevel = null;
			UIAssistant.Instance.ShowPage("training_top");
			machine.m_hudGrow.UpdateTrainingList();
			foreach( IconStatus icon in machine.m_hudGrow.m_iconStatusList)
			{
				icon.ShowUp(0);
			}
			machine.m_hudGrow.m_btnBack.onClick.AddListener(() =>
			{
				machine.SetState(new GrowMain.TopMenu(machine));
				UIAssistant.Instance.ShowParentPage();
			});
			machine.m_hudGrow.OnTrainingLevel.AddListener((value) =>
			{
				if (m_selectTrainingLevel == value)
				{
					machine.SetState(new GrowMain.TrainingExe(machine, value));
				}
				else
				{
					m_selectTrainingLevel = value;

					int iFailRate = machine.CalcFailRate(
						m_selectTrainingLevel,
						DataManager.Instance.unitTrainingParam.stamina);

					machine.m_hudGrow.UpTrainingButton(value , iFailRate);

					MasterTrainingParam param = DataManager.Instance.masterTraining.list.Find(p =>
					p.training_type == value.training_type && p.training_level == value.level);
					foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
					{
						FieldInfo info = param.GetType().GetField(icon.paramName);
						icon.ShowUp((int)info.GetValue(param));
					}
				}
			});
		}
		public override void OnExitState()
		{
			foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
			{
				icon.ShowUp(0);
			}
			machine.m_hudGrow.m_btnBack.onClick.RemoveAllListeners();
			machine.m_hudGrow.OnTrainingLevel.RemoveAllListeners();
		}
	}

	private class TrainingExe : StateBase<GrowMain>
	{
		private TrainingLevel m_trainingLevel;
		public TrainingExe(GrowMain machine, TrainingLevel value) : base(machine)
		{
			m_trainingLevel = value;
		}
		public override void OnEnterState()
		{
			base.OnEnterState();
			//Debug.Log(m_trainingLevel.training_name);
			//Debug.Log(m_trainingLevel.level);
			int iFailRate = machine.CalcFailRate(
				m_trainingLevel,
				DataManager.Instance.unitTrainingParam.stamina);

			int iRand = Random.Range(0, 100);
			// 成功したら

			bool bIsSuccess = iFailRate < iRand;
			MasterTrainingParam param = DataManager.Instance.masterTraining.list.Find(p =>
			p.training_type == m_trainingLevel.training_type && p.training_level == m_trainingLevel.level);

			if (bIsSuccess)
			{
				machine.unitTrainingParam.BuildTraining(param);
			}
			else
			{
				// 失敗した処理
			}

			// スタミナは成功失敗に関わらず減らす
			DataManager.Instance.unitTrainingParam.stamina += param.training_cost;
			machine.m_hudGrow.m_slStamina.SetValue(
				DataManager.Instance.unitTrainingParam.stamina
			);


			foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
			{
				icon.ShowUp(0);
				icon.SetParam(machine.unitTrainingParam);
			}
			machine.SetState(new GrowMain.TopMenu(machine));
		}

	}

}
