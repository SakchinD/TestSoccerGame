using PolyAndCode.UI;
using UnityEngine;
using Zenject;

public class ResultsScreen : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] private RecyclableScrollRect _recyclableScrollRect;
    [SerializeField] private int _dataLength;

    private IPlayersScoresSaveManager _scoresSaveManager;

    [Inject]
    private void Construct(IPlayersScoresSaveManager scoresSaveManager)
    {
        _scoresSaveManager = scoresSaveManager;
        _scoresSaveManager?.LoadScore();
    }

    private void OnEnable()
    {
        _recyclableScrollRect.Initialize(this);
    }

    public int GetItemCount()
    {
        return _scoresSaveManager.PlayersScores.Count;
    }

    public void SetCell(ICell cell, int index)
    {
        var item = cell as ResultCell;
        item.ConfigureCell(_scoresSaveManager.PlayersScores[index], index + 1);
    }
}
