using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Woodsman : NPC {

	private bool hasWholeAxe = false;
	private float chopTimer = 0.0f;
	[SerializeField]
	private GameObject woodContainer;

	[SerializeField]
	private const float addWoodInterval = 120.0f;
	
	void Update ()
	{
		if (hasWholeAxe == true)
		{
			chopTimer += Time.deltaTime;
			if (chopTimer >= addWoodInterval)
			{
				woodContainer.GetComponent<Container>().AddItem(ItemType.Wood, 5);
				chopTimer = 0.0f;
	
				string variableName = "$woodcontainer_amount";
				FindObjectOfType<VariableStorage>().SetValue(variableName, new Yarn.Value(woodContainer.GetComponent<Container>().GetCount()));
			}
		}
	}

	[YarnCommand("chop")]
	public void StartChopping()
	{
		hasWholeAxe = true;
	}
}
