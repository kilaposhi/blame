using UnityEditor;
using UnityEngine;
using System.Collections;

public class LifeRemainingUI : MonoBehaviour {

	// Script from SimpleHelvetica package that generates 3D Text dynamically
	private SimpleHelvetica simpleHelveticaScript;
	private GameManager gameManager; // Script that manages ship life and distance traveled
	private int currentSpaceshipLife;

	void Start()
	{
		simpleHelveticaScript = GetComponent<SimpleHelvetica>();
		gameManager = FindObjectOfType<GameManager>();
		currentSpaceshipLife = gameManager.GetSpaceshipLife();
	}

	void Update()
	{
		int spaceshipLife = gameManager.GetSpaceshipLife();

		if (currentSpaceshipLife != spaceshipLife) // Need to update the 3D UI for Life Remaining
		{
			simpleHelveticaScript.Text = spaceshipLife + "%";
			simpleHelveticaScript.GenerateText(); // Generate the 3D text
			currentSpaceshipLife = spaceshipLife;
		}
	}
}
