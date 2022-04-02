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
    public partial class frmContacts : Form
    {
        SidcEntities db = null;
        frmAddEditDelete frm = null;


        public frmContacts()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (db = new SidcEntities())
            {
                dgvView.DataSource = db.Member.ToList();
                dgvView.ReadOnly = true;
            }
        }

        private void EditDelete(member m)
        {
            frm = new frmAddEditDelete(m);
            frm.ShowDialog();

            if(frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void dgvView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvView.Rows[e.RowIndex];

            member m = row.DataBoundItem as member;
            EditDelete(m);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frm = new frmAddEditDelete();
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new SidcEntities())
                {
                    var result = db.Member.Where(n => n.EmployeeName.Contains(txtSearch.Text)).Select(s => s).ToList();

                    dgvView.DataSource = result;

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
