using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataManager : Singleton<DataManager>
{
	public TextAsset m_taMasterTraining;
	public MasterTraining masterTraining;


	public TextAsset m_taDataUnitTrainingParam;
	public DataUnitTrainingParam unitTrainingParam; 
	public override void Initialize()
	{
		base.Initialize();
		masterTraining = new MasterTraining();
		masterTraining.Load(m_taMasterTraining);

		DataUnitTraining dataUnitTraining = new DataUnitTraining();
		dataUnitTraining.Load(m_taDataUnitTrainingParam);
		if(dataUnitTraining.list.Count == 1)
		{
			unitTrainingParam = dataUnitTraining.list[0];
		}
		else
		{
			Debug.LogError("デバッグ用のデータ数に誤りがあります。一人だけですよ");
		}

		//Debug.Log(unitTrainingParam.stamina);
	}
}
