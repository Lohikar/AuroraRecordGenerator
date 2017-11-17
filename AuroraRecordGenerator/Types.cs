using ProtoBuf;
using System;

namespace AuroraRecordGenerator
{
	[ProtoContract]
	public enum SpeciesType
	{
		[ProtoEnum]
		None = 0,

		[ProtoEnum]
		Human,

		[ProtoEnum]
		Skrell,

		[ProtoEnum]
		Tajara,

		[ProtoEnum]
		Unathi,

		[ProtoEnum]
		Vaurca,

		[ProtoEnum]
		Diona,

		[ProtoEnum]
		IPC
	}

	[ProtoContract]
	public enum SpeciesSubType
	{
		[ProtoEnum]
		None = 0,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Tajara, "M'sai")]
		MsaiTajara,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Tajara, "Zhan-Khazan")]
		ZhanTajara,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Vaurca, "Type A (Worker)")]
		VaurcaWorker,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Vaurca, "Type B (Warrior)")]
		VaurcaWarrior,

		[ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Shell Frame")]
		IpcShell,

		[ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Industrial Frame")]
		IpcG1Industrial
	}

	[ProtoContract]
	public enum GenderType
	{
		[ProtoEnum]
		NotApplicable = 0,

		[ProtoEnum]
		Male,

		[ProtoEnum]
		Female
	}

	public static class Info
	{
		/// <summary>
		/// The current in-character date.
		/// </summary>
		public static DateTime IcDate => new DateTime(DateTime.Now.Year + 442,
			DateTime.Now.Month,
			DateTime.Now.Day);
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class SubspeciesMetaAttribute : Attribute
	{
		public SpeciesType AssociatedSpecies {get; private set;}
		public string NiceName { get; private set; }
		public SubspeciesMetaAttribute(SpeciesType associatedType, string nicename)
		{
			AssociatedSpecies = associatedType;
			NiceName = nicename;
		}
	}
}