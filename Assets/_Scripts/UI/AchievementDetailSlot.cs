using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farmanji.Data;
using UnityEngine.Serialization;
using System.IO;

namespace Farmanji.Game
{
    public class AchievementDetailSlot : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private Image icon;
        [FormerlySerializedAs("_name")] [SerializeField] private TextMeshProUGUI _nameText;
        [FormerlySerializedAs("details")] [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button shareButton;
        #endregion

        #region UNITY
        private void Start()
        {
            shareButton.onClick.AddListener(Share);
        }
        #endregion


        #region METHODS
        public void InitializeDetail(AchievementData data)
        {
            _nameText.SetText(data.Name);
            _descriptionText.SetText(data.Description);
            icon.sprite = data.Image;
        }

        private void Share()
        {
            RectTransform rect = GetComponent<RectTransform>();
            StartCoroutine(Utilities.ShareUtility.TakeScreenShotAndShare(rect, "Mi insignia!"));

        }

        IEnumerator TakeScreenShotAndShare()
        {
            yield return new WaitForEndOfFrame();

            Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tx.Apply();

            string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
            File.WriteAllBytes(path, tx.EncodeToPNG());

            Destroy(tx); //to avoid memory leaks

            new NativeShare()
                .AddFile(path)
                .SetSubject("Esta es mi insignia!")
                .SetText("Comparte con tus amigos!")
                .Share();


        }

        #endregion
    }
}