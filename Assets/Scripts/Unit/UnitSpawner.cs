using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	[SerializeField]
	private	GameObject	unitPrefab;
	[SerializeField]
	private	int			maxUnitCount;

	private	Vector2		minSize = new Vector2(-10, -10);
	private	Vector2		maxSize = new Vector2(10, 10);

	public List<UnitController> SpawnUnits()
	{	
		List<UnitController> unitList = new List<UnitController>(maxUnitCount);

		for ( int i = 0; i < maxUnitCount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y));

			Instantiate(unitPrefab, position, Quaternion.identity);
			UnitController	unit	= unitPrefab.GetComponent<UnitController>();

			unitList.Add(unit);
		}

		return unitList;
	}
}

