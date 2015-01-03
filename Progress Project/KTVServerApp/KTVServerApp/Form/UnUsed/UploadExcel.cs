using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyExcel=Microsoft.Office.Interop.Excel;

namespace KTVServerApp
{
    public partial class UploadExcel : Form
    {
        public UploadExcel()
        {
            InitializeComponent();
        }

        private void TestExcel_Load(object sender, EventArgs e)
        {
            
        }
        private void InitDataGrid(string path)
        {
            MyExcel.Range range = ReadExcelFile(@path);
            List<string> headers = GetListHeader(range);
            foreach (string head in headers)
            {
                DataGridViewTextBoxColumn txt = new DataGridViewTextBoxColumn();
                txt.HeaderText = head;
                dgvSong.Columns.Add(txt);
            }
            LoadData(range);
        }
        private MyExcel.Range ReadExcelFile(string path)
        {
            MyExcel.Application xlp = new MyExcel.Application();
            MyExcel.Workbook xlworkbook = xlp.Workbooks.Open(path, 0, true, 5, "", "", true, MyExcel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            MyExcel._Worksheet xlworksheet = (MyExcel._Worksheet)xlworkbook.Sheets[1];
            MyExcel.Range xlRange = xlworksheet.UsedRange;
            return xlRange;
        }
        private List<string> GetListHeader(MyExcel.Range range)
        {
            List<string> headers = new List<string>();
            for (int i = 1; i <= range.Cells.Columns.Count; i++)
            {
                headers.Add((string)(range.Cells[1, i] as MyExcel.Range).Value2);
            }
                return headers;
        }
        private void LoadData(MyExcel.Range range)
        {
            //MessageBox.Show((range.Cells[3, 2] as MyExcel.Range).Value2);
            
            int rnd = 1;
            Object[,] data = range.get_Value(Type.Missing);
           
            for (int i = 2; i <= data.GetLength(0); i++)
            {
                int ind = dgvSong.Rows.Add();
                DataGridViewRow row = dgvSong.Rows[ind];
                for (int j = 1; j <= data.GetLength(1); j++)
                {
                    if (j == 1)
                    {
                        row.Cells[j-1].Value = rnd + "";
                    }
                    else
                    {
                        row.Cells[j-1].Value = data[i, j].ToString();
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Multiselect = true;
            open.Filter = "Excel File (*.XLS)|*.xls";
            open.Title = "Select Excel";
            if (open.ShowDialog() == DialogResult.OK)
            {
                InitDataGrid(open.FileName);
            }
        }
    }
}
