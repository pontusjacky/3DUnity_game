using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class PanelBase : MonoBehaviour
{
    protected CanvasGroup cg;

    protected virtual void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
    }

    public virtual void Open()  => gameObject.SetActive(true);
    public virtual void Close() => gameObject.SetActive(false);
}
