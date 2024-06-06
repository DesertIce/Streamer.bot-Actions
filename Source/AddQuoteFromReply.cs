public class CPHInline
{
	public bool Execute()
	{
		if(CPH.TryGetArg<string>("reply.userId", out var quoteUserId) && 
			CPH.TryGetArg<string>("reply.msgBody", out var quoteMsgBody))
		{
			quoteMsgBody = quoteMsgBody.Replace("\\s", " ");
			var quoteId = CPH.AddQuoteForTwitch(quoteUserId, quoteMsgBody, true);
			if(quoteId > 0)
				CPH.SendAction($"Quote added as #{quoteId}");
			else
				CPH.LogError($"Quote {quoteMsgBody} failed to be added");
			
			return true;
		}
		
		CPH.LogWarn("Arguments for reply quote not found");
		
		return false;
	}
}