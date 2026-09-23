using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using WinFormsApp1;
using Microsoft.Data.SqlClient;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        string output = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Documentation", "Images"));
        Directory.CreateDirectory(output);
        string target = args.Length == 0 ? "login" : args[0].ToLowerInvariant();
        if (target == "login") Capture(new frmLogin(), Path.Combine(output, "LoginForm.png"));
        if (target == "registration") Capture(new SnapshotRegistration(), Path.Combine(output, "RegistrationForm.png"));
        if (target == "records") Capture(new SnapshotRecords("Data Source=.\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"),
            Path.Combine(output, "StudentRecordsForm.png"));
        if (target == "dbtest") VerifyDatabase();
    }

    private static void VerifyDatabase()
    {
        const string cs = "Data Source=.\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        using var connection = new SqlConnection(cs);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        const string sql = @"INSERT INTO dbo.Registration
(regNo,firstName,lastName,dateOfBirth,gender,address,email,mobilePhone,homePhone,parentName,nic,contactNo)
VALUES (-2147483648,'QA','Student','2000-01-01','Male','Test Address','qa@example.com',771234567,112345678,'QA Parent','QA-NIC',771234567);
UPDATE dbo.Registration SET firstName='QA Updated' WHERE regNo=-2147483648;
IF NOT EXISTS (SELECT 1 FROM dbo.Registration WHERE regNo=-2147483648 AND firstName='QA Updated')
 THROW 50000,'CRUD verification failed',1;
DELETE FROM dbo.Registration WHERE regNo=-2147483648;
IF EXISTS (SELECT 1 FROM dbo.Registration WHERE regNo=-2147483648)
 THROW 50001,'Delete verification failed',1;";
        using var command = new SqlCommand(sql, connection, transaction);
        command.ExecuteNonQuery();
        transaction.Rollback();
        Console.WriteLine("Connection and transactional CRUD verification passed.");
    }

    private sealed class SnapshotRegistration : frmRegistration
    {
        protected override void OnLoad(EventArgs e) { }
    }

    private sealed class SnapshotRecords : frmStudentRecords
    {
        public SnapshotRecords(string connectionString) : base(connectionString) { }
        protected override void OnLoad(EventArgs e) { }
    }

    private static void Capture(Form form, string path)
    {
        using (form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(-3000, -3000);
            form.ShowInTaskbar = false;
            form.Show();
            Application.DoEvents();
            form.Refresh();
            using var image = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(image, new Rectangle(Point.Empty, form.Size));
            image.Save(path, ImageFormat.Png);
            form.Hide();
        }
    }
}
