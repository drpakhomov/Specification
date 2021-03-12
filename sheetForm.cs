using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Specification
{
    public partial class sheetForm : Form
    {
        public sheetForm(Specification spec)
        {
            InitializeComponent();
            this.Text = "Просмотр закупочной ведомости: " + spec.specName;

            foreach (equipmentSet set in spec)
            {
                string[] row = new string[2];
                int pos1 = spec.IndexOf(set) + 1;
                row[0] = pos1.ToString();
                row[1] = set.setName;
                dataGridView1.Rows.Add(row);

                var query = (from t in set.setEquip
                             group t by new { t.equipCode, t.equipCurrency, t.equipDelType, t.equipName, t.equipPrice, t.equipProducer, t.equipSeller }
                                 into grup
                                 select new
                                 {
                                     grup.Key.equipCode,
                                     grup.Key.equipCurrency,
                                     grup.Key.equipDelType,
                                     grup.Key.equipName,
                                     grup.Key.equipPrice,
                                     grup.Key.equipProducer,
                                     grup.Key.equipSeller
                                 }).ToList();

                List<Equipment> list = new List<Equipment>();
                foreach (var equip in query)
                {
                    int count = 0;
                    foreach (Equipment equ in set.setEquip)
                        if (equip.equipCode == equ.equipCode && equip.equipCurrency == equ.equipCurrency && equip.equipDelType == equ.equipDelType && equip.equipName == equ.equipName && equip.equipProducer == equ.equipProducer && equip.equipSeller == equ.equipSeller && equip.equipPrice == equ.equipPrice)
                            count += equ.equipCount;
                    list.Add(new Equipment(0, equip.equipSeller, equip.equipCode, equip.equipName, count, equip.equipPrice, equip.equipProducer, (int)equip.equipCurrency, (int)equip.equipDelType, null, null));
                }

                int pos2 = 0;
                foreach (Equipment equip in list)
                {
                    row = new string[9];
                    pos2++;
                    row[0] = pos1.ToString() + "." + pos2.ToString();
                    row[1] = equip.equipName;
                    row[2] = equip.equipCode.ToString();
                    row[3] = equip.equipSeller;
                    row[4] = equip.equipProducer;
                    row[5] = equip.equipCount.ToString();
                    row[6] = equip.equipPrice.ToString() + " " + mainForm.selfRef.CurrencyToString(equip.equipCurrency);
                    row[7] = (equip.equipPrice * equip.equipCount).ToString() + " " + mainForm.selfRef.CurrencyToString(equip.equipCurrency);
                    row[8] = equip.equipDelType.ToString();
                    dataGridView1.Rows.Add(row);
                }

                foreach (equipmentGroup group in set)
                {
                    row = new string[9];
                    pos2++;
                    row[0] = pos1.ToString() + "." + pos2.ToString();
                    row[1] = group.groupName;
                    dataGridView1.Rows.Add(row);

                    equipmentGroup grp = group;
                    query = (from t in grp
                                 group t by new { t.equipCode, t.equipCurrency, t.equipDelType, t.equipName, t.equipPrice, t.equipProducer, t.equipSeller }
                                     into grup
                                     select new
                                     {
                                         grup.Key.equipCode,
                                         grup.Key.equipCurrency,
                                         grup.Key.equipDelType,
                                         grup.Key.equipName,
                                         grup.Key.equipPrice,
                                         grup.Key.equipProducer,
                                         grup.Key.equipSeller
                                     }).ToList();

                    list = new List<Equipment>();
                    foreach (var equip in query)
                    {
                        int count = 0;
                        foreach (Equipment equ in group)
                            if (equip.equipCode == equ.equipCode && equip.equipCurrency == equ.equipCurrency && equip.equipDelType == equ.equipDelType && equip.equipName == equ.equipName && equip.equipProducer == equ.equipProducer && equip.equipSeller == equ.equipSeller && equip.equipPrice == equ.equipPrice)
                                count += equ.equipCount;
                        list.Add(new Equipment(0, equip.equipSeller, equip.equipCode, equip.equipName, count, equip.equipPrice, equip.equipProducer, (int)equip.equipCurrency, (int)equip.equipDelType, null, null));
                    }
                    
                    int pos3 = 0;
                    foreach (Equipment equip in list)
                    {
                        row = new string[9];
                        pos3++;
                        row[0] = pos1.ToString() + "." + pos2.ToString() + "." + pos3.ToString();
                        row[1] = equip.equipName;
                        row[2] = equip.equipCode.ToString();
                        row[3] = equip.equipSeller;
                        row[4] = equip.equipProducer;
                        row[5] = equip.equipCount.ToString();
                        row[6] = equip.equipPrice.ToString() + " " + mainForm.selfRef.CurrencyToString(equip.equipCurrency);
                        row[7] = (equip.equipPrice * equip.equipCount).ToString() + " " + mainForm.selfRef.CurrencyToString(equip.equipCurrency);
                        row[8] = equip.equipDelType.ToString();
                        dataGridView1.Rows.Add(row);
                    }
                }

                if (spec.Last() != set)
                    dataGridView1.Rows.Add();
            }

            dataGridView1.Height = dataGridView1.Rows.Count * 24 + 39;
            this.Height = dataGridView1.Height + 90;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.ShowDialog();
        }
    }
}
