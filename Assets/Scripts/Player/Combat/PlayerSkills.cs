using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    //TEMP
    //[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Skill _tempSkill;

    public GameObject GetSkillProjectile()
    {
        return _tempSkill.ToSpawn;
    }
    
}