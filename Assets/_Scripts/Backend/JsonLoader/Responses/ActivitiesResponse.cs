using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


namespace Farmanji.Data
{
    [System.Serializable]
    public class ActivitiesResponse
    {
        public string _id;
        public string name;
        public string description;
        public string videoUrl;
        public string coverImage;
        public string rewardType;
        public int reward;
        public string question;

        public string[] responses;
        //public Question q = new Question();
        //public JSONArray questions;
        public Questions[] questions;
        public string activityType;
        public string stageId;
        public string levelId;
        public string worldId;
        public string gameType;
        public bool coins;
        public bool gems;
        public int gemsReward;
        public int coinsReward;
        public bool disabled;
        public bool usersWithProgress;
        public string createdAt;
        public string updatedAt;
        public string deletedAt;
        public bool completed;
        public AssociatedQuiz associatedQuiz;
    }

    [System.Serializable]
    public class Questions
    {
        public string id;
        public string question;
        public Responses[] responses;
    }

    [System.Serializable]
    public class Responses
    {
        public string id;
        public string response;
        public bool isCorrect;
    }


    [System.Serializable]
    public class AssociatedQuiz
    {
        public Questions[] questions;
    }
}


