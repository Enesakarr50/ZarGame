using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start butonunun altına tam ekran panel / Image / TMP koyunca çocuklar tıklamayı "çalar".
/// Sadece Button'un Target Graphic'i tıklanabilir bırakılır; böylece OnClick çalışır.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class StartButtonRaycastGuard : MonoBehaviour
{
    Button _button;

    void Awake() => _button = GetComponent<Button>();

    void OnEnable() => Apply();

    void OnTransformChildrenChanged() => Apply();

#if UNITY_EDITOR
    void OnValidate() => Apply();
#endif

    void Apply()
    {
        if (!isActiveAndEnabled) return;
        if (_button == null) _button = GetComponent<Button>();
        if (_button == null) return;

        Graphic keep = _button.targetGraphic;
        foreach (var g in GetComponentsInChildren<Graphic>(true))
        {
            if (g != null && g != keep)
                g.raycastTarget = false;
        }
    }
}
