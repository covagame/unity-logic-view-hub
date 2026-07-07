using System;

namespace Covagame.LVH.Common
{
    /// <summary>
    /// IView BindするPropertyのインターフェース
    /// Generics のBind制約用に定義
    /// </summary>
    /// <remarks>具体的なParameter(Property)は実装先で異なるのでこのInterfaceではIDisposable派生以外は制約を課さない</remarks>
    public interface IHubProperty : IDisposable
    {
    
    }
}
