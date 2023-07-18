using UnityEngine;
using UnityEngine.EventSystems;

public enum InteractionType { None = 0, Select, Build}

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private InputReaderSO _inputReader = default;

    [Header("Broadcasting on")]
    [SerializeField] private BuildingEventChannelSO _onBuildInteract = default;
    [SerializeField] private SelectEventChannelSOcs _onSelect = default;

    [Space]
    public InteractionType currentInteractionType;

    private bool IsOverUI;

    private void OnEnable()
    {
        _inputReader.InteractEvent += OnInteractionButtonPressed;
        currentInteractionType = InteractionType.Build;
    }

    public void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            IsOverUI = true;
        }
        else
        {
            IsOverUI = false;
        }
    }

    private void OnInteractionButtonPressed()
    {

        switch (currentInteractionType)
        {
            case InteractionType.None: break;
            case InteractionType.Select:
                {
                    _onSelect.RaiseEvent();
                    break;
                }
            case InteractionType.Build:
                {
                    if (IsOverUI)
                    {
                        currentInteractionType = InteractionType.Select; 
                        break;
                    }
                    _onBuildInteract.RaiseBuildInteractEvent();
                    break;
                }
            default:
                {
                    throw new System.ArgumentOutOfRangeException();
                }
        }
    }

}
