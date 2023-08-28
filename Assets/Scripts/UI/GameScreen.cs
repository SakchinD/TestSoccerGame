using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private WinPopup _winPopup;

    private void Awake()
    {
        _winPopup.gameObject.SetActive(false);
    }

    public void UpdateTimeOnSlider(float currentTime, float startTime)
    {
        _timeSlider.value = currentTime / startTime;
    }

    public void UpdateTimeOnText(string time)
    {
        _timeText.text = time;
    }

    public void SetWipPopap(int score)
    {
        _winPopup.SetPlayerScore(score);
        _winPopup.gameObject.SetActive(true);
    }
}
