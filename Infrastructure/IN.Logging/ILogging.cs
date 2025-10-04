
// Assembly location: C:\Users\imranq\.nuget\packages\nova.interfaces\1.1.7\lib\netstandard1.5\Nova.Interfaces.dll

using System;

namespace Logging
{
    public interface ILogging
    {
        void Log(Exception exception);
        void Log(string message);
        void Error(Exception exception);
        void Error(string message);
        void Fatal(Exception exception);
        void Fatal(string message);
        void Info(Exception exception);
        void Info(string message);
    }
}