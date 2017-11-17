using System.Linq;
using System.Text;
using Humanizer;

namespace AuroraRecordGenerator
{
	internal partial class RecordFormatter
	{
		private void MakeCommonRecords()
		{
			var record = new StringBuilder();
			record.AppendLine("/// PUBLIC RECORD ///");
			record.AppendLine(MakeNameLine());
			record.AppendLine($"Date of Birth: {_targetRecord.BirthDate.ToString("MMMM")} {_targetRecord.BirthDate.Day.Ordinalize()}, {_targetRecord.BirthDate.Year}");
			record.AppendLine($"Species: {_targetRecord.Species.Humanize()}");// might fuck up the names
			record.AppendLine(_targetRecord.Species.HasGender()
				? $"Gender: {_targetRecord.Gender.Humanize()}"
				: "Gender: Not Applicable.");
			record.AppendLine($"Citizenship: {_targetRecord.Citizenship.IfEmpty("None.")}");
			record.AppendLine($"Clearance Level: {_targetRecord.Clearance.IfEmpty("Not Specified")}");
			record.AppendLine($"Employed As: {_targetRecord.EmployedAs.IfEmpty("Assistant")}");
			if (_targetRecord.CharHeight != null)
				record.AppendLine($"Height: {_targetRecord.CharHeight} cm ({Utility.CmToFeet(_targetRecord.CharHeight.Value)})");

			if (_targetRecord.Weight != null)
				record.AppendLine($"Weight: {_targetRecord.Weight} kg ({Utility.KgToLb(_targetRecord.Weight ?? 0)})");

			record.AppendLine();

			// identifying features
			// TODO: identifying features

			// general notes
			WriteSectionIfAny(ref record,
				"General Notes:",
				_employmentPublicRecord);

			WriteSectionIfAny(ref record,
				"Medical Notes:",
				_medicalPublicRecord);

			WriteSectionIfAny(ref record,
				"Security Notes:",
				_securityPublicRecord);

			_commonRecords = record.ToString();
		}

		private void MakeEmploymentRecords()
		{
			var recordText = new StringBuilder();
			if (_commonRecords.IsEmpty())
				MakeCommonRecords();

			recordText.Append(_commonRecords);

			if (!_employmentExperience.Any() &&
				!_employmentFormalEducation.Any() &&
				!_employmentNtEmployment.Any() &&
				!_employmentPreNtEmployment.Any() &&
				!_employmentPublicRecord.Any() &&
				!_employmentSkills.Any())
			{
				recordText.AppendLine("/// NO EMPLOYMENT RECORD FOUND ///");
			}
			else
			{
				recordText.AppendLine("/// EMPLOYMENT RECORD ///");
				recordText.AppendLine();

				WriteSectionIfAny(ref recordText,
					"Experience:",
					_employmentExperience);

				WriteSectionIfAny(ref recordText,
					"Formal Education History:",
					_employmentFormalEducation);

				WriteSectionIfAny(ref recordText,
					"Pre-NanoTrasen Employment History:",
					_employmentPreNtEmployment);

				WriteSectionIfAny(ref recordText,
					"NanoTrasen Employment History:",
					_employmentNtEmployment);

				WriteSectionIfAny(ref recordText,
					"Trained in the following:",
					_employmentSkills);
			}

			_employmentRecordGenerated = recordText.ToString();
		}

		private void MakeMedicalRecords()
		{
			var recordText = new StringBuilder();
			if (_commonRecords.IsEmpty())
				MakeCommonRecords();

			recordText.Append(_commonRecords);

			// TODO: make this less horrible
			if (!_medicalHistory.Any() &&
				!_medicalNotes.Any() &&
				!_medicalPsychHistory.Any() &&
				!_medicalPsychNotes.Any() &&
				!_medicalPrescriptions.Any() &&
				!TargetRecord.NoBorg &&
				!TargetRecord.NoClone &&
				!TargetRecord.NoProsthetic &&
				!TargetRecord.NoRevive)
			{
				recordText.AppendLine("/// NO MEDICAL RECORD FOUND ///");
			}
			else
			{
				recordText.AppendLine("/// MEDICAL RECORD ///");
				recordText.AppendLine();

				recordText.AppendLine(
					" The following information is protected by doctor-patient confidentiality laws. Do not release without patient's consent.\n");

				if (TargetRecord.NoBorg || TargetRecord.NoClone || TargetRecord.NoProsthetic || TargetRecord.NoRevive)
				{
					recordText.AppendLine("IMPORTANT NOTES:");

					if (TargetRecord.NoBorg)
						MakeMedicalNote(ref recordText, "DO NOT BORGIFY");

					if (TargetRecord.NoClone)
						MakeMedicalNote(ref recordText, "DO NOT CLONE");

					if (TargetRecord.NoProsthetic)
						MakeMedicalNote(ref recordText, "DO NOT INSTALL PROSTHETICS");

					if (TargetRecord.NoRevive)
						MakeMedicalNote(ref recordText, "DO NOT REVIVE");

					recordText.AppendLine();
				}

				WriteSectionIfAny(ref recordText,
					"Notes:",
					_medicalNotes);

				WriteSectionIfAny(ref recordText,
					"Medical History:",
					_medicalHistory);

				WriteSectionIfAny(ref recordText,
					"Psychiatric Notes:",
					_medicalPsychNotes);

				WriteSectionIfAny(ref recordText,
					"Psychiatric History:",
					_medicalPsychHistory);

				WriteSectionIfAny(ref recordText,
					"Prescriptions:",
					_medicalPrescriptions);
			}

			_medicalRecordGenerated = recordText.ToString();
		}

		private void MakeSecurityRecords()
		{
			var recordText = new StringBuilder();
			if (_commonRecords.IsEmpty())
				MakeCommonRecords();

			recordText.Append(_commonRecords);

			if (!_securityRecords.Any() && !_securityNotes.Any())
			{
				recordText.AppendLine("/// NO SECURITY RECORD FOUND ///");
			}
			else
			{
				recordText.AppendLine("/// SECURITY RECORD ///");
				recordText.AppendLine();

				WriteSectionIfAny(ref recordText,
					"Notes:",
					_securityNotes);

				WriteSectionIfAny(ref recordText,
					"Record:",
					_securityRecords);
			}

			_securityRecordGenerated = recordText.ToString();
		}
	}
}
