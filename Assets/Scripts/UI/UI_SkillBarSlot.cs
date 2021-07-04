using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OnSpellDropEventArgs : EventArgs
{
    public Spell DroppedSpell;
    public int Index;
}

public class UI_SkillBarSlot : MonoBehaviour, IDropHandler
{
    private Image _image;
    
    private Image _cooldownFiller;
    private TextMeshProUGUI _cooldownText;

    private float _cooldown;

    public event EventHandler<OnSpellDropEventArgs> OnSpellDrop;
    private int _index;
    
    private void Awake()
    {
        _image = transform.Find("icon").GetComponent<Image>();
        
        Transform filler = transform.Find("cooldown_filler");
        _cooldownFiller = filler.GetComponent<Image>();
        _cooldownText = filler.GetComponentInChildren<TextMeshProUGUI>();

        _index = transform.GetSiblingIndex();
    }

    private void Start()
    {
        CooldownToggle(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        //Debug.Log(eventData.pointerDrag);

        Spell spell = eventData.pointerDrag.GetComponent<UI_SkillButton>().Spell;
        _image.sprite = spell.Icon;
        OnSpellDrop?.Invoke(this, new OnSpellDropEventArgs(){DroppedSpell = spell, Index = _index});
        //Debug.Log($"Spell {spell.Name} was dropped to slot {gameObject}");
        //OnSpellCast(spell.Cooldown);
    }

    
    public void OnSpellCast(float cooldown)
    {
        _cooldown = cooldown;
        CooldownToggle(true);
        StartCoroutine(CooldownTimer(cooldown));
    }

    private IEnumerator CooldownTimer(float cooldown)
    {
        float currTime = cooldown;
        float STEP_SIZE = 0.1f;
        while (currTime > 0f)
        {
            _cooldownFiller.fillAmount = currTime/cooldown;
            _cooldownText.text = $"{currTime:F1}";
            yield return new WaitForSeconds(STEP_SIZE);
            currTime -= STEP_SIZE;
        }
        CooldownToggle(false);
    }

    private void CooldownToggle(bool state) => _cooldownFiller.gameObject.SetActive(state);

}
