using Farmanji.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Farmanji.Data
{
    [CreateAssetMenu(menuName = "Customization Data")]
    public class CustomizationData : ScriptableObject
    {
        public PieceData[] PiecesData = new PieceData[1];
        public Accesory[] AccesoriesData = new Accesory[1];
        [FormerlySerializedAs("PetData")] public Pet[] PetsData = new Pet[1];
    }
    
    [System.Serializable]
    public class PieceData
    {
        public AvatarPiece AvatarPiece;
        public Piece[] Pieces = new Piece[1]; 
    }

    [System.Serializable]
    public struct Piece
    {
        public string Name;
        public int ID;
        public Sprite PreviewSprite;
        public Sprite Sprite;
        public Sprite SecondarySprite;
        public Sprite TerciarySprite;
        public bool StartAvailable;
    }
    
    [System.Serializable]
    public struct PieceInfo
    {
        public string Name;
        public int ID;
        public Sprite PreviewSprite;
        public bool StartAvailable;
    }

    [System.Serializable]
    public struct Pet
    {
        public PieceInfo PieceData;
        public Sprite PetHeadSprite;
        public Sprite PetBodySprite;
    }

    [System.Serializable]
    public struct Face
    {
        public PieceInfo PieceData;
        public Sprite FaceSprite;
    }

    [System.Serializable] 
    public struct Hair
    {
        public PieceInfo PieceData;
        public Sprite HairSprite;
    }

    [System.Serializable]
    public struct Body
    {
        public PieceInfo PieceData;
        public Sprite BodySprite;
        public Sprite FrontHandSprite;
        public Sprite BackHandSprite;
    }
    
    [System.Serializable] 
    public struct Legs
    {
        public PieceInfo PieceData;
        public Sprite FrontLegSprite;
        public Sprite BackLegSprite;
    }
    
    [System.Serializable] 
    public struct Accesory
    {
        public PieceInfo PieceData;
        public Sprite AccesorySprite;
        public AccesoryType AccesoryType;
    }
    
    [System.Serializable] 
    public struct Background
    {
        public PieceInfo PieceData;
        public Sprite BackgroundSprite; 
    }
    
    public enum AccesoryType
    {
        Hat,
        FrontHand,
        BackHand,
    }
}