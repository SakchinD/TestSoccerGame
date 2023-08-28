using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    protected UiController _uiController;

    protected void SetCanvasGroupInteractable(bool value)
    {
        _canvasGroup.interactable = value;
    }

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
}
