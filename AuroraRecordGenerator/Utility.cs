using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuroraRecordGenerator
{
	public static class Utility
	{
		/// <summary>
		///     Splits a string into an array of strings.
		/// </summary>
		/// <param name="source">The string to split.</param>
		/// <param name="predictedSplitLen">How large each string probably will be. Specify to theoretically improve performance.</param>
		/// <param name="splitValues">Array of chars to split on.</param>
		/// <returns></returns>
		public static IEnumerable<string> LazySplit(this string source, int predictedSplitLen, params char[] splitValues)
		{
			var builder = predictedSplitLen <= 0
				? new StringBuilder()
				: new StringBuilder(predictedSplitLen);
			foreach (var c in source)
			{
				if (splitValues.Contains(c))
				{
					// split off string
					var result = builder.ToString();
					builder.Clear();
					yield return result;
				}

				builder.Append(c);
			}
		}

		/// <summary>
		///     Splits a string into an array of strings.
		/// </summary>
		/// <param name="source">The string to split.</param>
		/// <param name="splitValues">Array of chars to split on.</param>
		/// <returns></returns>
		public static IEnumerable<string> LazySplit(this string source, params char[] splitValues) =>
			source.LazySplit(-1, splitValues);

		/// <summary>
		///     Splits a string into an array of strings.
		/// </summary>
		/// <param name="source">The string to split.</param>
		/// <param name="predictedSplitLen">How large each string probably will be. Specify to theoretically improve performance.</param>
		/// <param name="splitValue">Char to split on.</param>
		/// <returns></returns>
		public static IEnumerable<string> LazySplit(this string source, int predictedSplitLen, char splitValue)
		{
			var builder = predictedSplitLen <= 0
				? new StringBuilder()
				: new StringBuilder(predictedSplitLen);
			foreach (var c in source)
			{
				if (c == splitValue)
				{
					// split off string
					var result = builder.ToString();
					builder.Clear();
					yield return result;
				}

				builder.Append(c);
			}
		}

		public static IList<string> LineSplit(this string source) => 
			source.Split('\n').Where(item => item.Trim().Length != 0).ToList();


		public static string CmToFeet(double cm)
		{
			return "0'0\"";
		}

		public static string KgToLb(double kg) => $"{Math.Round(kg * 2.2046, 2)} lb";

		/// <summary>
		/// Returns <paramref name="val"/> and a trailing space if val is not whitespace, <see cref="string.Empty"/> otherwise.
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string SpaceIfValue(this string val) => string.IsNullOrWhiteSpace(val) ? string.Empty : " " + val + " ";

		public static string IfEmpty(this string target, string fallback) =>
			target.IsEmpty() ? fallback : target;

		public static bool IsEmpty(this string val) => string.IsNullOrWhiteSpace(val);

		public static string FormatAsList(this IEnumerable<string> target) => 
			target.Aggregate(new StringBuilder(), (b, s) => b.AppendLine($" - {s.Trim()}")).ToString();

		public static string Repeat(this string target, int repeatNum)
		{
			var builder = new StringBuilder(target.Length*repeatNum);
			for (var i = 0; i < repeatNum; i++)
				builder.Append(target);

			return builder.ToString();
		}

		/// <summary>
		///		Returns true if the specified species has gender.
		/// </summary>
		/// <param name="species"></param>
		/// <returns></returns>
		public static bool HasGender(this SpeciesType species) =>
			!(species == SpeciesType.Diona || species == SpeciesType.IPC || species == SpeciesType.Vaurca);

		public static string SubspeciesNiceName(SpeciesSubType species)
		{
			switch (species)
			{
				case SpeciesSubType.MsaiTajara:
					return "M'sai";
				case SpeciesSubType.ZhanTajara:
					return "Zhan-Khazan";
				case SpeciesSubType.VaurcaWorker:
					return "Worker (Type A)";
				case SpeciesSubType.VaurcaBreeder:
					return "Warrior (Type B)";
				case SpeciesSubType.IpcShell:
					return "Shell Frame";
				case SpeciesSubType.IpcG1Industrial:
					return "Industrial Frame";
				default:
					return Enum.GetName(typeof(SpeciesSubType), species);
			}
		}
	}
}
