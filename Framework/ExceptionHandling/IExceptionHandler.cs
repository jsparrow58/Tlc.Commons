using System;

namespace SJ.ExceptionHandling
{
    /// <summary>
    ///   异常处理
    /// </summary>
    public interface IExceptionHandler
    {
        void HandleError(Exception exception);
    }
}