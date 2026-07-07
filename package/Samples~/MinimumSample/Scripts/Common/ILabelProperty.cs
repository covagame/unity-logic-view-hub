using System; 
using UnityEngine;
using R3;
using Covagame.LVH.Common;

namespace Covagame.LVH.Sample.Common
{

    public interface ILabelProperty : IHubProperty
    {
       ReactiveProperty<string> Label { get; }
    }

}