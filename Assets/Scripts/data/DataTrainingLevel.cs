using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataTrainingLevelParam : CsvDataParam
{
	public string training_type;
	public int training_level;
}

public class DataTrainingLevel : CsvData<DataTrainingLevelParam>
{
}
