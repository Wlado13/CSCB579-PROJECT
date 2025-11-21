using System;
using System.Windows.Forms;
using StudentEnrollmentApp.Logic;
using StudentEnrollmentApp.Models;

namespace StudentEnrollmentApp.UI
{
    /// <summary>
    /// Основен форм за управление на студентите (UI слой).
    /// Автор: Владимир Василев Владимиров - F113578
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly StudentManager _manager;

        public MainForm()
        {
            InitializeComponent();
            _manager = new StudentManager();
            _manager.Load();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var list = _manager.GetAllStudents();
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.DataSource = list;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var first = textBoxFirstName.Text.Trim();
                var last = textBoxLastName.Text.Trim();
                var fn = textBoxFacultyNumber.Text.Trim();
                var email = textBoxEmail.Text.Trim();
                var dob = dateTimePickerDOB.Value.Date;

                var student = new Student(first, last, fn, email, dob);
                _manager.AddStudent(student);
                _manager.Save();

                RefreshGrid();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при добавяне: {ex.Message}");
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count == 0) return;

            var idCell = dataGridViewStudents.SelectedRows[0].Cells[0]; // Id is first hidden column
            if (idCell == null) return;

            if (!(idCell.Value is Guid id)) return;
            var ok = _manager.RemoveStudent(id);
            if (ok)
            {
                _manager.Save();
                RefreshGrid();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var q = textBoxSearch.Text.Trim();
            var results = _manager.Search(q);
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.DataSource = results;
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void ClearInputFields()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxFacultyNumber.Text = "";
            textBoxEmail.Text = "";
            dateTimePickerDOB.Value = DateTime.Today;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _manager.Save();
        }
    }
}
