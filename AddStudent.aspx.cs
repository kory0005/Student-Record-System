using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_4
{
    public partial class AddStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*************** HEADER MENU LINKS ***************/
            LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
            BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");

            if (!IsPostBack)
            {
                topMenu.Items.Add(new ListItem("Add Courses"));
            }

            btnHome.Click += (arg1, arg2) => Response.Redirect("Default.aspx");
            topMenu.Click += (arg1, arg2) => Response.Redirect("AddCourses.aspx");


            /*************** GENERATE DROP DOWN LIST ***************/
            if(!IsPostBack)
            {
                using(StudentRecordEntities entityContext = new StudentRecordEntities())
                {
                    var couses = (from c in entityContext.Courses select new {CourseId = c.Code, CourseText = c.Code + " - " + c.Title}).ToList();

                    drpAddedCourses.DataSource = couses;
                    drpAddedCourses.DataValueField = "CourseId";
                    drpAddedCourses.DataTextField = "CourseText";
                    drpAddedCourses.DataBind();
                }
            }

        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            /*************** Validation (Back-end) ***************/
            bool hasErrors = false;

            if (txtStudentNum.Text == "")
            {
                ValidateIfExistId.Text = "";
                validateTxtStudentNum.Text = "Required";
                hasErrors = true;
            }
            if (txtStudentName.Text == "")
            {
                validateStudentName.Text = "Required";
                hasErrors = true;
            }
            if (txtGrade.Text == "")
            {
                validateTxtStudentGrade.Text = "Required";
                hasErrors = true;
            }

            if (hasErrors == false)
            {
                validateTxtStudentNum.Text = "";
                validateStudentName.Text = "";
                validateTxtStudentGrade.Text = "";

                using (StudentRecordEntities entityContext = new StudentRecordEntities())
                {
                    foreach (Course c in entityContext.Courses)
                    {
                        if (c.Code == drpAddedCourses.SelectedItem.Value)
                        {
                            List<Student> students = entityContext.Students.ToList<Student>();
                            List<AcademicRecord> records = entityContext.AcademicRecords.ToList<AcademicRecord>();

                            var inStudents = (from s in students
                                              where s.Id == txtStudentNum.Text
                                              select s.Id).FirstOrDefault();                                             // if exist in Students table

                            var inRecords = (from r in records
                                             where r.StudentId == inStudents && r.CourseCode == c.Code
                                             select r.StudentId).FirstOrDefault();                                       // if exist in Records



                            if (inStudents != null && inRecords != null)         // if exist in both Students and Acdm Records tables
                            {
                                ValidateIfExistId.Text = "Student with this id already exist";
                            }
                            else
                            {
                                ValidateIfExistId.Text = "";
                                if (inStudents == null)
                                {
                                    /************* Create new data item(student) *************/
                                   Student student = new Student();
                                    student.Id = txtStudentNum.Text;
                                    student.Name = txtStudentName.Text;
                                    entityContext.Students.Add(student);
                                }

                                /************* Create new data item(academic record) *************/
                                AcademicRecord record = new AcademicRecord();
                                record.CourseCode = c.Code;
                                record.Grade = Convert.ToInt16(txtGrade.Text);
                                record.StudentId = txtStudentNum.Text;
                                entityContext.AcademicRecords.Add(record);

                            }
                        }
                    }
                    entityContext.SaveChanges();

                }


                /***************  DELETE DATA ITEM FROM STUDENTS AND ACADEMIC RECORDS TABLES ***************/
                /*using (StudentRecordEntities entityContext = new StudentRecordEntities())
                {
                    var student = (from s in entityContext.Students where s.Id == txtStudentNum.Text select s).FirstOrDefault<Student>();

                    if (student != null)
                    {
                        for (int i = student.AcademicRecords.Count() - 1; i >= 0; i--)
                        {
                            var ar = student.AcademicRecords.ElementAt<AcademicRecord>(i);
                            student.AcademicRecords.Remove(ar);
                        }
                    }

                    entityContext.Students.Remove(student);
                    entityContext.SaveChanges();
                }*/

            }
        }

        private void DisplayStudentTable(List<AcademicRecord> records)
        {
            if (records.Count() == 0)
            {
                TableRow rowNew = new TableRow();

                TableCell varningCell = new TableCell();
                varningCell.Text = "No courses record exist";
                varningCell.ColumnSpan = 3;
                varningCell.ForeColor = System.Drawing.Color.Red;
                varningCell.HorizontalAlign = HorizontalAlign.Center;
                rowNew.Cells.Add(varningCell);

                tblStudents.Rows.Add(rowNew);
            }
            else
            {
                /*************** DRAWS TABLE ***************/

                /*************** DRAW DATA ITEM IN TABLE ***************/
                foreach (AcademicRecord record in records)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Text = record.Student.Id;
                    row.Cells.Add(cell);

                    TableCell cell2 = new TableCell();
                    cell2.Text = record.Student.Name;
                    row.Cells.Add(cell2);

                    TableCell cell3 = new TableCell();
                    cell3.Text = record.Grade.ToString();
                    row.Cells.Add(cell3);

                    tblStudents.Rows.Add(row);
                }
            }
        }

        /*************** Draw table when select another course (draws all students if they exist in the selected course) ***************/
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            using (StudentRecordEntities entityContext = new StudentRecordEntities())
            {
                Course currentCourse = entityContext.Courses.Find(drpAddedCourses.SelectedItem.Value.ToString());
                DisplayStudentTable(currentCourse.AcademicRecords.ToList());

            }
        }

        protected void drpAddedCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*************** Clear Back-end Validation Fields & Text Fields ***************/
            validateTxtStudentNum.Text = "";
            validateStudentName.Text = "";
            validateTxtStudentGrade.Text = "";

            txtStudentNum.Text = "";
            txtStudentName.Text = "";
            txtGrade.Text = "";

            ValidateIfExistId.Text = "";
        }
    }
}