using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_4
{
    public partial class AddCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*************** HEADER MENU LINKS ***************/
            LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
            BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");

            if (!IsPostBack)
            {
                topMenu.Items.Add(new ListItem("Add Student Records"));
            }

            btnHome.Click += (arg1, arg2) => Response.Redirect("Default.aspx");
            topMenu.Click += (arg1, arg2) => Response.Redirect("AddStudent.aspx");


            /*************** DISPLAY EXISTING RECORDS(COURSES) IN THE TABLE ***************/
            if (!IsPostBack)
            {
                using (StudentRecordEntities entityContext = new StudentRecordEntities())
                {
                    List<Course> courses = entityContext.Courses.ToList<Course>();

                    DisplayCourseTable(courses);
                }
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            /*************** ADD NEW DATA ITEM TO THE DATABASE ***************/
            using (StudentRecordEntities entityContext = new StudentRecordEntities())
            {
                /*************** Validation (Back-end) ***************/
                bool hasErrors = false;

                if (txtCourseNum.Text == "")
                {
                    ValidateIfExistNum.Text = "";
                    validateTxtCourseNum.Text = "Required";
                    hasErrors = true;
                }
                else
                {
                    validateTxtCourseNum.Text = "";
                }

                if (txtCourseName.Text == "")
                {
                    validateTxtCourseName.Text = "Required";
                    hasErrors = true;
                }
                else
                {
                    validateTxtCourseName.Text = "";
                }

                if (hasErrors == false)
                {
                    validateTxtCourseNum.Text = "";
                    validateTxtCourseName.Text = "";

                    /*************** Create new data item (course) ***************/
                    Course course = new Course();
                    course.Code = txtCourseNum.Text;
                    course.Title = txtCourseName.Text;

                    if (entityContext.Courses.ToList().Any(x => x.Code == course.Code))
                    {
                        ValidateIfExistNum.Text = "Course with this code already exist";
                    }
                    else
                    {
                        ValidateIfExistNum.Text = "";
                        entityContext.Courses.Add(course);
                        entityContext.SaveChanges();
                    }

                }

                List<Course> courses = entityContext.Courses.ToList<Course>();
                DisplayCourseTable(courses);
            }


            /***************  DELETE DATA ITEM FROM COURSES AND ACADEMIC RECORDS TABLES ***************/
            /*using (StudentRecordEntities entityContext = new StudentRecordEntities())
            {
                var course = (from c in entityContext.Courses where c.Code == txtCourseNum.Text select c).FirstOrDefault<Course>();

                if (course != null)
                {
                    for (int i = course.AcademicRecords.Count() - 1; i >= 0; i--)
                    {
                        var ar = course.AcademicRecords.ElementAt<AcademicRecord>(i);
                        course.AcademicRecords.Remove(ar);
                    }
                }

                entityContext.Courses.Remove(course);
                entityContext.SaveChanges();
            }*/

        }

        private void DisplayCourseTable(List<Course> courseList)
        {
            var course = (from c in courseList select new Course { Code = c.Code, Title = c.Title }).ToList();

            if (course.Count == 0)
            {
                TableRow rowNew = new TableRow();
                tblCourses.Rows.Add(rowNew);

                TableCell varningCell = new TableCell();
                rowNew.Cells.Add(varningCell);

                varningCell.Text = "No courses record exist";
                varningCell.ColumnSpan = 3;
                varningCell.ForeColor = System.Drawing.Color.Red;
                varningCell.HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                foreach (Course c in course)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell();
                    cell.Text = c.Code;
                    row.Cells.Add(cell);

                    TableCell cell2 = new TableCell();
                    cell2.Text = c.Title;
                    row.Cells.Add(cell2);

                    tblCourses.Rows.Add(row);
                }
            }
        }

    }
}