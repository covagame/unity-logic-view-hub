using System;

namespace Covagame.LVH.Common
{
    /// <summary>
    /// View の共通インターフェース
    /// </summary>
    /// <remarks>public なAPIを具象クラスに生やさない! Property を介してやりとりさせる</remarks>
    /// <typeparam name="TProperty"></typeparam>
    public interface IView<TProperty> : IDisposable where TProperty : IHubProperty 
    {
        void Bind(TProperty property);
    }
}