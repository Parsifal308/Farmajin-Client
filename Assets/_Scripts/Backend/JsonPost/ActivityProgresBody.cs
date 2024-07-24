using System.Collections;
using System.Collections.Generic;
using Farmanji.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Farmanji.Data
{
    public class ActivityProgresBody : PostBody
    {
        public string worldId;
        public string levelId;
        public string stageId;
        public string activityId;
        public string progressType;
        public bool completed;
        public float activityPercentage;
        public static ActivityProgresBody Create(string worldId, string levelId, string stageId, string activityId, bool completed, string progresType,float activityPercentage)
        {
            var activityResponse = new ActivityProgresBody();
            activityResponse.worldId = worldId;
            activityResponse.levelId = levelId;
            activityResponse.stageId = stageId;
            activityResponse.activityId = activityId;
            activityResponse.completed = completed;
            activityResponse.progressType = progresType;
            activityResponse.activityPercentage = activityPercentage;
            return activityResponse;
        }
    }
}