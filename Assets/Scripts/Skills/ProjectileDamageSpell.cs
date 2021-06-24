using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Spell", menuName = "Spells/Damage Spell")]
public class ProjectileDamageSpell : Spell
{
    [Header("Damage SPELL")]
    public float Damage;
    public float DamageOverTimeDuration = 0f;
    public Element ElementType;
    
    public GameObject ToSpawn;
    
    public List<Effect> EffectsToApply;

    public override void Cast(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Instantiate(ToSpawn, position, Quaternion.identity);
        projectile.GetComponent<Bullet>()?.Setup(direction,Damage);
    }
}