using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct PlayerScore
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerScore(string name, int score)
    {
        Name = name;
        Score = score;
    }
}

[Serializable]
public class SaveData
{
    public List<PlayerScore> savedPlayersScores = new();
}

public interface IPlayersScoresSaveManager
{
    void SaveScore(string name, int score);
    void LoadScore();

    List<PlayerScore> PlayersScores { get; }
}

public class PlayersScoresSaveManager : IPlayersScoresSaveManager
{
    private readonly string _savePath = Application.persistentDataPath + "/MySaveData.dat";

    private List<PlayerScore> _playersScores = new();

    public List<PlayerScore> PlayersScores => _playersScores;

    public void SaveScore(string name, int score)
    {
        var playerScore = new PlayerScore(name,score);
        _playersScores.Add(playerScore);
        _playersScores = _playersScores
            .OrderByDescending(x => x.Score)
            .ToList();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(_savePath);
        SaveData data = new SaveData();
        data.savedPlayersScores = _playersScores;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadScore()
    {
        if (File.Exists(_savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_savePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _playersScores = data.savedPlayersScores;
            Debug.Log("Game data loaded!");
        }
    }
}