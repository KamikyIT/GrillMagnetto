using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace KamikyForms
{
	public static class XamlExtensionHelper
	{
		public static string NumbersOnlyText(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			if (text.All(x => x == '0' || x == '1' || x == '2' || x == '3' || x == '4' || x == '5' || x == '6' || x == '7' || x == '8' || x == '9'))
				return text;

			var sb = new StringBuilder();

			foreach (var x in text)
				if (x == '0' || x == '1' || x == '2' || x == '3' || x == '4' || x == '5' || x == '6' || x == '7' || x == '8' || x == '9')
					sb.Append(x);

			return sb.ToString();
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
				{
					if (rawChild is DependencyObject)
					{
						DependencyObject child = (DependencyObject)rawChild;
						if (child is T)
						{
							yield return (T)child;
						}

						foreach (T childOfChild in FindLogicalChildren<T>(child))
						{
							yield return childOfChild;
						}
					}
				}
			}
		}

		public static bool CheckStyleName(ResourceDictionary resources, Style style, string styleName)
		{
			foreach (var key in resources.Keys)
				if (resources[key] == style)
					return key.ToString() == styleName;

			return false;
		}
	}
}