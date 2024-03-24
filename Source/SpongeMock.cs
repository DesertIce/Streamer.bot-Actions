using System;
using System.Linq;
using System.Text;
public class CPHInline
{
	public bool Execute()
	{
		if(CPH.TryGetArg<string>("reply.msgId", out var replymsgId))
			CPH.TwitchDeleteChatMessage(replymsgId);
			
		if(CPH.TryGetArg<string>("reply.msgBody", out var replymsgBody) && !string.IsNullOrWhiteSpace(replymsgBody))
		{	
			replymsgBody = replymsgBody.Replace("\\s", " ");
			replymsgBody = string.Join("",  replymsgBody.Select(c => {
				if(char.IsLetter(c))
				{
					if(CPH.Between(0, 100) > 50)
						return char.ToUpper(c);
					else
						return char.ToLower(c);
				}
				return c;
			}));
			
			CPH.SendAction($"\"{replymsgBody}\"");
		}
		return true;
	}
}