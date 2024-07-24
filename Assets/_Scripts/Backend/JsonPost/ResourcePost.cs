using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Data
{
    public abstract class ResourcePost : MonoBehaviour
    {
        [SerializeField] protected JsonPost _jsonPost;

        public JsonPost JsonPost { get { return _jsonPost; } }
        public abstract IEnumerator Post(PostBody body);

        public virtual IEnumerator CreateConcreteData()
        {
            yield return null;
        }
    }
}