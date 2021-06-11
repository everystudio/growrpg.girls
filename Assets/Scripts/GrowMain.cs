using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using System.Reflection;

public class GrowMain : StateMachineBase<GrowMain>
{
	[SerializeField] GrowHUD m_hudGrow;
	public DataTrainingUnit m_trainingUnit;
	private void Awake()
	{
		SetState(new GrowMain.Standby(this));
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
			foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
			{
				icon.ShowUp(0);
				icon.SetParam(machine.m_trainingUnit);
			}


			machine.m_hudGrow.m_btnTraining.onClick.AddListener(() =>
			{
				machine.SetState(new GrowMain.TrainingList(machine));
			});
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

					machine.m_hudGrow.UpTrainingButton(value);

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

		}

	}

}
