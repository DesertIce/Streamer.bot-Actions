using System;

public class CPHInline
{
	public bool Execute()
	{
		var repeatCount = args.ContainsKey("repeatCount") ? int.Parse(args["repeatCount"]?.ToString()) : 0;
		var actionName = args.ContainsKey("actionName") ? args["actionName"]?.ToString() : string.Empty;
		if(string.IsNullOrWhiteSpace(actionName) || repeatCount < 2)
			return false;
		
		for(var i = 0; i < repeatCount; ++i)
			CPH.RunAction(actionName, true);
		return true;
	}
}