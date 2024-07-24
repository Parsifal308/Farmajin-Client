using Farmanji.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public class GamesLoader : ResourceLoader
    {
        public override IEnumerator Load()
        {
            _jsonLoader.LoadFromWeb<GamesCollection>();



            yield return new WaitForSeconds(1f);
        }
    }

}
