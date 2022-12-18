using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace prjWinCsMidTerm
{
    public partial class frmEmployees : Form
    {
        struct Employee
        {
            public string EmpNum;
            public string FName;
            public string LName;
            public string Company;
        }
        Employee[] tabEmp = new Employee[50];
        Int16 nbEmp = 0;
        int disp = 0;
        
        public frmEmployees()
        {
            InitializeComponent();
            StreamReader myfile = new StreamReader("Employees.txt");
            int i = 0;
            while (myfile.EndOfStream == false)
            {
                tabEmp[i].EmpNum = myfile.ReadLine();
                tabEmp[i].FName = myfile.ReadLine();
                tabEmp[i].LName = myfile.ReadLine();
                tabEmp[i].Company = myfile.ReadLine();
                i++;
                nbEmp++;
            }
            myfile.Close();
            for (i = 0; i < nbEmp; i++)
            {
                lstEmpNum.Items.Add(tabEmp[i].EmpNum);
            }
        }

        private void txtBoxEmployee_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string email = txtBoxEmployee.Text.Trim();
            Int32 fn = email.IndexOf(".");
            if (fn == -1)
            {
                MessageBox.Show("PLease enter a dot seperating the values.");
                txtBoxEmployee.Focus();
                return;
            }
            Int32 ln = email.IndexOf(".", fn + 1);
            Int32 comp = email.IndexOf("@");
            Int32 comp2 = email.IndexOf(".", comp + 1);
            string fname, lname, number, company;

            fname = email.Substring(0, fn).Trim().ToUpper();
            lname = email.Substring(fn + 1, ln - (fn + 1)).Trim().ToUpper();
            number = email.Substring(ln + 1, comp - (ln + 1)).Trim().ToUpper();
            company = email.Substring(comp + 1, comp2 - (comp + 1)).ToUpper();

            if (!(lstEmpNum.Items.Contains(number)))
            {
                lstEmpNum.Items.Add(number);
                tabEmp[nbEmp].EmpNum = number;
                tabEmp[nbEmp].FName = fname;
                tabEmp[nbEmp].LName = lname;
                tabEmp[nbEmp].Company = company;
                nbEmp++;
            }
            else
            {
                MessageBox.Show("Number alredy exists. Please enter a different number.");
                txtBoxEmployee.Focus();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lstEmpNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            string numdisp = lstEmpNum.GetItemText(lstEmpNum.SelectedItem).Trim();
            for (int i = 0; i < nbEmp; i++)
            {
                if (numdisp == tabEmp[i].EmpNum)
                {
                    txtBoxFName.Text = tabEmp[i].FName;
                    txtBoxLName.Text = tabEmp[i].LName;
                    txtBoxCompany.Text = tabEmp[i].Company;
                    txtboxNumber.Text = tabEmp[i].EmpNum;
                    disp = i;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtBoxFName.Focus();
            tabEmp[disp].FName = txtBoxFName.Text;
            tabEmp[disp].LName = txtBoxLName.Text;
            tabEmp[disp].Company = txtBoxCompany.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tabEmp[disp].FName = txtBoxFName.Text;
            tabEmp[disp].LName = txtBoxLName.Text;
            tabEmp[disp].Company = txtBoxCompany.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lstEmpNum.Items.RemoveAt(disp);
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StreamWriter myfile = new StreamWriter("Employees.txt");
            for (int i = 0; i < nbEmp; i++)
            {
                myfile.WriteLine(tabEmp[i].EmpNum);
                myfile.WriteLine(tabEmp[i].FName);
                myfile.WriteLine(tabEmp[i].LName);
                myfile.WriteLine(tabEmp[i].Company);
            }
            myfile.Close();
            return;
        }
    }
}
