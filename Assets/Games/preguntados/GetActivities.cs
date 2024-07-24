using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic;  // Importante para usar List<>
using Unity.VisualScripting;
using Newtonsoft.Json;
using Farmanji.Auth;

public class GetActivities : MonoBehaviour
{
    //public InputField outputArea;
    [SerializeField] private Text tesText;


    private const string URL = "https://ux72a80ug6.execute-api.us-east-1.amazonaws.com/dev//app-get-activities?worldId=64f276a18a2f9e0bcc31e46a&activityType=quiz&levelId=64f276bc6ceaf0b98a6b3fdf";
    private const string ContentType = "application/json";
    private static readonly string Authorization = SessionManager.Instance.UserData.Token;
    //public List<Question> AllQuestions = new List<Question>();
    public List<Dictionary<string, object>> TestsList = new List<Dictionary<string, object>>();
    public JSONArray testArray = new JSONArray();

    public TestList testList;
    public TestListQuiz testListQuiz;

    public void LoadData()
    {
        StartCoroutine(ProcessRequest(URL));
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
                Debug.Log("Received: " + request.downloadHandler.text);
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


                        TestsList.Add(Tests);
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
                foreach (Dictionary<string, object> Tests in TestsList)
                {
                    string test = Tests["Test"] as string;
                    NamesTest.Add(test.ToString());
                }
               testList.CreateTestCopies( TestsList);
            }
        }
    }
}
