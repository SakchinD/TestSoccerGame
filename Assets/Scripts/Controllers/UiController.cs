using System.Collections.Generic;
using System;
using UnityEngine;

public enum UiType
{
    MainMenu,
    LevelSelect,
    Game,
    Results,
    Policy
}

public class UiController : MonoBehaviour
{
    [Serializable]
    public class UiView
    {
        public UiType type;
        public GameObject view;
    }
    [SerializeField] private List<UiView> _viewList = new List<UiView>();
    [SerializeField] private TopbarView _topbar;

    private readonly string _levelSelectTopbarName = "Game cards";
    private Stack<UiType> _uiTypeStack = new();

    private void Start()
    {
        ToUi(UiType.MainMenu);
    }

    public void ToUi(UiType uiType)
    {
        var isMainMenu = uiType == UiType.MainMenu;
        
        if (isMainMenu)
        {
            _uiTypeStack.Clear();
        }

        _viewList.ForEach(uiView =>
        {
            var isSame = uiView.type == uiType;
            uiView.view.SetActive(isSame);
            if (isSame)
            {
                _uiTypeStack.Push(uiType);
            }
        });

        _topbar.SetCurrentUiType(uiType);
        _topbar.gameObject.SetActive(!isMainMenu);

        _topbar.SetTopbarHeaderText(SetTopbarText(uiType));
    }

    public void ToPreviosUI()
    {
        if (!_uiTypeStack.TryPop(out var type))
        {
            return;
        }

        if (_uiTypeStack.TryPop(out var result))
        {
            ToUi(result);
        }
    }

    private string SetTopbarText(UiType uiType)
    {
        switch(uiType)
        {
            case UiType.LevelSelect:
                return _levelSelectTopbarName;
            case UiType.Results:
            case UiType.Policy:
                return uiType.ToString();
            default: return string.Empty;
        }
    }
}
