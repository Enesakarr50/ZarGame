using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class AnimateUI : MonoBehaviour
{
   
    RectTransform rectTransform;
    Vector2 size;

    
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        rectTransform.localPosition = Vector3.up * (size.y + 10);
    }

    private void OnEnable() {
        ShowPanel();
    }

    public void ShowPanel(){
        // Sequence inAndOut = DOTween.Sequence();
        // inAndOut.Append(rectTransform.DOAnchorPosY(0, 0.7f));
        // inAndOut.AppendInterval(1f);
        // inAndOut.Append(rectTransform.DOAnchorPosY(size.y + 10, 0.7f));

        rectTransform.DOAnchorPosY(0, 1f);
    }

    
}

#if UNITY_EDITOR
[CustomEditor(typeof(AnimateUI))]
public class AnimateUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimateUI obj = (AnimateUI)target;

        if(GUILayout.Button("Show")){
            obj.ShowPanel();
        }
    }
}
#endif