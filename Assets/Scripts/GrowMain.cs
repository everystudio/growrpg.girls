using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using System.Reflection;

public class GrowMain : StateMachineBase<GrowMain>
{
	[SerializeField] GrowHUD m_hudGrow;
	private void Awake()
	{
		SetState(new GrowMain.TopMenu(this));
	}

	private class TopMenu : StateBase<GrowMain>
	{
		public TopMenu(GrowMain _machine) : base(_machine)
		{
		}
		public override void OnEnterState()
		{
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
		public TrainingList(GrowMain _machine) : base(_machine)
		{
		}
		public override void OnEnterState()
		{
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
				Debug.Log(value.training_type);
				Debug.Log(value.level);

				MasterTrainingParam param = DataManager.Instance.masterTraining.list.Find(p =>
				p.training_type == value.training_type && p.training_level == value.level);

				Debug.Log(param.training_name);
				//Debug.Log(param.strength);

				foreach (IconStatus icon in machine.m_hudGrow.m_iconStatusList)
				{
					/*
					Debug.Log(icon.paramName);
					Debug.Log(param.GetType());
					Debug.Log(param.GetType().GetField("strength"));
					Debug.Log(param.GetType().GetField(icon.paramName.Trim()));
					Debug.Log(info);
					Debug.Log(info.GetValue(param));
					*/
					FieldInfo info = param.GetType().GetField(icon.paramName);
					icon.ShowUp((int)info.GetValue(param));
				}

			});
		}
		public override void OnExitState()
		{
			machine.m_hudGrow.m_btnBack.onClick.RemoveAllListeners();
			machine.m_hudGrow.OnTrainingLevel.RemoveAllListeners();
		}

	}
}
