using System;
using ProtoBuf;

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
		[ProtoEnum]
		MsaiTajara,
		[ProtoEnum]
		ZhanTajara,
		[ProtoEnum]
		VaurcaWorker,
		[ProtoEnum]
		VaurcaWarrior,
		[ProtoEnum]
		IpcShell,
		[ProtoEnum]
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
		public static DateTime IcDate => new DateTime(2458, 
			DateTime.Now.Month,
			DateTime.Now.Day);
	}
}
