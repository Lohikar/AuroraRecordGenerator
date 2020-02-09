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
		[ProtoEnum, SubspeciesMeta(SpeciesType.None, "N/A")]
		None = 0,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Tajara, "M'sai", "Ethnicity")]
		MsaiTajara,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Tajara, "Zhan-Khazan", "Ethnicity")]
		ZhanTajara,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Vaurca, "Type A (Worker)", "Classification")]
		VaurcaWorker,

		[ProtoEnum, SubspeciesMeta(SpeciesType.Vaurca, "Type B (Warrior)", "Classification")]
		VaurcaWarrior,

		[ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Shell", "Model")]
		IpcShell,

		[ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Hephaestus G1 Heavy", "Model")]
		IpcG1Industrial,

        [ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Hephaestus G2 Heavy", "Model")]
        IpcG2Industrial,

        [ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Bishop Accessory", "Model")]
        IpcFancy,

        [ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Zeng-Hu Mobility", "Model")]
        IpcMedsci,

        [ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Shell", "Model")]
        IpcHumanoid,

        [ProtoEnum, SubspeciesMeta(SpeciesType.IPC, "Xion Industrial", "Model")]
        IpcHeavy,

        [ProtoEnum, SubspeciesMeta(SpeciesType.Unathi, "Aut'akh", "Variant")]
        UnathiRobot,

        [ProtoEnum, SubspeciesMeta(SpeciesType.Human, "Offworlder", "Variant")]
        HumanOffworld
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
		public string FieldName { get; private set; }
		public SubspeciesMetaAttribute(SpeciesType associatedType, string nicename, string fieldname = "Subspecies")
		{
			AssociatedSpecies = associatedType;
			NiceName = nicename;
			FieldName = fieldname;
		}
	}
}