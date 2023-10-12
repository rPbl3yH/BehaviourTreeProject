﻿using Sample;
using UnityEngine;

namespace AI.Sensors
{
    public class ResourceBagCapacitySensor : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private void Update()
        {
            if (_character.IsResourceBagFull())
            {
                Debug.Log("Resource bag is full");
            }
        }
    }
}