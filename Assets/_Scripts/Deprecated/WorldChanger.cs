using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;

namespace Farmanji.UI
{
    public class WorldChanger : MonoBehaviour
    {
        public Sprite[] LevelsTitle;


        public WorldData[] worlds;

        public TextMeshProUGUI currentWorldText;
        public Image backgroundReference;
        public Image adventureBackgroundReference;
        public Image worldPlatformImage;
        public Image worldPlatformImageAdventure;
        public TextMeshProUGUI levelText;
        public Image levelImageTitle;


        private void Start()
        {
            
        }


        public void SetActiveWorld(int _index)
        {
            WorldData world = worlds[_index];
            currentWorldText.text = world.name;
            backgroundReference.sprite = world.background;
            adventureBackgroundReference.sprite = world.background;
            levelText.text = "NIVEL " + world.level.ToString();

            worldPlatformImage.sprite = world.platformImage;
            worldPlatformImageAdventure.sprite = world.platformImage;

            levelImageTitle.sprite = LevelsTitle[world.level - 1];
        }
    }
}

