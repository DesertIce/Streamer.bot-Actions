using System;
using System.Linq;

public class CPHInline
{
	public bool Execute()
	{		
		string ConvertTempAndFormat(char degreeType, decimal degrees)	
			=> degreeType switch
			{
					'F' => $"{FahrenheitToCelsius(degrees)}°C",
					'C' => $"{CelsiusToFahrenheit(degrees)}°F",
					_ => string.Empty
			};
		
		
		var regexMatchValue = args.ContainsKey("match[0]") ? args["match[0]"]?.ToString()?.ToUpper()?.Replace("°", string.Empty) : string.Empty;
		if(!string.IsNullOrWhiteSpace(regexMatchValue))
		{
			var degreeInput = regexMatchValue.Last();
			var numericString = regexMatchValue.TrimEnd('C').TrimEnd('F').TrimEnd();
			if(decimal.TryParse(numericString, out var degreeDouble))
			{
				var convertedOutput = ConvertTempAndFormat(degreeInput, degreeDouble);
				if(!string.IsNullOrWhiteSpace(convertedOutput))
					CPH.SendAction($"{regexMatchValue} is {convertedOutput}");
				else
					CPH.LogWarn("Something failed in the conversion");
			}
			else
				CPH.LogWarn($"Parsing of {numericString} failed");
		}
		else
			CPH.LogWarn("Command triggered, but no match argument");

		return true;
	}

	private decimal FahrenheitToCelsius(decimal fahrenheit)
		=> Math.Round((fahrenheit - 32) * 5/9, 1);

	private decimal CelsiusToFahrenheit(decimal celsius)
		=> Math.Round((celsius * 9) / 5 + 32, 1);
}