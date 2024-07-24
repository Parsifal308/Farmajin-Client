using Farmanji.UI;
using System.Collections;
using System.Collections.Generic;
using Farmanji.Managers;
using UnityEngine;

using SimpleJSON;
using Newtonsoft.Json.Linq;

using Unity.VisualScripting;
using UnityEngine.Events;
using System;


namespace Farmanji.Data
{
    public class LevelsLoader : ResourceLoader
    {
        public List<LevelData> List;
        //public TestListQuiz testListQuiz;
        //public QuizManager quizManager;
        //public QuestionManager questionManager;
        //public List<Dictionary<string, object>> TestsList = new List<Dictionary<string, object>>();


        public override IEnumerator Load()
        {
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<LevelsCollection>());

            yield return StartCoroutine(CreateConcreteData());
        }

        public override IEnumerator CreateConcreteData()
        {
            LevelsCollection response = _jsonLoader.ResponseInfo as LevelsCollection;

            foreach (var levelElement in response.data)
            {
                yield return StartCoroutine(DownloadImage(levelElement.backgroundImg));
                Sprite backgroundImage = DownloadedSprite;

                yield return StartCoroutine(DownloadImage(levelElement.worldImage));
                Sprite platformImage = DownloadedSprite;
                List<StageData> stageList = new List<StageData>();

                float levelProgress = 0;
                foreach (var stageElement in levelElement.stages)
                {
                    List<ActivityData> activitiesList = new List<ActivityData>();

                    float stageProgress = 0;

                    foreach (var activityElement in stageElement.activities)
                    {
                        yield return StartCoroutine(DownloadImage(activityElement.coverImage));
                        Sprite activityCoverImage = DownloadedSprite;

                        /*if (activityElement.questions.Length > 0)
                        {

                            List<Question> AllQuestions = new List<Question>();
                            foreach(Questions item in activityElement.questions)
                            {
                                string questionText = item.question;
                                //Debug.Log("Pregunta: "+questionText);
                                Responses []responses = item.responses;

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
                            Dictionary<string, object> Tests = new Dictionary<string, object>();
                            Tests.Add("Test", activityElement.name);
                            Tests.Add("Questions", AllQuestions);

                            QuestionsSingleton.Instance.TestsList = Tests;
                            //TestsList.Add(Tests);
                            //testListQuiz.CreateQuestion(AllQuestions);
                            //testListQuiz.CreateQuestion(TestsList[0]);

                        }*/
                        float activityWorldProgress = 100 / stageElement.activities.Length;
                        if (activityElement.completed) stageProgress += activityWorldProgress;

                        activitiesList.Add(new ActivityData(activityElement, activityCoverImage, activityWorldProgress, levelElement._id, stageElement._id));
                    }
                    stageList.Add(new StageData(stageElement, activitiesList, stageProgress));
                    levelProgress += stageProgress;
                }
                List.Add(new LevelData(levelElement._id, levelElement.name, levelElement.worldId, backgroundImage, stageList, levelProgress, levelElement.completed));
                //Agreegar al dictionary
            }

            yield return null;
        }


        public IEnumerator LoadLevelsAtWorld(string worldId)
        {
            List.Clear();
            //string queryParams = "?page=1,perPage=500,worldId="+ worldId;
            string queryParams = "?page=1&perPage=500&worldId=" + worldId;
            yield return StartCoroutine(_jsonLoader.LoadFromWeb<LevelsCollection>(queryParams, "data"));

            yield return StartCoroutine(CreateConcreteData());
        }
    }
}
