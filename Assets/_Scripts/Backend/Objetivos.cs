using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivos : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string objetivos =
        "Objetivo 1: Loadear cosas desde json que ya tenemos en disco\n" +
        "Objetivo 2: Pedir ese json al endpoint correspondiente";
}
