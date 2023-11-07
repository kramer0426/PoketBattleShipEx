using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class EnemyShipDataExcel : ScriptableObject
{
	public List<EnemyShipEntity> Sheet1; // Replace 'EntityType' to an actual type that is serializable.

}
