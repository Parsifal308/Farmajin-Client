using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Farmanji.Managers;
using UnityEngine.UI;
using TMPro;

public class Lista_Tests : MonoBehaviour
{
    //Variables para la creacion de la lista de tests.
    public GameObject testObjectPrefab; // El prefab del objeto de prueba (con texto y botón)
    public VerticalLayoutGroup verticalLayout; // El VerticalLayoutGroup donde se crearán las copias
    private int numCopies = 6; // Número de copias que deseas crear
    public string[] words = { "futbol", "basquet", "tenis", "golf", "ajedrez", "poker" }; // Arreglo de palabras

    //Cambiar el tamaño para el scroll view
    public RectTransform targetRectTransform; // Referencia al RectTransform del objeto que deseas cambiar
    public float newHeight = 300.0f; // La nueva altura que deseas aplicar

    // Start is called before the first frame update
    public void Start()
    {
        //cambiar el tamaño del vetical layout para que valga el 
        targetRectTransform.sizeDelta = new Vector2(targetRectTransform.sizeDelta.x, newHeight*6); 
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    //Funcion para crear las vistas de los test
    public void CreateTestCopies()
    {
        for (int i = 1; i <= numCopies; i++)
        {
            // Crear una copia del prefab
            GameObject newTestObject = Instantiate(testObjectPrefab, verticalLayout.transform);
            
            // Obtener el componente Text del objeto copiado
            Text textComponent = newTestObject.GetComponentInChildren<Text>();
            
            // Cambiar el texto según la palabra en el arreglo
            if (textComponent != null && i <= words.Length)
            {
                textComponent.text = words[i-1];
            }
            // Obtener el componente Button del objeto copiado
            Button playButton = newTestObject.GetComponentInChildren<Button>();
            // Asignar una función al botón (por ejemplo, una función de clic)
            if (playButton != null)
            {
                int copyNumber = i; // Almacenar el número de copia en una variable local
                playButton.onClick.AddListener(() => PlayButtonClick(copyNumber)); // Asignar función de clic
            }
        }
    }

    // Función que se llama cuando se hace clic en un botón "Play"
    public void PlayButtonClick(int copyNumber)
    {
        Debug.Log("Botón Play de la copia " + copyNumber + " presionado.");
        // Aquí puedes agregar el comportamiento que desees para el botón Play de esta copia
    }
}
