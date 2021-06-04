using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

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
}

public class MasterTraining : CsvData<MasterTrainingParam>
{
}
