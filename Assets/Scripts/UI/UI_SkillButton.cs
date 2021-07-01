using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Spell _spell;
    
    //private Image _icon;
    private TextMeshProUGUI _skillName;
    private TextMeshProUGUI _skillDesc;

    private Canvas _canvas;

    private RectTransform _movingIcon;
    private CanvasGroup _movingIconCanvasGroup;

    private void Awake()
    {
        _canvas = GameObject.Find("UI").GetComponent<Canvas>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Tooltip show
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Tooltip disable
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject icon = transform.Find("icon").gameObject;
        _movingIcon = Instantiate(icon, gameObject.GetComponent<RectTransform>().position, Quaternion.identity,transform.parent).GetComponent<RectTransform>();
        _movingIcon.gameObject.AddComponent<SpellContainer>().SetSpell(_spell);        
        
        _movingIconCanvasGroup = _movingIcon.gameObject.AddComponent<CanvasGroup>();
        _movingIconCanvasGroup.alpha = 0.6f;
        _movingIconCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _movingIcon.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_movingIcon.gameObject);
    }

    public Spell Spell => _spell;


}
