using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Aim,
    Self
}

public enum CastingMode
{
    Instant,
    Charging,
    Chanelling,
    Holding
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/BasicSkill")]
public abstract class Skill : ScriptableObject
{

    public string Name;
    [TextArea(15,20)]
    public string Description;
    public GameObject ToSpawn;
    public float ManaCost;
    public float Cooldown;

}

[CreateAssetMenu(fileName = "New Damage Skill", menuName = "Skills/Damage Skill")]
public class DamageSkill : Skill
{
    public float Damage;
    public float DamageOverTimeDuration = 0f;
    public Element ElementType;

    public List<Effect> EffectsToApply;
}

