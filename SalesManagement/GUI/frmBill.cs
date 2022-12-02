using BUS;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmBill : Form
    {
        frmBillInfo fBillInfo;
        BUS_Bill busBill = new BUS_Bill();

        public frmBill(string email)
        {
            InitializeComponent();
            fBillInfo = new frmBillInfo(email);
        }
       
        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            fBillInfo.ShowDialog();
            this.Show();
            gvBill.DataSource = busBill.ListOfBills();
            LoadGridView();
        }

        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            ToExcel(gvBill, @"D:\visual\file");
        }

        private void LoadGridView()
        {
            gvBill.Columns[0].HeaderText = "Mã HD";
            gvBill.Columns[1].HeaderText = "Tên KH";
            gvBill.Columns[2].HeaderText = "Thời gian";
            gvBill.Columns[3].HeaderText = "Tổng tiền";
            foreach (DataGridViewColumn item in gvBill.Columns)
            {
                item.DividerWidth = 1;
            }
            gvBill.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvBill.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvBill.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void ToExcel(DataGridView dataGridView1, string fileName)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Quản lý bán hàng";

                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
    


private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string name = txtSearch.Text.Trim();
            if (name == "")
            {
                frmBill_Load(sender, e);
                txtSearch.Focus();
            }
            else
            {
                DataTable data = busBill.SearchCustomerInBill(txtSearch.Text);
                gvBill.DataSource = data;
            }
        }

        private void frmBill_Load(object sender, EventArgs e)
        {
            gvBill.DataSource = busBill.ListOfBills();
            LoadGridView();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
