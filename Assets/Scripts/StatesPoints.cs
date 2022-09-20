using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [Serializable]
    public class Points
    {
        public Transform Transform;
        public bool isLast;
    }
public class StatesPoints : MonoBehaviour
{
        [SerializeField] private List<Transform> _listTransform;
        List<Points> _listStates = new List<Points>();



        private void Start()
        {

            for (int i = 0; i < _listTransform.Count; i++)
            {
            Points point = new Points { Transform = _listTransform[i], isLast = false };
                if (i == _listTransform.Count - 1)
                {
                    point.isLast = true;
                }
                _listStates.Add(point);
            }
        }

        public Points GetState(int index)
        {

        Points returned = null;
            if (index < _listStates.Count)
            {
                returned = _listStates[index];
            }

            return returned;
        }
      

 }

