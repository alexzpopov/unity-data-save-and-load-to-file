using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	// "Player data"
	private float health;
	private float experience;
	
	private String filePath;
	private Text[] labels;


	void Start () 
	{
		health = 100;
		experience = 0;

		// Get all Text GameObjects in Canvas
		GameObject canvas = GameObject.Find ("Canvas");
		labels = canvas.GetComponentsInChildren<Text> ();

		// Save filepath for storing game data
		filePath = Application.persistentDataPath + "/playerInfo.dat";
	}

	// Update Health and Experience labels
	void Update () 
	{
		labels [0].text = "Health: " + health;
		labels [1].text = "Experience: " + experience;
		labels [2].text = "Filepath: " + filePath;
	}

	// Functions to manipulate "Player Data" via buttons
	public void gainHealth () 
	{
		health += 10;
	}

	public void loseHealth ()
	{
		health -= 10;
	}

	public void gainExperience ()
	{
		experience += 10;
	}

	public void loseExperience ()
	{
		experience -= 10;
	}

	//Exit game button function
	public void exitGame () 
	{
		Application.Quit ();
	}

	// Save "Player data" to an external file
	public void Save () 
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (filePath);
	
		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;

		bf.Serialize (file, data);
		file.Close ();
	}

	// Load "Player data" from an external file
	public void Load ()
	{
		if (File.Exists (filePath)) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(filePath, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			health = data.health;
			experience = data.experience;
		}
	}
}

// Helper class for data transformation
[Serializable]
class PlayerData 
{
	public float health;
	public float experience;
}