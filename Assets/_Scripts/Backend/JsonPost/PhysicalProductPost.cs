using System.Collections;
using Newtonsoft.Json;

namespace Farmanji.Data
{
    public class PhysicalProductPost : ResourcePost
    {
        public PhysicalProductResponse Response;
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<PhysicalProductResponse>("", JsonConvert.SerializeObject(body)));
            Response = JsonConvert.DeserializeObject<PhysicalProductResponse>(_jsonPost.Response);
        }
    }
}