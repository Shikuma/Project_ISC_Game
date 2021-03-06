﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGamesData : MonoBehaviour {
	public string[] games;
	public string getColumn, gamesDataText;
	public List<ScoreObject> scores;
	public ScoreObject[] sortedScores;
	leaderBoardHandler handler;
	int iteration;
	public Text debugtext;

	void Start() {
		handler = gameObject.GetComponent<leaderBoardHandler>();
		WWW gamesData = new WWW("http://104.236.217.201/ISC_GetHighScores.php");
		StartCoroutine(MyCoroutine(gamesData));

		//Change to this when switching to live
		//WWW gamesData = new WWW("http://104.236.217.201/ISC_GetHighScores.php");

	}

	private IEnumerator MyCoroutine(WWW www) {
		yield return www;
		gamesDataText = www.text;
		scores = new List<ScoreObject>();
		games = gamesDataText.Split(';');
		//debugtext.text = gamesDataText;
		//Debug.Log(gamesDataText);
		//Get all scores
		//getColumn = "score";
		for (int i = 0; i < games.Length - 1; i++) {
			if (games[i] != "" || games[i] == null) {
				//Add scores and firstname/lastname to list of ScoreObjects
				ScoreObject obj = new ScoreObject(
					int.Parse(GetDataValue(games[i], ("GameID:"))),
					float.Parse(GetDataValue(games[i], ("score:"))),
					int.Parse(GetDataValue(games[i], ("user_id:"))),
					GetDataValue(games[i], ("Date_complete:")),
					GetDataValue(games[i], ("first_name:")),
					GetDataValue(games[i], ("last_name:"))
					);
				scores.Add(obj);

			}
		}
		Debug.Log("Finished setting games");

		handler.SetData(SortScores());
	}

	//Index will be the column desired from the database
	//Take in the required index.
	//Remove any part of the string after '|' which is the start of a new index
	string GetDataValue(string data, string index) {
		//Take a piece of a string. index is the string it takes in. +Length will take the rest of the string.
		string value = data.Substring(data.IndexOf(index) + index.Length);
		if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
		iteration++;
		//print(iteration + " " + value);
		return value;
	}

	public ScoreObject[] SortScores() {
		ScoreObject[] sorted = new ScoreObject[scores.Count];
		//Set sorted array
		for (int i = 0; i < sorted.Length; i++) {
			sorted[i] = scores[i];
		}
		//Sort
		for (int i = 0; i < sorted.Length; i++) {
			for (int j = i + 1; j < sorted.Length; j++) {
				if ((sorted[i].score > sorted[j].score) && (i != j)) {
					ScoreObject temp = sorted[j];
					sorted[j] = sorted[i];
					sorted[i] = temp;
				}
			}
		}
		//print(sorted[11].score);
		sortedScores = sorted;
		Debug.Log("Finished sorting");
		return sorted;
	}
}
