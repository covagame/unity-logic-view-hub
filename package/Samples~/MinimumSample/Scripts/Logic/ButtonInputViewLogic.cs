using System; 
using UnityEngine;
using R3;
using Covagame.LVH.Common;

namespace Covagame.LVH.Sample.Logic
{
    public class ButtonInputViewLogic : IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public void Dispose() => _disposable.Dispose();

        public ButtonInputViewLogic()
        {
        
        }
    }

}