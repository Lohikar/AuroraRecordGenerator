using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuroraRecordGenerator
{
	internal partial class RecordFormatter
	{
		public Record TargetRecord
		{
			get { return _targetRecord; }
			set
			{
				UpdateSplitRecords();
				MakeCommonRecords();
				_lastRecordHash = value.GetHashCode();
				_targetRecord = value;
			}
		}

		public RecordFormatter(Record r)
		{
			_targetRecord = r;
			UpdateSplitRecords();
			MakeCommonRecords();
			_lastRecordHash = r.GetHashCode();
		}

		private Record _targetRecord;

		private int? _lastRecordHash;

		private IList<string> _medicalPublicRecord;
		private IList<string> _medicalHistory;
		private IList<string> _medicalNotes;
		private IList<string> _medicalPsychHistory;
		private IList<string> _medicalPsychNotes;
		private IList<string> _medicalPrescriptions;

		private IList<string> _securityPublicRecord;
		private IList<string> _securityRecords;
		private IList<string> _securityNotes;

		private IList<string> _employmentPublicRecord;
		private IList<string> _employmentExperience;
		private IList<string> _employmentPreNtEmployment;
		private IList<string> _employmentFormalEducation;
		private IList<string> _employmentNtEmployment;
		private IList<string> _employmentSkills;


		private void UpdateSplitRecords()
		{
			if (_targetRecord == null)
			{
				_targetRecord = new Record();
			}

			// Medical
			_medicalPublicRecord = _targetRecord.MedicalPublicRecord?.LineSplit();
			_medicalHistory = _targetRecord.MedicalHistory?.LineSplit();
			_medicalNotes = _targetRecord.MedicalNotes?.LineSplit();
			_medicalPsychHistory = _targetRecord.MedicalPsychHistory?.LineSplit();
			_medicalPsychNotes = _targetRecord.MedicalPsychNotes?.LineSplit();
			_medicalPrescriptions = _targetRecord.MedicalPrescriptions?.LineSplit();

			// security
			_securityPublicRecord = _targetRecord.SecurityPublicRecord?.LineSplit();
			_securityRecords = _targetRecord.SecurityRecords?.LineSplit();
			_securityNotes = _targetRecord.SecurityNotes?.LineSplit();

			// employment
			_employmentPublicRecord = _targetRecord.EmploymentPublicRecord?.LineSplit();
			_employmentExperience = _targetRecord.EmploymentExperience?.LineSplit();
			_employmentPreNtEmployment = _targetRecord.EmploymentPreNtEmployment?.LineSplit();
			_employmentFormalEducation = _targetRecord.EmploymentFormalEducation?.LineSplit();
			_employmentNtEmployment = _targetRecord.EmploymentNtEmploymentHistory?.LineSplit();
			_employmentSkills = _targetRecord.EmploymentSkills?.LineSplit();

			// flush the record cache so they're regenerated
			_commonRecords = null;
			_medicalRecordGenerated = null;
			_securityRecordGenerated = null;
			_employmentRecordGenerated = null;
		}

		public string EmploymentRecords
		{
			get
			{
				//if (_employmentRecordGenerated.IsEmpty())
					MakeEmploymentRecords();
				return _employmentRecordGenerated;
			}
		}

		private string _employmentRecordGenerated;

		public string MedicalRecords
		{
			get
			{
				//if (_medicalRecordGenerated.IsEmpty())
					MakeMedicalRecords();
				return _medicalRecordGenerated;
			}
		}

		private string _medicalRecordGenerated;

		public string SecurityRecords
		{
			get
			{
				//if (_securityRecordGenerated.IsEmpty())
					MakeSecurityRecords();
				return _securityRecordGenerated;
			}
		}

		private string _securityRecordGenerated;

		private string _commonRecords;
		

		/// <summary>
		///		Writes the <see cref="string"/> form of a record section to the specified <see cref="StringBuilder"/>, as long as there's entries to write.
		/// </summary>
		/// <param name="builder">The <see cref="StringBuilder"/> to write to.</param>
		/// <param name="header">The title for the section.</param>
		/// <param name="entries">The entries of this section.</param>
		private static void WriteSectionIfAny(ref StringBuilder builder, string header, IList<string> entries)
		{
			if (entries == null || !entries.Any() || entries[0].Trim().Length == 0)
				return;
			builder.AppendLine(header);
			builder.AppendLine(entries.FormatAsList());
		}

		/// <summary>
		///		Writes the <see cref="string"/> form of a record section to the specified <see cref="StringBuilder"/>, as long as there's entries to write. 
		///		Inserts a newline before the section.
		/// </summary>
		/// <param name="builder">The <see cref="StringBuilder"/> to write to.</param>
		/// <param name="header">The title for the section.</param>
		/// <param name="entries">The entries of this section.</param>
		private static void WritePrefixedSectionifAny(ref StringBuilder builder, string header, IList<string> entries)
		{
			builder.AppendLine();
			WriteSectionIfAny(ref builder, header, entries);
		}

		private string MakeNameLine()
		{
			var builder = new StringBuilder("Name: ");
			builder.Append(_targetRecord.FirstName);
			builder.Append(_targetRecord.MiddleName.SpaceIfValue());
			builder.Append($" {_targetRecord.LastName}");
			builder.Append(_targetRecord.NameSuffix.SpaceIfValue());
			return builder.ToString();
		}

		private static void MakeMedicalNote(ref StringBuilder b, string s) =>
			b.AppendLine($" - {s}");
	}
}
