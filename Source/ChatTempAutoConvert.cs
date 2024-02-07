using System;
using System.Linq;

public class CPHInline
{
	public bool Execute()
	{		
		string ConvertTempAndFormat(char degreeType, double degrees)	
			=> degreeType switch
			{
					'F' => $"{FahrenheitToCelsius(degrees)}C",
					'C' => $"{CelsiusToFahrenheit(degrees)}F",
					_ => string.Empty
			};
		
		
		var regexMatchValue = args.ContainsKey("match[0]") ? args["match[0]"]?.ToString()?.ToUpper() : string.Empty;
		if(!string.IsNullOrWhiteSpace(regexMatchValue))
		{
			var degreeInput = regexMatchValue.ToUpper().Last();
			var numericString = regexMatchValue.TrimEnd('C').TrimEnd('F').TrimEnd();
			if(double.TryParse(numericString, out var degreeDouble))
			{
				var convertedOutput = ConvertTempAndFormat(degreeInput, degreeDouble);
				if(!string.IsNullOrWhiteSpace(convertedOutput))
					CPH.SendAction(convertedOutput);
				else
					CPH.LogWarn("Something failed in the conversion");
			}
			else
				CPH.LogWarn($"Parsing of {numericString} to double failed");
		}
		else
			CPH.LogWarn("Command triggered, but no match argument");

		return true;
	}

	private double FahrenheitToCelsius(double fahrenheit)
		=> Math.Round((fahrenheit - 32) * 5/9, 1);

	private double CelsiusToFahrenheit(double celsius)
		=> Math.Round((celsius * 9) / 5 + 32, 1);
}