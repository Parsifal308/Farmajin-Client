using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.Game
{
    public abstract class BaseTab : MonoBehaviour
    {
        [SerializeField] protected Image backgroundImage;
        protected Animator Animator { get; set; }

        public virtual void Open()
        {
            GetComponent<Animator>().SetTrigger("Show");
        }

        public virtual void Close()
        {
            GetComponent<Animator>().SetTrigger("Hide");
        }
    }
}