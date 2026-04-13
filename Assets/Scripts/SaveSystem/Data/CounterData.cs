using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CounterData
{
        public string currentCustomerID;
        public List<string> metCustomersID = new List<string>();
        public List<string> visitedTodayID = new List<string>();
        public int dialogueStageInt;
        public string givenBookID;
        public bool triggerGiveBook;
}
