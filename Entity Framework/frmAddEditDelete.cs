using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity_Framework.Model;

namespace Entity_Framework
{
    public partial class frmAddEditDelete : Form
    {
        member memberInfo = new member();

        public frmAddEditDelete()
        {
            InitializeComponent();

            btnSave.Location = new Point(391, 247);
            btnDelete.Visible = false;
        }
        
        public frmAddEditDelete(member _member)
        {
            InitializeComponent();

            memberInfo = _member;
            lblID.Text = _member.Id.ToString();
            txtName.Text = _member.EmployeeName;
            txtEmail.Text = _member.Email;
            txtContactNumber.Text = _member.ContactNumber;
            txtAddress.Text = _member.Address;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var msg = MessageBox.Show("Are you sure you want to save data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(msg.Equals(DialogResult.Yes))
                {
                    using (var db = new SidcEntities())
                    {
                        memberInfo.EmployeeName = txtName.Text;
                        memberInfo.ContactNumber = txtContactNumber.Text;
                        memberInfo.Email = txtEmail.Text;
                        memberInfo.Address = txtAddress.Text;
                        memberInfo.CreatedDate = DateTime.Now;

                        db.Member.Add(memberInfo);
                        int result = db.SaveChanges();

                        if(result > 0)
                        {
                            MessageBox.Show("Data saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Failed to save data. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var msg = MessageBox.Show("Are you sure you want to delete data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msg.Equals(DialogResult.Yes))
                {
                    using (var db = new SidcEntities())
                    {
                        db.Member.Remove(memberInfo);

                        int result = db.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Data deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Failed to delete data. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
