using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using System;

public class MasterTrainingParam : CsvDataParam
{
	public int training_id;
	public int training_level;
	public string training_type;
	public string training_name;

	public int strength;
	public int vital;
	public int agility;
	public int wisdom;
	public int luck;
	public int training_cost;
	public int fail_start;
}

public class MasterTraining : CsvData<MasterTrainingParam>
{
	public MasterTrainingParam Get(DataTrainingLevelParam level)
	{
		return list.Find(p => p.training_type == level.training_type &&
		p.training_level == level.training_level);
	}
}
