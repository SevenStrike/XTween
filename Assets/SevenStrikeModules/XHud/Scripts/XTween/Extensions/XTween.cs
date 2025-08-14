namespace SevenStrikeModules.XTween
{
    /// <summary>
    /// 用于获取当前值的委托
    /// 此委托定义了一个方法，用于从目标对象中读取当前值
    /// </summary>
    /// <typeparam name="T">委托返回值的类型</typeparam>
    /// <returns>当前值</returns>
    public delegate T XTween_Getter<T>();
    /// <summary>
    /// 用于设置目标值的委托
    /// 委托定义了一个方法，用于将目标值应用到目标对象上
    /// </summary>
    /// <typeparam name="T">委托参数的类型</typeparam>
    /// <param name="value">要设置的目标值</param>
    public delegate void XTween_Setter<T>(T value);

    public static partial class XTween
    {

    }
}