using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic;  // Importante para usar List<>
using Unity.VisualScripting;
using Newtonsoft.Json;
using Farmanji.Auth;

using Farmanji.Data;

public class GetActivitiesQuiz : MonoBehaviour
{
    //public InputField outputArea;
    [SerializeField] private Text tesText;


    private const string URL = "https://ux72a80ug6.execute-api.us-east-1.amazonaws.com/dev//app-get-activities?worldId=6400d7c615d3afb7eeda5083&activityType=quiz&levelId=64d3e5a0d30e175469d08c5a";
    private const string ContentType = "application/json";
    private static readonly string Authorization = SessionManager.Instance.UserData.Token;

    //public List<Question> AllQuestions = new List<Question>();
    //public List<Dictionary<string, object>> TestsList = new List<Dictionary<string, object>>();
    public JSONArray testArray = new JSONArray();

    public TestListQuiz testListQuiz;

    public void LoadData()
    {
        Dictionary<string, object> testsList = new Dictionary<string, object>(); //QuestionsSingleton.Instance.TestsList;
        //Debug.Log("Ya cargue los datos");
        //Debug.Log("Tipo de dato: " + testsList.GetType());
        List<Question> AllQuestions = new List<Question>();
        foreach (Questions item in Farmanji.Managers.ResourcesLoaderManager.Instance.Worlds.CurrentActivity.Questions)
        {
            string questionText = item.question;
            //Debug.Log("Pregunta: "+questionText);
            Responses[] responses = item.responses;

            Question newQuestion = new Question();
            newQuestion.questionInfo = questionText;
            newQuestion.options = new List<string>();
            newQuestion.questionImg = null;
            newQuestion.questionClip = null;
            newQuestion.questionVideo = null;
            newQuestion.questionType = QuestionType.TEXT;
            foreach (Responses answer in responses)
            {
                string option = answer.response;
                //Debug.Log ("OPTION: "+option );
                newQuestion.options.Add(option);

                if (answer.isCorrect)
                {
                    newQuestion.correctAns = option;
                }
            }
            AllQuestions.Add(newQuestion);
            //Questions.Add(newQuestion);
            //quizManager.OnPlayButtonClicked(newQuestion);
        }
        //Dictionary<string, object> Tests = new Dictionary<string, object>();
        //testsList.Add("Test", activityElement.name);
        testsList.Add("Questions", AllQuestions);

        //QuestionsSingleton.Instance.TestsList = Tests;
        //TestsList.Add(Tests);
        //testListQuiz.CreateQuestion(AllQuestions);
        //testListQuiz.CreateQuestion(TestsList[0]);



        testListQuiz.CreateQuestion(testsList);
        //StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("Content-Type", ContentType);
            request.SetRequestHeader("Authorization", Authorization);

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                //Debug.Log("Received: " + request.downloadHandler.text);
                JSONNode itemsData = JSON.Parse(request.downloadHandler.text);
                JSONArray dataArray = itemsData["data"].AsArray;
                foreach (JSONNode Test in dataArray)
                {

                    List<Question> AllQuestions = new List<Question>();
                    JSONArray questionsArray = new JSONArray();

                    if (dataArray.Count > 0)
                    {
                        
                        string Names = Test["name"];
                        
                        JSONArray questions = Test["questions"].AsArray;
                        foreach (JSONNode item in questions)
                        {
                            string questionText = item["question"];
                            JSONArray responses = item["responses"].AsArray;

                            Question newQuestion = new Question();
                            newQuestion.questionInfo = questionText;
                            newQuestion.options = new List<string>();
                            newQuestion.questionImg = null;
                            newQuestion.questionClip = null;
                            newQuestion.questionVideo = null;
                            newQuestion.questionType = QuestionType.TEXT;
                            foreach (JSONNode response in responses)
                            {
                                string option = response["response"];
                                newQuestion.options.Add(option);

                                if (response["isCorrect"].AsBool)
                                {
                                    newQuestion.correctAns = option;
                                }
                            }

                            AllQuestions.Add(newQuestion);
                        }

                        /* ---------Option 2----------- */
                        Dictionary<string, object> Tests = new Dictionary<string, object>();
                        Tests.Add("Test", Names);
                        Tests.Add("Questions", AllQuestions);


                        //TestsList.Add(Tests);
                    }
                    else
                    {
                        Debug.Log("No data found in the response.");
                    }
                }

                // Convertir el objeto a formato JSON
/*                 string json2 = JsonConvert.SerializeObject(TestsList.Count, Formatting.Indented);
                Debug.Log("JSON2:\n" + json2);
                Debug.Log("JSON2/tamaño:\n" + json2); */

                List<string> NamesTest = new List<string>();
                //foreach (Dictionary<string, object> Tests in TestsList)
                {
                    //string test = Tests["Test"] as string;
                    //NamesTest.Add(test.ToString());
                }
               //testList.CreateTestCopies( TestsList);
               //testListQuiz.CreateQuestion(TestsList[0]);
            }
        }
    }
}
