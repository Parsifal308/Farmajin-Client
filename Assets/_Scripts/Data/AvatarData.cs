using UnityEngine;

namespace Farmanji.Data
{
    [CreateAssetMenu(menuName = "Avatar Data")]
    public class AvatarData : ScriptableObject
    {
        [SerializeField] private AvatarCustomizationData _avatarCustomizationData;
        
        public void SaveAvatarData(AvatarCustomizationData DataToSave)
        {
            _avatarCustomizationData = DataToSave;
        }

        public AvatarCustomizationData LoadAvatarData()
        {
            return _avatarCustomizationData;
        }
    }

    [System.Serializable]
    public struct AvatarCustomizationData
    {
        public Sprite HeadSprite;
        public Sprite FaceSprite;
        public Sprite BeardSprite;
        public Sprite HairSprite;
        public Sprite HatSprite;
        public Sprite BodySprite;
        public Sprite FrontHandSprite;
        public Sprite BackHandSprite;
        public Sprite FrontLegSprite;
        public Sprite BackLegSprite;
        public Sprite FrontItemSprite;
        public Sprite BackItemSprite;
        public Sprite PetHeadSprite;
        public Sprite PetBodySprite;
        public Color FaceColor;
        public Color HairColor;
    }
}