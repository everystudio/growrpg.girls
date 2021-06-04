using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataManager : Singleton<DataManager>
{
	public TextAsset m_taMasterTraining;

	public MasterTraining masterTraining;
	public override void Initialize()
	{
		base.Initialize();
		masterTraining = new MasterTraining();
		masterTraining.Load(m_taMasterTraining);
	}
}
