namespace AuroraRecordGenerator
{
	public partial class GeneratedResultWindow
	{
		public GeneratedResultWindow()
		{
			InitializeComponent();
		}

		public GeneratedResultWindow(Record record) : this()
		{
			var formatter = new RecordFormatter(record);
			EmploymentBox.Text = formatter.EmploymentRecords;
			MedicalBox.Text = formatter.MedicalRecords;
			SecurityBox.Text = formatter.SecurityRecords;
		}
	}
}
