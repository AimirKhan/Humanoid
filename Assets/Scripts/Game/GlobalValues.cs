using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Humanoid
{
    public class GlobalValues : IGlobalValues
    {
        private ReactiveProperty<float> gravityValue = new(-9.8f);
        public ReactiveProperty<float> GravityValue => gravityValue;
    }

    interface IGlobalValues
    {
        ReactiveProperty<float> GravityValue { get; }
    }
}