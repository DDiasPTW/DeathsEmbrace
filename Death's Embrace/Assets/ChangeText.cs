using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text sacrificeText;
    public string text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        sacrificeText.text = text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sacrificeText.text = " ";
    }
}
