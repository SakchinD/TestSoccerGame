using PolyAndCode.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultCell : MonoBehaviour, ICell
{
    private readonly Color _firstPlaceColor = new Color(0.749f, 0.243f, 0.216f);
    private readonly Color _secondPlaceColor = new Color(0.141f, 0.812f, 0.369f);
    private readonly Color _thirdPlaceColor = new Color(0.945f, 0.353f, 0.165f);

    [SerializeField] private TMP_Text _placeByResultText;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerBallCountText;
    [SerializeField] private Image _placeIcon;

    private Color _startColor;

    private void Awake()
    {
        _startColor = _placeIcon.color;
    }

    public void ConfigureCell(PlayerScore playerScore, int cellIndex)
    {
        _placeByResultText.text = cellIndex.ToString();
        _playerNameText.text = playerScore.Name;
        _playerBallCountText.text = playerScore.Score.ToString();

        _placeIcon.color = cellIndex switch
        {
            1 => _firstPlaceColor,
            2 => _secondPlaceColor,
            3 => _thirdPlaceColor,
            _ => _startColor
        };
    }
}
