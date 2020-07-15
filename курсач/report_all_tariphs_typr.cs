﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace курсач
{
    public partial class report_all_tariphs_typr : Form
    {
        Form2 f2;
        tree_providers.root pr;
        int type;
        int error_number;
        public report_all_tariphs_typr()
        {
            InitializeComponent();
        }
        public report_all_tariphs_typr(Form2 a)
        {
            f2 = a;
            InitializeComponent();
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            pr = f2.provider.find(textBoxProvider.Text);
            type = 0;
            if (checkBoxAddTariphInternet.Checked)
                type = 1;
            else if (checkBoxAddTariphTV.Checked)
                type = 3;
            else if ((checkBoxAddTariphTV.Checked) && (checkBoxAddTariphInternet.Checked))
                type = 2;
            if (type == 0)
            {
                error_number = 1;
                f2.message_box(error_number);
            }
            else if (textBoxProvider.Text == "")
            {
                error_number = 1;
                f2.message_box(error_number);
            }
            else if (pr == null)
            {
                error_number = 3;
                f2.message_box(error_number);
            }
            else if (textBoxProvider.Text.Length > 30)
            {
                error_number = 2;
                f2.message_box(error_number);
            }
            else
            {
                string name = "";
                for (int i = 0; i < pr.current_tariph; i++)
                {
                    spisok_tariph.nest a = f2.tariph.find(pr.arr[i].name, pr);
                    if (a.type == type)
                    {
                        name = a.name;
                        dataGridViewTypeRep.Rows.Add(name);
                    }
                }
            }
        }

        private void buttonSaveInFile_Click(object sender, EventArgs e)
        {
            StreamWriter file = new StreamWriter(@"a:\gitjub\курсач\otchet_provider_type.txt");
            if (pr == null)
            {
                file.Close();
                error_number = 5;
                f2.message_box(error_number);
            }
            else
            {
                file.WriteLine("Провайдер: " + pr.title);
                file.WriteLine("Тип: " + type);
                int i = 0;
                while (dataGridViewTypeRep.Rows[i].Cells[0].Value != null)
                {
                    file.WriteLine(dataGridViewTypeRep.Rows[i].Cells[0].Value.ToString());
                    i++;
                }
                file.Close();
            }
        }

        private void dataGridViewTypeRep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void report_all_tariphs_typr_Load(object sender, EventArgs e)
        {

        }

        private void textBoxProvider_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
