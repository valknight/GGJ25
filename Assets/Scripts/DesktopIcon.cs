using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour
{
    private ApplicationDefinition _definition;

    public Image iconImage;
    public TMP_Text nameText;
    private GameObject targetCanvas;
    
    public void Init(ApplicationDefinition definition)
    {
        _definition = definition;
        iconImage.sprite = _definition.applicationIcon;
        nameText.text = _definition.applicationId;
        targetCanvas = GameObject.Find("Desktop");
    }

    public void OnIconClicked()
    {
        if (ApplicationManager.TryGetWindowForApplication(_definition.applicationId, out var window))
        {
            if (!window.gameObject.activeSelf)
                window.gameObject.SetActive(true);
            window.transform.SetAsLastSibling();
            return;
        } 
        var newWindow= Instantiate(_definition.applicationWindow.gameObject, targetCanvas.transform).transform;
        newWindow.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
