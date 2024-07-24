using System.Collections;

namespace Farmanji.Data
{
    public class UserProgressLoader : ResourceLoader
    {
        public UserProgressData UserProgressData;
        
        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<UserProgressCollection>());

            yield return CreateConcreteData();
        }
        
        public override IEnumerator CreateConcreteData()
        {
            if (_jsonLoader.ResponseInfo is UserProgressCollection response)
            {
                var data = new UserProgressData(response.data);
                UserProgressData = data;
            }
            yield return null;
        }
    }
}