using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    [System.Serializable]
    public class GameData
    {
        public string Name;
        public Minigame miniGame;

        public string world;
        public int level;
        public int stage;
        
    }

    public enum Minigame
    {
        TapColor,
        Preguntados,
        Memoria
    }
}

