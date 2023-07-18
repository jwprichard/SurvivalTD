using UnityEngine;
using UnityEngine.Events;
using Assets.Scriptables.Units;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Events/UI Event Channel")]
internal class UIEventChannelSO : ScriptableObject
{
    public UnityAction OnBuildInteract = delegate { };


    public void OnButtonClicked(string buildingType)
    {
        OnBuildInteract.Invoke();
    }

}
