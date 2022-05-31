using SurvivalGame.Inventory.Items;
using SurvivalGame.Survival.Mining.Enums;
using UnityEngine;

namespace SurvivalGame.Survival.Mining
{
    [CreateAssetMenu(fileName = "miningTool", menuName = "Items/MiningTool")]
    public class MiningTool : UsableItem
    {
        [SerializeField] private MiningToolType _miningToolType;
        [SerializeField] private float _effectivity;

        public MiningToolType ToolType => _miningToolType;
        public float Effectivity => _effectivity;

        private GameObject _hitArea;

        public void SetHitArea(GameObject hitArea) => _hitArea = hitArea;
        
        public override void Use()
        {
            //Get Target
            if (_hitArea == null)
                return;
            //Play anim
            //Mine Mineable
            var hitAreaSize = _hitArea.GetComponent<BoxCollider>().size;
            Collider[] collidersInHitArea = Physics.OverlapBox(_hitArea.transform.position, hitAreaSize);
            foreach (var colliderInHitArea in collidersInHitArea)
            {
                if (colliderInHitArea.TryGetComponent<IMineable>(out var mineable))
                {
                    mineable.Mine(this);
                }
            }
        }
    }
}