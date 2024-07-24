using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TestListQuiz : MonoBehaviour
{
    public GameObject testObjectPrefab;
    public RectTransform targetRectTransform;
    public RectTransform verticalLayout;
    public QuizManager quizManager;

    public void CreateQuestion(Dictionary<string, object> TestsList)
    {
        List<Question> questions = TestsList["Questions"] as List<Question>;
        //Debug.Log("Question: " + questions);


        LoadGame(questions);

    }

    // Método para cargar preguntas del test seleccionado
    public void LoadGame(List<Question> questions)
    {
        //List<Question> q = test["Questions"] as List<Question>;
        quizManager.OnPlayButtonClicked(questions);
        return;
    }
}
