using System;
using System.IO;
using System.Linq;
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
				var saveFilePath = string.Empty;
				if(CPH.TryGetArg<string>("fullPath", out saveFilePath)){}
				else if(CPH.TryGetArg<string>("saveFolderPath", out var saveFolderPath))
				{
					var directory = new DirectoryInfo(saveFolderPath);
					var fileInfo = directory.GetFiles("*.es3").OrderByDescending(f => f?.LastWriteTime ?? DateTime.MinValue).FirstOrDefault();
					saveFilePath = fileInfo?.FullName;
				}
				
				if(string.IsNullOrWhiteSpace(saveFilePath))
				{
					CPH.LogError("No valid file path could be determined for Super Market Sim");
					return false;
				}
				
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
		return true;
	}
}