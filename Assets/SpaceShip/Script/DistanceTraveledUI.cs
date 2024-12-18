using UnityEditor;
using UnityEngine;
using System.Collections;

public class DistanceTraveledUI : MonoBehaviour {

	// Script from SimpleHelvetica package that generates 3D Text dynamically
	private SimpleHelvetica simpleHelveticaScript;
	private GameManager gameManager; // Script that manages ship life and distance traveled
	private int currentDistanceTraveled;

	void Start()
	{
		simpleHelveticaScript = GetComponent<SimpleHelvetica>();
		gameManager = FindObjectOfType<GameManager>();
		currentDistanceTraveled = gameManager.GetDistanceTraveled();
	}

	void Update()
	{
		int distanceTraveled = gameManager.GetDistanceTraveled();

		if (currentDistanceTraveled != distanceTraveled) // Need to update the 3D UI for Life Remaining
		{
			simpleHelveticaScript.Text = distanceTraveled + " m";
			simpleHelveticaScript.SpaceWidth = 350; // To avoid the "m" to cross the distance
			simpleHelveticaScript.GenerateText(); // Generate the 3D text
			currentDistanceTraveled = distanceTraveled;
		}
	}
}
