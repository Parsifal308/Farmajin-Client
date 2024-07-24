using System.Collections;
using Newtonsoft.Json;

namespace Farmanji.Data
{
    public class UserProgressPost : ResourcePost
    {
        public PostUserProgressResponse postUserProgressResponse;
        
        public override IEnumerator Post(PostBody body)
        {
            yield return StartCoroutine(_jsonPost.TryPost<PostUserProgressResponse>("", JsonConvert.SerializeObject(body)));
        }

        public void PostTest(PostBody body)
        {
            StartCoroutine(Post(body));
        }
    }
}