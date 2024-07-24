using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestList : MonoBehaviour
{
    public GameObject testObjectPrefab;
    public RectTransform targetRectTransform;
    public RectTransform verticalLayout;
    public QuizManager quizManager;

    // Llamado para crear las copias de los objetos de prueba
    public void CreateTestCopies(List<Dictionary<string, object>> TestsList)
    {
        // Asegurarnos de que el prefab y el layout estén configurados
        if (testObjectPrefab == null || verticalLayout == null)
        {
            Debug.Log("Mensaje de depuración");
            return;
        }

        // Cambiar el tamaño del layout vertical según la cantidad de pruebas
        targetRectTransform.sizeDelta = new Vector2(targetRectTransform.sizeDelta.x, 300 * TestsList.Count);

        foreach (var test in TestsList)
        {
            // Obtener el nombre del test y las preguntas
            string testName = test["Test"] as string;
            List<Question> questions = test["Questions"] as List<Question>;

            // Crear una copia del prefab
            GameObject newTestObject = Instantiate(testObjectPrefab, verticalLayout.transform);

            // Obtener el componente Text del objeto copiado
            Text textComponent = newTestObject.GetComponentInChildren<Text>();

            // Cambiar el texto según el nombre del test
            textComponent.text = testName;

            // Obtener el componente Button del objeto copiado
            Button playButton = newTestObject.GetComponentInChildren<Button>();

            // Asignar una función al botón (cargar preguntas del test)
            if (playButton != null)
            {
                playButton.onClick.AddListener(() => LoadGame(questions));
            }
        }
    }

    // Método para cargar preguntas del test seleccionado
    public void LoadGame(List<Question> questions)
    {

        quizManager.OnPlayButtonClicked(questions);
        return;
    }
}
