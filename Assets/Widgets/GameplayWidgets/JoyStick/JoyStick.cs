using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public delegate void InputUpdatedDelegate(Vector2 inputVal);
    public event InputUpdatedDelegate OnInputUpdated;

    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;
    [SerializeField] private RectTransform CenterTransform;
    [SerializeField] private float deadZone = 0.2f;

    private float _range;

    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x / 2;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rangeTransform.position = eventData.pressPosition;
        thumbStickTransform.position = eventData.pressPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        thumbStickTransform.localPosition = Vector2.zero;
        rangeTransform.localPosition = Vector2.zero;
        OnInputUpdated.Invoke(Vector2.zero);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = eventData.position - eventData.pressPosition;
        
        offset = Vector2.ClampMagnitude(offset, _range);
        thumbStickTransform.position = eventData.pressPosition + offset;
        Vector2 input = offset/_range;
        if (input.sqrMagnitude < deadZone)
        {
            return;
        }

        OnInputUpdated.Invoke(offset/_range);
    }

}
