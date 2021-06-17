using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataUnitTrainingParam : UnitProperty
{
	public int chara_id;
	public int stamina;
	public int stamina_max;

	public int motivation;

	public void BuildTraining(MasterTrainingParam _training)
	{
		strength += _training.strength;
		vital += _training.vital;
		agility += _training.agility;
		wisdom += _training.wisdom;
		luck += _training.luck;
	}
}

public class DataUnitTraining : CsvData<DataUnitTrainingParam>
{
}
