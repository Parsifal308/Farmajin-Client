using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Farmanji.Utilities
{
    public class ShareUtility : MonoBehaviour
    {

        public static IEnumerator TakeScreenShotAndShare(RectTransform rectToScreen, string subjectText)
        {
            yield return new WaitForEndOfFrame();

            Vector2 size = Vector2.Scale(rectToScreen.rect.size, rectToScreen.lossyScale);
            var r = new Rect((Vector2)rectToScreen.position - (size * 0.5f), size);


            Texture2D tx = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);
            tx.ReadPixels(r, 0, 0);
            tx.Apply();

            string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
            File.WriteAllBytes(path, tx.EncodeToPNG());

            
            Destroy(tx); //to avoid memory leaks

            new NativeShare()
                .AddFile(path)
                .SetSubject(subjectText)
                .SetText("Comparte con tus amigos!")
                .Share();


        }
    }
}

