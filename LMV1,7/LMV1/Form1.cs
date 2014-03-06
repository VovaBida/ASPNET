//Form.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LMV1
{
    public partial class Form1 : Form
    {
        List <Human> humanList=new List <Human>(0);
        List<Deposit> depositList = new List<Deposit>(0);
        public Form1()
        {
            InitializeComponent();
        }

        private void saveToXML(string filename) 
        {
            using (XmlTextWriter writer = new XmlTextWriter(filename,Encoding.Unicode))
            {
                writer.Formatting = Formatting.Indented ;
                writer.Indentation = 1;
                writer.IndentChar = '\t';
                writer.WriteStartDocument ();
                writer.WriteStartElement("root");
                foreach (Human hum in humanList)
                {
                    writer.WriteStartElement("Human");
                    writer.WriteElementString("Name",hum.name);
                    writer.WriteElementString("Surname", hum.surname);
                    writer.WriteEndElement();
                }

                foreach (Deposit dep in depositList)
                {
                    if (dep is Credit) writer.WriteStartElement("Credit"); else writer.WriteStartElement("Deposit");
                    writer.WriteElementString("Amount",dep.amount.ToString());
                    writer.WriteElementString("Months", dep.months.ToString());
                    writer.WriteElementString("Percent", dep.percent.ToString());

                    writer.WriteStartElement("Client");
                    writer.WriteElementString("Name", dep.client.name);
                    writer.WriteElementString("Surname", dep.client.surname);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Manager");
                    writer.WriteElementString("Name", dep.manag.name);
                    writer.WriteElementString("Surname", dep.manag.surname);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Human newhum = new Human(textBox1.Text, textBox2.Text);
            humanList.Add(newhum);
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            if (humanList.Count > dataGridView1.RowCount) { dataGridView1.RowCount=humanList.Count; }
            for (int i = 0; i < humanList.Count; i++) { 
                dataGridView1.Rows[i].Cells[0].Value = humanList[i].name;
                dataGridView1.Rows[i].Cells[1].Value = humanList[i].surname;
                comboBox1.Items.Add(humanList[i].name+" "+humanList[i].surname);
                comboBox2.Items.Add(humanList[i].name + " " + humanList[i].surname);

            }
        
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Human temp = humanList[dataGridView1.CurrentCell.RowIndex];
            dataGridView2.RowCount = temp.credit_history.Count + temp.deposit_history.Count;
            int i = 0;
            for (; i < temp.deposit_history.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = temp.deposit_history[i].manag.surname;
                dataGridView2.Rows[i].Cells[1].Value = temp.deposit_history[i].client.surname;
                dataGridView2.Rows[i].Cells[2].Value = temp.deposit_history[i].amount;
                dataGridView2.Rows[i].Cells[3].Value = temp.deposit_history[i].percent;
                dataGridView2.Rows[i].Cells[4].Value = "Deposit";
                dataGridView2.Rows[i].Cells[5].Value = temp.deposit_history[i].MonthPay();
            };
            for (int j=0; j < temp.credit_history.Count; j++)
            {
                dataGridView2.Rows[i].Cells[0].Value = temp.credit_history[j].manag.surname;
                dataGridView2.Rows[i].Cells[1].Value = temp.credit_history[j].client.surname;
                dataGridView2.Rows[i].Cells[2].Value = temp.credit_history[j].amount;
                dataGridView2.Rows[i].Cells[3].Value = temp.credit_history[j].percent;
                dataGridView2.Rows[i].Cells[4].Value = "Credit";
                dataGridView2.Rows[i].Cells[5].Value = temp.credit_history[j].MonthPay();
                i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Deposit d=new Deposit();
            d.amount=Convert.ToInt32(textBox3.Text);
            d.percent = Convert.ToInt32(numericUpDown1.Value);
            d.months = Convert.ToInt32(textBox4.Text);
            d.client = (Human)humanList[comboBox1.SelectedIndex];
            d.manag = (Human)humanList[comboBox2.SelectedIndex];
            humanList[comboBox1.SelectedIndex].deposit_history.Add(d);
            humanList[comboBox2.SelectedIndex].deposit_history.Add(d);
            depositList.Add(d);
            dataGridView1_CellEnter(dataGridView1, new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Credit d = new Credit();
            d.amount = Convert.ToInt32(textBox3.Text);
            d.percent = Convert.ToInt32(numericUpDown1.Value);
            d.months = Convert.ToInt32(textBox4.Text);
            d.client = (Human)humanList[comboBox1.SelectedIndex];
            d.manag = (Human)humanList[comboBox2.SelectedIndex];
            depositList.Add(d);
            humanList[comboBox1.SelectedIndex].credit_history.Add(d);
            humanList[comboBox2.SelectedIndex].credit_history.Add(d);
            dataGridView1_CellEnter(dataGridView1, new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));
        }

        private void saveToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToXML("1.xml");
        }

        private void loadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromXML("1.xml");

            if (humanList.Count > dataGridView1.RowCount) { dataGridView1.RowCount = humanList.Count; }
            for (int i = 0; i < humanList.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = humanList[i].name;
                dataGridView1.Rows[i].Cells[1].Value = humanList[i].surname;
                comboBox1.Items.Add(humanList[i].name + " " + humanList[i].surname);
                comboBox2.Items.Add(humanList[i].name + " " + humanList[i].surname);

            }
        }
        private void ReadHuman_Short(XmlReader reader)
        {
            Human temp = new Human();
            humanList.Add(temp);
            while (reader.Read())
            {
                if (reader.IsStartElement("Name"))
                    temp.name = reader.ReadString();
                else
                    if (reader.IsStartElement("Surname"))
                        temp.surname = reader.ReadString();
                    else break;
            }
        }

        private void ReadCredit(XmlReader reader)
        {
            Credit cr = new Credit();
            while (reader.Read())
            {
                if (reader.IsStartElement("Amount"))
                    cr.amount = Int32.Parse(reader.ReadString());
                else
                    if (reader.IsStartElement("Months"))
                        cr.months = Int32.Parse(reader.ReadString());
                    else
                        if (reader.IsStartElement("Percent"))
                            cr.percent = Double.Parse(reader.ReadString());
                        else if (reader.IsStartElement("Manager"))
                        {

                            Human temp = new Human();
                            while (reader.Read())
                            {
                                if (reader.IsStartElement("Name"))
                                    temp.name = reader.ReadString();
                                else if (reader.IsStartElement("Surname"))
                                    temp.surname = reader.ReadString();
                                else break;
                            }
                            cr.manag = temp;
                        }
                        else if (reader.IsStartElement("Client"))
                        {

                            Human temp = new Human();
                            while (reader.Read())
                            {
                                if (reader.IsStartElement("Name"))
                                    temp.name = reader.ReadString();
                                else if (reader.IsStartElement("Surname"))
                                    temp.surname = reader.ReadString();
                                else break;
                            }
                            cr.client = temp;
                        }
              
            }
            
            foreach (Human hum in this.humanList)
            {
                if (hum.Equals(cr.manag)) { cr.manag = hum; if (!hum.credit_history.Contains(cr)) hum.credit_history.Add(cr); }
                if (hum.Equals(cr.client)) { cr.client = hum; if (!hum.credit_history.Contains(cr)) hum.credit_history.Add(cr); }

            }
            depositList.Add(cr);
        }

        private void LoadFromXML(string fileName)
        {
            humanList.Clear();
            depositList.Clear();

            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement("Human") && !reader.IsEmptyElement)
                    {
                        ReadHuman_Short(reader);
                        
                    }

                    if (reader.IsStartElement("Credit"))
                    {
                        ReadCredit(reader);
                    }

                    if (reader.IsStartElement("Deposit"))
                    {
                        ReadDeposit(reader);
                    }

                }
                
            }

        }

        private void ReadDeposit(XmlTextReader reader)
        {
            Deposit cr = new Deposit();
            while (reader.Read())
            {
                if (reader.IsStartElement("Amount"))
                    cr.amount = Int32.Parse(reader.ReadString());
                else
                    if (reader.IsStartElement("Months"))
                        cr.months = Int32.Parse(reader.ReadString());
                    else
                        if (reader.IsStartElement("Percent"))
                            cr.percent = Double.Parse(reader.ReadString());
                        else if (reader.IsStartElement("Manager"))
                        {

                            Human temp = new Human();
                            while (reader.Read())
                            {
                                if (reader.IsStartElement("Name"))
                                    temp.name = reader.ReadString();
                                else if (reader.IsStartElement("Surname"))
                                    temp.surname = reader.ReadString();
                                else break;
                            }
                            cr.manag = temp;
                        }
                        else if (reader.IsStartElement("Client"))
                        {

                            Human temp = new Human();
                            while (reader.Read())
                            {
                                if (reader.IsStartElement("Name"))
                                    temp.name = reader.ReadString();
                                else if (reader.IsStartElement("Surname"))
                                    temp.surname = reader.ReadString();
                                else break;
                            }
                            cr.client = temp;
                        }
                
            }
            foreach (Human hum in this.humanList)
            {
                if (hum.Equals(cr.manag)) { cr.manag = hum; if (!hum.deposit_history.Contains(cr)) hum.deposit_history.Add(cr); }
                if (hum.Equals(cr.client)) {cr.client = hum; if (!hum.deposit_history.Contains(cr)) hum.deposit_history.Add(cr);}

            }
            depositList.Add(cr);
        }
    }
}
