using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrainingUnit" , menuName ="ScriptableObject/TrainingUnit")]
public class DataTrainingUnit : DataUnitAttribute
{
	public int stamina;
	public int stamina_max;

	public int motivation;
}
