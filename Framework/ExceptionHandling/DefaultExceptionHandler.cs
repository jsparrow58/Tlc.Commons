using System;

namespace SJ.ExceptionHandling
{
    /// <summary>
    ///   捕获所有异常，简单的使用日志记录
    /// </summary>
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public void HandleError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}