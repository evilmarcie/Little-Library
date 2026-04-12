using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CounterData
{
        public Character currentCustomer;
        public List<Character> metCustomers = new List<Character>();
        public List<Character> visitedToday = new List<Character>();
        public int dialogueStageInt;
        public string givenBookID;
        public bool triggerGiveBook;
}
