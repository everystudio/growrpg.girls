using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data/TrainingLevel", menuName = "Data/TrainingLevel")]
public class TrainingLevel : ScriptableObject
{
	public string training_type;
	public string training_name;
	public int level;
}


