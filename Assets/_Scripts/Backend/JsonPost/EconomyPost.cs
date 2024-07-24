using System.Collections;
using Newtonsoft.Json;

namespace Farmanji.Data
{
    public class EconomyPost : ResourcePost
    {
        public EconomyResponse EconomyResponse;
        public EconomyBody body;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<EconomyResponse>("", JsonConvert.SerializeObject(body)));
        }
    }
}