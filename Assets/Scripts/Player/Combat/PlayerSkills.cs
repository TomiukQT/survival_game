using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    //TEMP
    [SerializeField] private GameObject _bulletPrefab;
    

    public GameObject GetSkillProjectile()
    {
        return _bulletPrefab;
    }
    
}