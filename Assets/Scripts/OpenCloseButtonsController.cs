using UnityEngine;

public class OpenCloseButtonsController : MonoBehaviour
{
    [SerializeField] private Animator _openModelsButtonAnimator;
    [SerializeField] private Animator _openSettingsButtonAnimator;

    [SerializeField] private Animator _settingsWindowAnimator;
    [SerializeField] private Animator _modelsWindowAnimator;

    public void OpenSettingsWindow()
    {
        _openSettingsButtonAnimator.SetBool("Open", false);
        _settingsWindowAnimator.SetBool("Open", true);
    }
    public void OpenModelsWindow()
    {
        _openModelsButtonAnimator.SetBool("Open", false);
        _modelsWindowAnimator.SetBool("Open", true);
    }
    public void CloseSettingsWindow()
    {
        _openSettingsButtonAnimator.SetBool("Open", true);
        _settingsWindowAnimator.SetBool("Open", false);
    }
    public void CloseModelsWindow()
    {
        _openModelsButtonAnimator.SetBool("Open", true);
        _modelsWindowAnimator.SetBool("Open", false);
    }
}
