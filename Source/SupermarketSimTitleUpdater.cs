using System.IO;
using Newtonsoft.Json.Linq;
public class CPHInline
{
	public bool Execute()
	{
		if(CPH.TryGetArg<string>("targetChannelTitle", out var currentTitle))
		{
			var lastPipeIndex = currentTitle.LastIndexOf("|");
			if(lastPipeIndex > 0)
			{
				if(CPH.TryGetArg<string>("fullPath", out var saveFilePath))
				{
					currentTitle = currentTitle.Remove(lastPipeIndex, currentTitle.Length - lastPipeIndex);
					var saveFileJSON = JObject.Parse(File.ReadAllText(saveFilePath));
					var progressionData = saveFileJSON["Progression"]["value"];
					var money = progressionData.Value<decimal>("Money");
					var level = progressionData.Value<int>("CurrentStoreLevel");
					var day = progressionData.Value<int>("CurrentDay");
					currentTitle = $"{currentTitle}| Day: {day} Level: {level} Money: {money:C}";
					CPH.SetChannelTitle(currentTitle);
				}
			}
		}
		return true;
	}
}