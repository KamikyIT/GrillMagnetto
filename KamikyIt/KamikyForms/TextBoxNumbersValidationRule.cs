using System.Globalization;
using System.Windows.Controls;

namespace KamikyForms
{
	public class TextBoxNumbersValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var text = (string) value;

			int res;
			if (!int.TryParse(text, out res))
				return new ValidationResult(false, "Поле только для цифр.");

			return ValidationResult.ValidResult;
		}
	}
}