using System;
using System.Linq;

public class CPHInline
{
	public bool Execute()
	{
		if(CPH.TryGetArg<string>("match[0]", out var value) && !string.IsNullOrWhiteSpace(value))
		{
			var numberString = string.Concat(value.Where(v => char.IsDigit(v) || char.IsPunctuation(v)));
			var suffix = string.Concat(value.Where(char.IsLetter)).ToLower();
			
			if(decimal.TryParse(numberString, out var number))
			{
				if(suffix.StartsWith("mi"))
					CPH.SendAction($"{value} is {MilesToKilometers(number)} km");
				else if(suffix.StartsWith("km") || suffix.StartsWith("kilometer"))
					CPH.SendAction($"{value} is {KilometersToMiles(number)} miles");
				else
					CPH.LogWarn($"Failed to identify distance suffix");
			}
			else
				CPH.LogWarn($"Failed to parse {numberString} into a number");
		}
		else
			CPH.LogWarn("Command triggered, but no match");
		
		return true;
	}
	
	private decimal KilometersToMiles(decimal km)
		=> Math.Round(km * 0.6214m, 1);
		
	private decimal MilesToKilometers(decimal mi)
		=> Math.Round(mi * 1.609m, 1);
}