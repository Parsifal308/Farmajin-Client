using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExampleJsonLoadHandler : MonoBehaviour
{

    [SerializeField] private QuizInfo quizInfoDebug;
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private RectTransform questionParent;
    private string loginEndpoint = "AJJAj";

    //SPRITE VACIO PARA IR USANDO COMO HELPER TEMPORAL DE LOOS SPRITES QUE NOS TRAEMOS DE LA WEB
    Sprite sprite = null;

    void Start()
    {
        StartCoroutine(LoadJson());
    }


    private IEnumerator LoadJson()
    {
        yield return null;
        var jsonSource = File.ReadAllText(Application.streamingAssetsPath + "/Questions.json");
        Debug.Log(jsonSource);
        QuizInfo quizInfo = JsonUtility.FromJson<QuizInfo>(jsonSource);
        quizInfoDebug = quizInfo;

        foreach (var exampleLoad in quizInfo.quizInfo)
        {
            var question = Instantiate(questionPrefab, questionParent);
            question.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionInfo;


            //COSAS IMPORTANTES PARA MULTIMEDIA
            //Acá tenemos un path a un archivo de tipo img. Entonces, iniciamos una corutina, nos descargamos esta img
            //Y se la asignamos a un componente de tipo Image
            question.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionImg;
            //lo mismo de arriba pero con un componente de tipo Audio
            question.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionClip;
            //lo mismo de arriba pero con un componente de tipo VideoPlayer
            question.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionVideo;

            question.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.correctAns;
            question.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[0];
            question.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[1];
            question.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[2];
            question.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[3];
            question.transform.GetChild(9).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionType.ToString();
        }

        //ExampleLoad exampleLoad = JsonUtility.FromJson<ExampleLoad>(jsonSource);

    }

    private void AssignQuestions()
    {

    }

    private IEnumerator LoadJsonFromWeb()
    {

        UnityWebRequest request = new UnityWebRequest(loginEndpoint, "GET");
        request.SetRequestHeader("Content-Type", "application/json");
        //ESTO SÓLO SI HAY UNA VALIDACIÓN DE USER QUE PUEDA DESCARGAR RECURSO
        //PODES METER FIELDS A LA REQUEST QUE EL ENDPOINT VA A ESTAR ESPERANDO RECIBIR PARA VALIDAR
        // byte[] bodyRaw = Encoding.UTF8.GetBytes(
        //     "{ \"email\": \"" + email +
        //     "{ \"token\": \"" + token +
        //     "{ \"apisecret\": \"" + apisecret +
        //     "\", \"password\": \"" + password + "\" }");
        //request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        var handler = request.SendWebRequest();
        yield return handler;

        float startTime = 0;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10f)
            {
                Debug.LogError("Connection to server timed out");
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            QuizInfo quizInfo = JsonUtility.FromJson<QuizInfo>(request.downloadHandler.text);
            quizInfoDebug = quizInfo;

            foreach (var exampleLoad in quizInfo.quizInfo)
            {
                var question = Instantiate(questionPrefab, questionParent);
                question.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionInfo;

                //COSAS IMPORTANTES PARA MULTIMEDIA
                //Acá tenemos un path a un archivo de tipo img. Entonces, iniciamos una corutina, nos descargamos esta img
                //Y se la asignamos a un componente de tipo Image
                question.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionImg;
                yield return StartCoroutine(LoadImage(exampleLoad.questionImg));
                question.transform.GetChild(1).GetComponent<Image>().sprite = sprite;//REPRODUCIR ESTA LÓGICA PARA TODO RECURSSOS MULTIMEDIA
                //lo mismo de arriba pero con un componente de tipo Audio
                question.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionClip;
                //lo mismo de arriba pero con un componente de tipo VideoPlayer
                question.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionVideo;

                question.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.correctAns;
                question.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[0];
                question.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[1];
                question.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[2];
                question.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.options[3];
                question.transform.GetChild(9).GetComponent<TMPro.TextMeshProUGUI>().text = exampleLoad.questionType.ToString();
            }
        }
        else
        {
            //EN CASO QUE EL ENDPOINT DEVUELVA UN ERROR, PODES HACER UN SWITCH CON LOS DISTINTOS CODIGOS DE ERROR
            // switch (response.code)
            // {
            //     case 1:
            //         alertText.text = "There's no account with that email";
            //         ActivateButtons(true);
            //         break;
            //     case 2:
            //         alertText.text = "User is not active";
            //         ActivateButtons(true);
            //         break;
            //     case 3:
            //         alertText.text = "Invalid password";
            //         ActivateButtons(true);
            //         break;
            //     case 4:
            //         alertText.text = "Internal server error";
            //         ActivateButtons(true);
            //         break;
            //     default:
            //         alertText.text = "Unhandled error";
            //         ActivateButtons(true);
            //         break;
            // }
            Debug.LogError("Error: " + request.error);
        }
    }

    private IEnumerator LoadImage(string questionImg)
    {
        //TODO: Validar que no tengamos este recurso.
        //Si lo tenemos, no lo descargamos de nuevo
        //En lugar de eso lo obtenemos de la carpeta de recursos
        //Y lo asignamos al componente de tipo Image
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(questionImg);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
