using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContainer : MonoBehaviour
{
    private Spell _spell;
    public void SetSpell(Spell spell) => _spell = spell;
    public Spell Spell => _spell;
}
