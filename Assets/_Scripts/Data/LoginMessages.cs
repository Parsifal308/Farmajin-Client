using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Login Messages")]
public class LoginMessages : ScriptableObject
{
    [SerializeField] public List<Errors> errors = new List<Errors>();


}
[System.Serializable]
public class Errors
{
    [SerializeField] public int code;
    [SerializeField] public string type;
    [SerializeField] public string msg;
}