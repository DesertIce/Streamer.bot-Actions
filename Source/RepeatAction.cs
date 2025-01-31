using System;

public class CPHInline
{
    public bool Execute()
    {
        if (!CPH.TryGetArg<int>("repeatCount", out var repeatCount) || repeatCount < 2)
        {
            CPH.LogWarn("repeatCount must be at least 2");
            return false;
        }

        if (!CPH.TryGetArg<string>("actionName", out var actionName) || string.IsNullOrWhiteSpace(actionName))
        {
            CPH.LogWarn("actionName must be specified");
            return false;
        }

        for (var i = 0; i < repeatCount; ++i)
        {
            CPH.RunAction(actionName, true);
        }

        return true;
    }
}