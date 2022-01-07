using UnityEngine;
using UnityEngine.Events;
using static PreferencesManager;

public class UIPanelComponent : MonoBehaviour
{

    public bool EnabledDefault;

    public GameAction GameAction;

    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    private void Awake()
    {
        if (EnabledDefault)
            Show();
        else
            Hide();
    }

    public void TogglePanel()
    {
        if (gameObject.activeInHierarchy)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnOpen.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        OnClose.Invoke();
    }

    public bool IsOpen()
    {
        return gameObject.activeInHierarchy;
    }

}

