using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Specification
{
    public enum Currency // валюта
    {
        Ruble = 0,
        Dollar = 1,
        Euro = 2,
        Other = 3
    }

    public enum deliveryType // вид поставки
    {
        EXW = 0,
        FCA = 1,
        FAS = 2,
        FOB = 3,
        CFR = 4,
        CIF = 5,
        CIP = 6,
        CPT = 7,
        DAT = 8,
        DAP = 9,
        DDP = 10
    }

    public partial class mainForm : Form
    {
        string connStr = "server=localhost;user=root;database=specification;charset=utf8";
        public List<Specification> specList = new List<Specification>();
        List<Entity> entityList = new List<Entity>();
        public List<User> userList = new List<User>();
        List<userGroup> userGroupList = new List<userGroup>();
        SpecTreeNode lastNode = null;
        public static mainForm selfRef = null;
        public bool isOpened = false;
        User activeUser = null;
        bool allowEndGrouping = false;
        int groupingMode = -1;
        public string entityName = "Сущность";

        public mainForm()
        {
            InitializeComponent();
            selfRef = this;

            ImageList myImageList = new ImageList();
            myImageList.Images.Add(Image.FromFile("specification.png"));
            myImageList.Images.Add(Image.FromFile("kts.png"));
            myImageList.Images.Add(Image.FromFile("kizd.png"));
            myImageList.Images.Add(Image.FromFile("item.png"));
            myImageList.Images.Add(Image.FromFile("entity.png"));

            specTree.ImageList = myImageList;
        }

        public void acceptSpecChanges(string text, bool edit)
        {
            if (edit) // переименование спецификации
            {
                specList.Find(s => s == lastNode.nodeSpec).specName = text;
                mySQLExecute("UPDATE specifications SET spec_name = '" + text + "' WHERE spec_id = " + lastNode.nodeSpec.specId);
                lastNode.Text = text;
                forceInfoUpdate(lastNode);
            }
            else // создание новой спецификации
            {
                int maxId = 0;
                List<string> list = mySQLSelect("SELECT spec_id FROM specifications ORDER BY spec_id DESC LIMIT 1");
                if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT spec_id FROM specifications ORDER BY spec_id DESC LIMIT 1")[0]);
                Specification spec = new Specification(maxId + 1, text);
                mySQLExecute("INSERT INTO specifications VALUES (" + (maxId + 1) + ",'" + text + "')");
                specList.Add(spec);
                SpecTreeNode nodeS = new SpecTreeNode(text, spec, null, null, null);
                nodeS.Tag = "Specification";
                specTree.Nodes.Add(nodeS);
            }
        }

        public void acceptSetChanges(string text, bool edit)
        {
            if (edit) // переименование ктс
            {
                specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).setName = text;
                mySQLExecute("UPDATE equipment_sets SET set_name = '" + text + "' WHERE set_id = " + lastNode.nodeES.setId);
                lastNode.Text = text;
                forceInfoUpdate(lastNode);
            }
            else // создание нового ктс
            {
                int maxId = 0;
                List<string> list = mySQLSelect("SELECT set_id FROM equipment_sets ORDER BY set_id DESC LIMIT 1");
                if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT set_id FROM equipment_sets ORDER BY set_id DESC LIMIT 1")[0]);
                equipmentSet set = new equipmentSet(maxId + 1, text);
                mySQLExecute("INSERT INTO equipment_sets VALUES (" + (maxId + 1) + "," + lastNode.nodeSpec.specId + ",'" + text + "')");
                specList.Find(s => s == lastNode.nodeSpec).Add(set);
                SpecTreeNode parentNode = lastNode;
                SpecTreeNode nodeES = new SpecTreeNode(text, lastNode.nodeSpec, set, null, null);
                nodeES.Tag = "EqSet";
                nodeES.ImageIndex = 1;
                nodeES.SelectedImageIndex = 1;
                parentNode.Nodes.Add(nodeES);
                parentNode.Expand();
            }
        }

        public void acceptGroupChanges(string text, bool edit)
        {
            if (edit) // переименование комплектной единицы
            {
                specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == lastNode.nodeEG).groupName = text;
                mySQLExecute("UPDATE equipment_groups SET group_name = '" + text + "' WHERE group_id = " + lastNode.nodeEG.groupId);
                lastNode.Text = text;
                forceInfoUpdate(lastNode);
            }
            else // создание новой комплектной единицы
            {
                int maxId = 0;
                List<string> list = mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1");
                if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1")[0]);
                equipmentGroup group = new equipmentGroup(maxId + 1, text);
                mySQLExecute("INSERT INTO equipment_groups VALUES (" + (maxId + 1) + "," + lastNode.nodeES.setId + ",'" + text + "')");
                specList.Find(s => s == lastNode.nodeSpec).Find(st => st == lastNode.nodeES).Add(group);
                SpecTreeNode parentNode = lastNode;
                SpecTreeNode nodeEG = new SpecTreeNode(text, lastNode.nodeSpec, lastNode.nodeES, null, group);
                nodeEG.Tag = "EqGroup";
                nodeEG.ImageIndex = 2;
                nodeEG.SelectedImageIndex = 2;
                parentNode.Nodes.Add(nodeEG);
                parentNode.Expand();
            }
        }

        public void acceptEquipChanges(string name, int code, string seller, string producer, int count, int price, int currency, int delivery_type, bool edit, bool kts)
        {
            if (edit) // изменение покупного изделия
            {
                Equipment equip = null;
                if (!kts)
                    equip = specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == lastNode.nodeEG).Find(eq => eq == lastNode.nodeEq);
                else
                    equip = specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).setEquip.Find(eq => eq == lastNode.nodeEq);
                equip.equipName = name;
                equip.equipCode = code;
                equip.equipSeller = seller;
                equip.equipProducer = producer;
                equip.equipCount = count;
                equip.equipPrice = price;
                equip.equipCurrency = (Currency)currency;
                equip.equipDelType = (deliveryType)delivery_type;
                mySQLExecute("UPDATE equipment SET equipment_name = '" + name + "', seller = '" + seller + "', code = " + code + ", producer = '" + producer + "', count = " + count + ", price = " + price + ", currency = " + currency + ", delivery_type = " + delivery_type + " WHERE equipment_id = " + lastNode.nodeEq.equipId);
                lastNode.Text = name;
                forceInfoUpdate(lastNode);
            }
            else // создание нового покупного изделия
            {
                int maxId = 0;
                List<string> list = mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1");
                if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1")[0]);
                if (!kts)
                {
                    Equipment equip = new Equipment(maxId + 1, seller, code, name, count, price, producer, currency, delivery_type, null, activeUser);
                    mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + "," + lastNode.nodeEG.groupId + ",'" + seller + "','" + code + "','" + name + "','" + count + "','" + price + "','" + producer + "','" + currency + "','" + delivery_type + "','1','" + activeUser.userId + "',0)");
                    specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == lastNode.nodeEG).Add(equip);
                    SpecTreeNode nodeE = new SpecTreeNode(name, lastNode.nodeSpec, lastNode.nodeES, equip, lastNode.nodeEG);
                    nodeE.Tag = "Equip";
                    nodeE.ImageIndex = 3;
                    nodeE.SelectedImageIndex = 3;
                    SpecTreeNode parentNode = lastNode;
                    parentNode.Nodes.Add(nodeE);
                    parentNode.Expand();
                }
                else
                {
                    Equipment equip = new Equipment(maxId + 1, seller, code, name, count, price, producer, currency, delivery_type, null, activeUser);
                    mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + ",null,'" + seller + "','" + code + "','" + name + "','" + count + "','" + price + "','" + producer + "','" + currency + "','" + delivery_type + "','1','" + activeUser.userId + "','" + lastNode.nodeES.setId + "')");
                    specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).setEquip.Add(equip);
                    SpecTreeNode nodeE = new SpecTreeNode(name, lastNode.nodeSpec, lastNode.nodeES, equip, null);
                    nodeE.Tag = "Equip";
                    nodeE.ImageIndex = 3;
                    nodeE.SelectedImageIndex = 3;
                    SpecTreeNode parentNode = lastNode;
                    parentNode.Nodes.Add(nodeE);
                    parentNode.Expand();
                }
            }
        }

        public void acceptLogin(string username, int userid)
        {
            loginButton.Enabled = false;
            onlyMineCheckBox.Enabled = true;
            groupModeCheckBox.Enabled = true;
            specOpen.Enabled = true;
            catalogButton.Enabled = true;
            welcomeLabel.Text = "Добро пожаловать, " + username + "!";

            // пользователи
            List<string> list = mySQLSelect("SELECT user_id, user_name, user_level FROM users");
            for (int i = 1; i <= list.Count; i++)
                if (i % 3 == 0)
                    userList.Add(new User(Convert.ToInt32(list[i - 3]), list[i - 2], Convert.ToInt32(list[i - 1])));

            activeUser = userList.Find(u => u.userId == userid);

            // пользовательские группы
            list = mySQLSelect("SELECT * FROM user_groups");
            userGroup ug = null;
            int groupId = 0;
            for (int i = 1; i <= list.Count; i++)
            {
                if (i % 2 == 0)
                {
                    if (groupId != Convert.ToInt32(list[i - 2]))
                    {
                        groupId = Convert.ToInt32(list[i - 2]);
                        ug = new userGroup(groupId);
                        ug.Add(userList.Find(u => u.userId == Convert.ToInt32(list[i - 1])));
                        userGroupList.Add(ug);
                    }
                    else
                        ug.Add(userList.Find(u => u.userId == Convert.ToInt32(list[i - 1])));
                }
            }
        }

        public List<string> mySQLSelect(string sql)
        {
            List<string> list = new List<string>();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                for (int i = 0; i < reader.FieldCount; i++)
                    list.Add(reader[i].ToString());

            reader.Close();
            conn.Close();

            return list;
        }

        public void mySQLExecute(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }

        public List<TreeNode> findCheckedNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    list.Add(node);
                if (node.Nodes.Count > 0)
                    list.AddRange(findCheckedNodes(node.Nodes));
            }
            return list;
        }

        public void updateTree()
        {
            List<string> list = new List<string>();
            specList.Clear();
            specTree.Nodes.Clear();
            infoBox.Items.Clear();

            // спецификации
            list = mySQLSelect("SELECT * FROM specifications");
            for (int i = 1; i <= list.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Specification spec = new Specification(Convert.ToInt32(list[i - 2]), list[i - 1]);
                    specList.Add(spec);
                    SpecTreeNode nodeS = new SpecTreeNode(list[i - 1], spec, null, null, null);
                    nodeS.Tag = "Specification";
                    nodeS.ImageIndex = 0;
                    specTree.Nodes.Add(nodeS);

                    // ктс
                    List<string> list1 = mySQLSelect("SELECT * FROM equipment_sets WHERE spec_id = " + spec.specId);
                    for (int j = 1; j <= list1.Count; j++)
                    {
                        if (j % 3 == 0)
                        {
                            equipmentSet eqSet = new equipmentSet(Convert.ToInt32(list1[j - 3]), list1[j - 1]);
                            spec.Add(eqSet);
                            SpecTreeNode parentNode = nodeS;
                            SpecTreeNode nodeES = new SpecTreeNode(list1[j - 1], spec, eqSet, null, null);
                            nodeES.Tag = "EqSet";
                            nodeES.ImageIndex = 1;
                            nodeES.SelectedImageIndex = 1;
                            parentNode.Nodes.Add(nodeES);

                            // покупное изделие
                            List<string> list4 = mySQLSelect("SELECT * FROM equipment WHERE set_id = " + eqSet.setId);
                            for (int u = 1; u <= list4.Count; u++)
                            {
                                if (u % 13 == 0)
                                {
                                    int entityId = -1;
                                    if (list4[u - 3] != "") entityId = Convert.ToInt32(list4[u - 3]);
                                    Equipment equip = new Equipment(Convert.ToInt32(list4[u - 13]), list4[u - 11], Convert.ToInt32(list4[u - 10]), list4[u - 9], Convert.ToInt32(list4[u - 8]), Convert.ToInt32(list4[u - 7]), list4[u - 6], Convert.ToInt32(list4[u - 5]), Convert.ToInt32(list4[u - 4]), null, userList.Find(user => user.userId == Convert.ToInt32(list4[u - 2])));
                                    if (entityId > 0)
                                        equip = new Equipment(Convert.ToInt32(list4[u - 13]), list4[u - 11], Convert.ToInt32(list4[u - 10]), list4[u - 9], Convert.ToInt32(list4[u - 8]), Convert.ToInt32(list4[u - 7]), list4[u - 6], Convert.ToInt32(list4[u - 5]), Convert.ToInt32(list4[u - 4]), entityList.Find(entity => entity.entityId == entityId), userList.Find(user => user.userId == Convert.ToInt32(list4[u - 2])));
                                    eqSet.setEquip.Add(equip);
                                    SpecTreeNode nodeE = new SpecTreeNode(list4[u - 9], spec, eqSet, equip, null);
                                    nodeE.Tag = "Equip";
                                    nodeE.ImageIndex = 3;
                                    nodeE.SelectedImageIndex = 3;
                                    parentNode = nodeES;
                                    if (onlyMineCheckBox.Checked) // режим "только мое"
                                    {
                                        bool inGroup = false;
                                        User createdBy = userList.Find(user => user.userId == Convert.ToInt32(list4[u - 2]));
                                        if (createdBy != null)
                                        {
                                            List<userGroup> usrgrp = userGroupList.FindAll(ug => ug.Contains(activeUser)); // список групп, в которые входит пользователь
                                            foreach (userGroup usrg in usrgrp)
                                                if (usrg.Contains(createdBy)) // если хотя бы в одной из групп пользователя есть тот, кто добавил позицию
                                                {
                                                    inGroup = true;
                                                    break;
                                                }
                                        }

                                        if (createdBy == activeUser || inGroup)
                                            parentNode.Nodes.Add(nodeE);
                                    }
                                    else
                                        parentNode.Nodes.Add(nodeE);
                                }
                            }

                            // комплектная единица
                            List<string> list3 = mySQLSelect("SELECT * FROM equipment_groups WHERE set_id = " + eqSet.setId);
                            for (int t = 1; t <= list3.Count; t++)
                            {
                                if (t % 3 == 0)
                                {
                                    equipmentGroup eqGroup = new equipmentGroup(Convert.ToInt32(list3[t - 3]), list3[t - 1]);
                                    eqSet.Add(eqGroup);
                                    parentNode = nodeES;
                                    SpecTreeNode nodeEG = new SpecTreeNode(list3[t - 1], spec, eqSet, null, eqGroup);
                                    nodeEG.Tag = "EqGroup";
                                    nodeEG.ImageIndex = 2;
                                    nodeEG.SelectedImageIndex = 2;
                                    parentNode.Nodes.Add(nodeEG);

                                    // покупное изделие
                                    List<string> list2 = mySQLSelect("SELECT * FROM equipment WHERE group_id = " + eqGroup.groupId);
                                    for (int k = 1; k <= list2.Count; k++)
                                    {
                                        if (k % 13 == 0)
                                        {
                                            int entityId = -1;
                                            if (list2[k - 3] != "") entityId = Convert.ToInt32(list2[k - 3]);
                                            Equipment equip = new Equipment(Convert.ToInt32(list2[k - 13]), list2[k - 11], Convert.ToInt32(list2[k - 10]), list2[k - 9], Convert.ToInt32(list2[k - 8]), Convert.ToInt32(list2[k - 7]), list2[k - 6], Convert.ToInt32(list2[k - 5]), Convert.ToInt32(list2[k - 4]), null, userList.Find(user => user.userId == Convert.ToInt32(list2[k - 2])));
                                            if (entityId > 0)
                                                equip = new Equipment(Convert.ToInt32(list2[k - 13]), list2[k - 11], Convert.ToInt32(list2[k - 10]), list2[k - 9], Convert.ToInt32(list2[k - 8]), Convert.ToInt32(list2[k - 7]), list2[k - 6], Convert.ToInt32(list2[k - 5]), Convert.ToInt32(list2[k - 4]), entityList.Find(entity => entity.entityId == entityId), userList.Find(user => user.userId == Convert.ToInt32(list2[k - 2])));
                                            eqGroup.Add(equip);
                                            SpecTreeNode nodeE = new SpecTreeNode(list2[k - 9], spec, eqSet, equip, eqGroup);
                                            nodeE.Tag = "Equip";
                                            nodeE.ImageIndex = 3;
                                            nodeE.SelectedImageIndex = 3;
                                            parentNode = nodeEG;
                                            if (onlyMineCheckBox.Checked) // режим "только мое"
                                            {
                                                bool inGroup = false;
                                                User createdBy = userList.Find(user => user.userId == Convert.ToInt32(list2[k - 2]));
                                                if (createdBy != null)
                                                {
                                                    List<userGroup> usrgrp = userGroupList.FindAll(ug => ug.Contains(activeUser)); // список групп, в которые входит пользователь
                                                    foreach (userGroup usrg in usrgrp)
                                                        if (usrg.Contains(createdBy)) // если хотя бы в одной из групп пользователя есть тот, кто добавил позицию
                                                        {
                                                            inGroup = true;
                                                            break;
                                                        }
                                                }

                                                if (createdBy == activeUser || inGroup)
                                                    parentNode.Nodes.Add(nodeE);
                                            }
                                            else
                                                parentNode.Nodes.Add(nodeE);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string CurrencyToString(Currency currency)
        {
            string currencyName = "";
            switch (currency)
            {
                case Currency.Dollar:
                    currencyName = "долл.";
                    break;
                case Currency.Euro:
                    currencyName = "евро";
                    break;
                case Currency.Ruble:
                    currencyName = "руб.";
                    break;
                case Currency.Other:
                    currencyName = "у.е.";
                    break;
            }
            return currencyName;
        }

        private void specOpen_Click(object sender, EventArgs e)
        {
            specOpen.Enabled = false;
            isOpened = true;
            groupButton.Enabled = true;

            // обновление дерева
            updateTree();
        }

        private void specTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
                MessageBox.Show(index.ToString());
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            SpecTreeNode node = (SpecTreeNode)specTree.GetNodeAt(e.X, e.Y);
            specTree.SelectedNode = node;

            if (node == null)
            {
                infoBox.Items.Clear();
                if (isOpened && activeUser.userLevel == 3) addSpecMenu.Show(specTree, new Point(e.X, e.Y));
                return;
            }

            node.Expand();

            if (node.Tag as string == "Specification")
            {
                lastNode = node;
                if (activeUser.userLevel >= 2) specMenu.Show(specTree, new Point(e.X, e.Y));
            }
            else if (node.Tag as string == "EqSet")
            {
                lastNode = node;
                if (activeUser.userLevel >= 2) eqSetMenu.Show(specTree, new Point(e.X, e.Y));
            }
            else if (node.Tag as string == "EqGroup")
            {
                lastNode = node;
                if (activeUser.userLevel >= 2) eqGroupMenu.Show(specTree, new Point(e.X, e.Y));
            }
            else if (node.Tag as string == "Equip")
            {
                lastNode = node;
                equipMenu.Show(specTree, new Point(e.X, e.Y));
            }
            else if (node.Tag as string == "Entity")
            {
                lastNode = node;
                entityMenu.Show(specTree, new Point(e.X, e.Y));
            }
        }

        private void specDeleteMenu_Click(object sender, EventArgs e) // удаление спецификации
        {
            if (activeUser.userLevel >= 2)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту спецификацию?", "Мастер спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<string> list = mySQLSelect("SELECT equipment_id FROM equipment WHERE set_id IN (SELECT set_id FROM equipment_sets WHERE spec_id = " + lastNode.nodeSpec.specId + ")");
                    if (list.Count > 0)
                        for (int i = 0; i < list.Count; i++)
                            mySQLExecute("DELETE FROM equipment WHERE equipment_id = " + list[i]);

                    specTree.Nodes.Remove(lastNode); // удаление спецификации из дерева
                    specList.Remove(lastNode.nodeSpec); // удаление спецификации из списка спецификаций
                    mySQLExecute("DELETE FROM specifications WHERE spec_id = " + lastNode.nodeSpec.specId); // удаление спецификации из БД
                }
            }
            else
                MessageBox.Show("У вас недостаточно прав для выполнения данной операции!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void eqSetDelete_Click(object sender, EventArgs e) // удаление ктс
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот комплекс?", "Мастер спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                specTree.Nodes.Remove(lastNode); // удаление ктс из дерева
                specList.Find(s => s.specId == lastNode.nodeSpec.specId).Remove(lastNode.nodeES); // удаление ктс из спецификации
                mySQLExecute("DELETE FROM equipment_sets WHERE spec_id = " + lastNode.nodeSpec.specId + " AND set_id = " + lastNode.nodeES.setId); // удаление ктс из БД

                List<string> list = mySQLSelect("SELECT equipment_id FROM equipment WHERE set_id = " + lastNode.nodeES.setId);
                if (list.Count > 0)
                    for (int i = 0; i < list.Count; i++)
                        mySQLExecute("DELETE FROM equipment WHERE equipment_id = " + list[i]);
            }
        }

        private void добавитьКомплектнуюЕдиницуToolStripMenuItem_Click(object sender, EventArgs e) // добавление ктс
        {
            setForm sf = new setForm("", false);
            sf.ShowDialog();
        }

        private void добавитьПокупноеИзделиеToolStripMenuItem_Click(object sender, EventArgs e) // добавление комплектной единицы
        {
            groupForm gf = new groupForm("", false);
            gf.ShowDialog();
        }

        private void onlyMineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isOpened)
            {
                // обновление дерева
                updateTree();
            }
        }

        private void удалитьПокупноеИзделиеToolStripMenuItem_Click(object sender, EventArgs e) // удаление покупного изделия
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить это покупное изделие?", "Мастер спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool kts = false;
                if (lastNode.nodeEG == null) kts = true; else kts = false;
                specTree.Nodes.Remove(lastNode); // удаление покупного изделия из дерева
                if (!kts)
                {
                    specList.Find(s => s.specId == lastNode.nodeSpec.specId).Find(es => es.setId == lastNode.nodeES.setId).Find(eg => eg.groupId == lastNode.nodeEG.groupId).Remove(lastNode.nodeEq); // удаление покупного изделия из спецификации
                    mySQLExecute("DELETE FROM equipment WHERE equipment_id = " + lastNode.nodeEq.equipId + " AND group_id = " + lastNode.nodeEG.groupId); // удаление покупного изделия из БД
                }
                else
                {
                    specList.Find(s => s.specId == lastNode.nodeSpec.specId).Find(es => es.setId == lastNode.nodeES.setId).setEquip.Remove(lastNode.nodeEq); // удаление покупного изделия из спецификации
                    mySQLExecute("DELETE FROM equipment WHERE equipment_id = " + lastNode.nodeEq.equipId + " AND set_id = " + lastNode.nodeES.setId); // удаление покупного изделия из БД
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SpecTreeNode node = (SpecTreeNode)e.Node;
            forceInfoUpdate(node);
        }

        public void forceInfoUpdate(SpecTreeNode node)
        {
            if (node.nodeEq == null && node.nodeES == null && node.nodeEG == null && node.nodeSpec != null)
            {
                // спецификация
                infoBox.Items.Clear();
                infoBox.Items.Add("Имя: " + node.nodeSpec.specName);
                infoBox.Items.Add("Тип: спецификация");
                int totalPriceRub = 0, totalPriceDollar = 0, totalPriceEuro = 0, totalPriceOther = 0;
                foreach (equipmentSet es in node.nodeSpec)
                {
                    if (es.setEquip.Count > 0) // если есть покупные изделия в составе ктс
                        foreach (Equipment eq in es.setEquip)
                        {
                            if (eq.equipCurrency == Currency.Ruble)
                                totalPriceRub += eq.equipCount * eq.equipPrice;
                            else if (eq.equipCurrency == Currency.Dollar)
                                totalPriceDollar += eq.equipCount * eq.equipPrice;
                            else if (eq.equipCurrency == Currency.Euro)
                                totalPriceEuro += eq.equipCount * eq.equipPrice;
                            else
                                totalPriceOther += eq.equipCount * eq.equipPrice;
                        }
                    foreach (equipmentGroup eg in es)
                        foreach (Equipment eq in eg)
                        {
                            if (eq.equipCurrency == Currency.Ruble)
                                totalPriceRub += eq.equipCount * eq.equipPrice;
                            else if (eq.equipCurrency == Currency.Dollar)
                                totalPriceDollar += eq.equipCount * eq.equipPrice;
                            else if (eq.equipCurrency == Currency.Euro)
                                totalPriceEuro += eq.equipCount * eq.equipPrice;
                            else
                                totalPriceOther += eq.equipCount * eq.equipPrice;
                        }
                }
                infoBox.Items.Add("Стоимость:");
                infoBox.Items.Add("        - " + totalPriceRub + " руб.");
                infoBox.Items.Add("        - " + totalPriceDollar + " долл.");
                infoBox.Items.Add("        - " + totalPriceEuro + " евро");
                infoBox.Items.Add("        - " + totalPriceOther + " у.е.");
            }
            else if (node.nodeEq == null && node.nodeEG == null && node.nodeSpec != null)
            {
                // ктс
                infoBox.Items.Clear();
                infoBox.Items.Add("Имя: " + node.nodeES.setName);
                infoBox.Items.Add("Тип: комплекс технических средств");
                int totalPriceRub = 0, totalPriceDollar = 0, totalPriceEuro = 0, totalPriceOther = 0;
                if (node.nodeES.setEquip.Count > 0) // если есть покупные изделия в составе ктс
                    foreach (Equipment eq in node.nodeES.setEquip)
                    {
                        if (eq.equipCurrency == Currency.Ruble)
                            totalPriceRub += eq.equipCount * eq.equipPrice;
                        else if (eq.equipCurrency == Currency.Dollar)
                            totalPriceDollar += eq.equipCount * eq.equipPrice;
                        else if (eq.equipCurrency == Currency.Euro)
                            totalPriceEuro += eq.equipCount * eq.equipPrice;
                        else
                            totalPriceOther += eq.equipCount * eq.equipPrice;
                    }
                foreach (equipmentGroup eg in node.nodeES)
                    foreach (Equipment eq in eg)
                    {
                        if (eq.equipCurrency == Currency.Ruble)
                            totalPriceRub += eq.equipCount * eq.equipPrice;
                        else if (eq.equipCurrency == Currency.Dollar)
                            totalPriceDollar += eq.equipCount * eq.equipPrice;
                        else if (eq.equipCurrency == Currency.Euro)
                            totalPriceEuro += eq.equipCount * eq.equipPrice;
                        else
                            totalPriceOther += eq.equipCount * eq.equipPrice;
                    }
                infoBox.Items.Add("Стоимость:");
                infoBox.Items.Add("        - " + totalPriceRub + " руб.");
                infoBox.Items.Add("        - " + totalPriceDollar + " долл.");
                infoBox.Items.Add("        - " + totalPriceEuro + " евро");
                infoBox.Items.Add("        - " + totalPriceOther + " у.е.");
            }
            else if (node.nodeEq == null && node.nodeSpec != null)
            {
                // комплектная единица
                infoBox.Items.Clear();
                infoBox.Items.Add("Имя: " + node.nodeEG.groupName);
                infoBox.Items.Add("Тип: комплектная единица");
            }
            else if (node.nodeSpec != null)
            {
                // покупное изделие
                infoBox.Items.Clear();
                infoBox.Items.Add("Имя: " + node.nodeEq.equipName);
                infoBox.Items.Add("Тип: покупное изделие");
                infoBox.Items.Add("Код в каталоге: " + node.nodeEq.equipCode);
                infoBox.Items.Add("Поставщик: " + node.nodeEq.equipSeller);
                infoBox.Items.Add("Производитель: " + node.nodeEq.equipProducer);
                infoBox.Items.Add("Количество: " + node.nodeEq.equipCount);
                infoBox.Items.Add("Цена: " + node.nodeEq.equipPrice + " " + CurrencyToString(node.nodeEq.equipCurrency));
                if (node.nodeEq.equipCount > 1)
                    infoBox.Items.Add("Стоимость: " + (node.nodeEq.equipPrice * node.nodeEq.equipCount) + " " + CurrencyToString(node.nodeEq.equipCurrency));
                infoBox.Items.Add("Вид поставки: " + node.nodeEq.equipDelType);
                infoBox.Items.Add("Добавил: " + node.nodeEq.equipCreatedBy.userName);
            }
            else
            {
                // сущность
                infoBox.Items.Clear();
                infoBox.Items.Add("Имя: " + node.Text);
                infoBox.Items.Add("Тип: сущность");
            }
        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e) // переименовать спецификацию
        {
            if (activeUser.userLevel >= 2)
            {
                specForm sf = new specForm(lastNode.Text, true);
                sf.ShowDialog();
            }
            else
                MessageBox.Show("У вас недостаточно прав для выполнения данной операции!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void переименоватьToolStripMenuItem1_Click(object sender, EventArgs e) // переименовать ктс
        {
            setForm sf = new setForm(lastNode.Text, true);
            sf.ShowDialog();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e) // редактировать покупное изделие
        {
            Equipment equip = lastNode.nodeEq;
            bool kts = false;
            if (lastNode.nodeEG == null) kts = true; else kts = false;
            equipForm ef = new equipForm(equip.equipName, equip.equipCode, equip.equipSeller, equip.equipProducer, equip.equipCount, equip.equipPrice, (int)equip.equipCurrency, (int)equip.equipDelType, true, kts);
            ef.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginForm lf = new loginForm();
            lf.ShowDialog();
        }

        private void новаяСпецификацияToolStripMenuItem_Click(object sender, EventArgs e) // создать новую спецификацию
        {
            specForm sf = new specForm("", false);
            sf.ShowDialog();
        }

        private void добавитьПокупноеИзделиеToolStripMenuItem1_Click(object sender, EventArgs e) // добавить покупное изделие
        {
            equipForm ef = new equipForm("", 0, "", "", 0, 0, 0, 0, false, false);
            ef.ShowDialog();
        }

        private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e) // переименовать комплектную единицу
        {
            groupForm gf = new groupForm(lastNode.Text, true);
            gf.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) // удалить комплектную единицу
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту комплектную единицу?", "Мастер спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                specTree.Nodes.Remove(lastNode); // удаление ктс из дерева
                specList.Find(s => s.specId == lastNode.nodeSpec.specId).Find(es => es.setId == lastNode.nodeES.setId).Remove(lastNode.nodeEG); // удаление ктс из спецификации
                mySQLExecute("DELETE FROM equipment_groups WHERE group_id = " + lastNode.nodeEG.groupId + " AND set_id = " + lastNode.nodeES.setId); // удаление ктс из БД
            }
        }

        private void закрепитьКТСЗаПользователемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeUser.userLevel >= 2)
            {
                pinForm pf = new pinForm();
                pf.ShowDialog();
            }
            else
                MessageBox.Show("У вас недостаточно прав для выполнения данной операции!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeUser.userLevel >= 2)
            {
                pinForm pf = new pinForm();
                pf.ShowDialog();
            }
            else
                MessageBox.Show("У вас недостаточно прав для выполнения данной операции!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void добавитьПокупноеИзделиеToolStripMenuItem2_Click(object sender, EventArgs e) // добавить покупное изделие
        {
            equipForm ef = new equipForm("", 0, "", "", 0, 0, 0, 0, false, true);
            ef.ShowDialog();
        }

        private void копироватьКТСToolStripMenuItem_Click(object sender, EventArgs e) // копирование КТС
        {
            int maxId = 0;
            List<string> list = mySQLSelect("SELECT set_id FROM equipment_sets ORDER BY set_id DESC LIMIT 1");
            if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT set_id FROM equipment_sets ORDER BY set_id DESC LIMIT 1")[0]);
            equipmentSet set = lastNode.nodeES;
            set.setId = maxId + 1;
            mySQLExecute("INSERT INTO equipment_sets VALUES (" + (maxId + 1) + "," + lastNode.nodeSpec.specId + ",'" + lastNode.Text + "')");
            specList.Find(s => s == lastNode.nodeSpec).Add(set);
            SpecTreeNode parentNode = (SpecTreeNode)lastNode.Parent;
            SpecTreeNode nodeES = new SpecTreeNode(lastNode.Text, lastNode.nodeSpec, set, null, null);
            nodeES.Tag = "EqSet";
            nodeES.ImageIndex = 1;
            nodeES.SelectedImageIndex = 1;
            parentNode.Nodes.Add(nodeES);
            parentNode.Expand();
            
            if (lastNode.nodeES.setEquip.Count > 0) // если есть покупные изделия, прикрепленные к этому КТС
            {
                List<Equipment> leq = new List<Equipment>();
                foreach (Equipment equ in lastNode.nodeES.setEquip)
                {
                    list = mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1");
                    if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1")[0]);
                    Equipment equip = equ;
                    equip.equipId = maxId + 1;
                    mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + ",null,'" + equip.equipSeller + "','" + equip.equipCode + "','" + equip.equipName + "','" + equip.equipCount + "','" + equip.equipPrice + "','" + equip.equipProducer + "','" + (int)equip.equipCurrency + "','" + (int)equip.equipDelType + "','1','" + activeUser.userId + "','" + lastNode.nodeES.setId + "')");
                    //specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).setEquip.Add(equip);
                    leq.Add(equip);
                    SpecTreeNode nodeE = new SpecTreeNode(equip.equipName, lastNode.nodeSpec, lastNode.nodeES, equip, null);
                    nodeE.Tag = "Equip";
                    nodeE.ImageIndex = 3;
                    nodeE.SelectedImageIndex = 3;
                    parentNode = nodeES;
                    parentNode.Nodes.Add(nodeE);
                    parentNode.Expand();
                }
                lastNode.nodeES.setEquip = leq;
            }
            if (lastNode.nodeES.Count > 0) // если есть комплектные единицы, прикрепленные к этому КТС
            {
                equipmentSet eqst = new equipmentSet(0, "");
                foreach (equipmentGroup equipg in lastNode.nodeES)
                {
                    maxId = 0;
                    list = mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1");
                    if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1")[0]);
                    equipmentGroup group = equipg;
                    group.groupId = maxId + 1;
                    mySQLExecute("INSERT INTO equipment_groups VALUES (" + (maxId + 1) + "," + lastNode.nodeES.setId + ",'" + equipg.groupName + "')");
                    //specList.Find(s => s == lastNode.nodeSpec).Find(st => st == lastNode.nodeES).Add(group);
                    eqst.Add(group);
                    parentNode = nodeES;
                    SpecTreeNode nodeEG = new SpecTreeNode(equipg.groupName, lastNode.nodeSpec, lastNode.nodeES, null, group);
                    nodeEG.Tag = "EqGroup";
                    nodeEG.ImageIndex = 2;
                    nodeEG.SelectedImageIndex = 2;
                    parentNode.Nodes.Add(nodeEG);
                    parentNode.Expand();

                    if (equipg.Count > 0) // если есть покупные изделия, прикрепленные к этой комплектной единице
                    {
                        equipmentGroup egg = new equipmentGroup(0, "");
                        foreach (Equipment equ in equipg)
                        {
                            maxId = 0;
                            list = mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1");
                            if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1")[0]);
                            Equipment equip = equ;
                            equip.equipId = maxId + 1;
                            mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + "," + equipg.groupId + ",'" + equip.equipSeller + "','" + equip.equipCode + "','" + equip.equipName + "','" + equip.equipCount + "','" + equip.equipPrice + "','" + equip.equipProducer + "','" + (int)equip.equipCurrency + "','" + (int)equip.equipDelType + "','1','" + activeUser.userId + "',0)");
                            egg.Add(equip);
                        }
                        foreach (Equipment equ in egg)
                        {
                            specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == equipg).Add(equ);
                            SpecTreeNode nodeE = new SpecTreeNode(equ.equipName, lastNode.nodeSpec, lastNode.nodeES, equ, lastNode.nodeEG);
                            nodeE.Tag = "Equip";
                            nodeE.ImageIndex = 3;
                            nodeE.SelectedImageIndex = 3;
                            parentNode = nodeEG;
                            parentNode.Nodes.Add(nodeE);
                            parentNode.Expand();
                        }
                    }
                }
                foreach(equipmentGroup egrp in eqst)
                    specList.Find(s => s == lastNode.nodeSpec).Find(st => st == lastNode.nodeES).Add(egrp);
            }
        }

        private void копироватьИзделиеToolStripMenuItem_Click(object sender, EventArgs e) // копирование покупного изделия
        {
            bool kts = false;
            if (lastNode.nodeEG == null) kts = true; else kts = false;
            int maxId = 0;
            List<string> list = mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1");
            if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1")[0]);
            if (!kts)
            {
                Equipment equip = lastNode.nodeEq;
                equip.equipId = maxId + 1;
                mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + "," + lastNode.nodeEG.groupId + ",'" + equip.equipSeller + "','" + equip.equipCode + "','" + equip.equipName + "','" + equip.equipCount + "','" + equip.equipPrice + "','" + equip.equipProducer + "','" + (int)equip.equipCurrency + "','" + (int)equip.equipDelType + "','1','" + activeUser.userId + "',0)");
                specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == lastNode.nodeEG).Add(equip);
                SpecTreeNode nodeE = new SpecTreeNode(equip.equipName, lastNode.nodeSpec, lastNode.nodeES, equip, lastNode.nodeEG);
                nodeE.Tag = "Equip";
                nodeE.ImageIndex = 3;
                nodeE.SelectedImageIndex = 3;
                SpecTreeNode parentNode = (SpecTreeNode)lastNode.Parent;
                parentNode.Nodes.Add(nodeE);
                parentNode.Expand();
            }
            else
            {
                Equipment equip = lastNode.nodeEq;
                equip.equipId = maxId + 1;
                mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + ",null,'" + equip.equipSeller + "','" + equip.equipCode + "','" + equip.equipName + "','" + equip.equipCount + "','" + equip.equipPrice + "','" + equip.equipProducer + "','" + (int)equip.equipCurrency + "','" + (int)equip.equipDelType + "','1','" + activeUser.userId + "','" + lastNode.nodeES.setId + "')");
                specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).setEquip.Add(equip);
                SpecTreeNode nodeE = new SpecTreeNode(equip.equipName, lastNode.nodeSpec, lastNode.nodeES, equip, null);
                nodeE.Tag = "Equip";
                nodeE.ImageIndex = 3;
                nodeE.SelectedImageIndex = 3;
                SpecTreeNode parentNode = (SpecTreeNode)lastNode.Parent;
                parentNode.Nodes.Add(nodeE);
                parentNode.Expand();
            }
        }

        private void копироватьКомплЕдиницуToolStripMenuItem_Click(object sender, EventArgs e) // копирование комплектной единицы
        {
            if (lastNode.nodeEG.Count >= 0) // если есть покупные изделия, прикрепленные к этой комплектной единице
            {
                int maxId = 0;
                List<string> list = mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1");
                if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT group_id FROM equipment_groups ORDER BY group_id DESC LIMIT 1")[0]);
                equipmentGroup group = lastNode.nodeEG;
                group.groupId = maxId + 1;
                mySQLExecute("INSERT INTO equipment_groups VALUES (" + (maxId + 1) + "," + lastNode.nodeES.setId + ",'" + lastNode.nodeEG.groupName + "')");
                specList.Find(s => s == lastNode.nodeSpec).Find(st => st == lastNode.nodeES).Add(group);
                SpecTreeNode parentNode = (SpecTreeNode)lastNode.Parent;
                SpecTreeNode nodeEG = new SpecTreeNode(lastNode.Text, lastNode.nodeSpec, lastNode.nodeES, null, group);
                nodeEG.Tag = "EqGroup";
                nodeEG.ImageIndex = 2;
                nodeEG.SelectedImageIndex = 2;
                parentNode.Nodes.Add(nodeEG);
                parentNode.Expand();

                equipmentGroup egg = new equipmentGroup(0, "");
                foreach (Equipment equ in lastNode.nodeEG)
                {
                    maxId = 0;
                    list = mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1");
                    if (list.Count > 0) maxId = Convert.ToInt32(mySQLSelect("SELECT equipment_id FROM equipment ORDER BY equipment_id DESC LIMIT 1")[0]);
                    Equipment equip = equ;
                    equip.equipId = maxId + 1;
                    mySQLExecute("INSERT INTO equipment VALUES (" + (maxId + 1) + "," + lastNode.nodeEG.groupId + ",'" + equip.equipSeller + "','" + equip.equipCode + "','" + equip.equipName + "','" + equip.equipCount + "','" + equip.equipPrice + "','" + equip.equipProducer + "','" + (int)equip.equipCurrency + "','" + (int)equip.equipDelType + "','1','" + activeUser.userId + "',0)");
                    egg.Add(equip);
                    SpecTreeNode nodeE = new SpecTreeNode(equip.equipName, lastNode.nodeSpec, lastNode.nodeES, equip, lastNode.nodeEG);
                    nodeE.Tag = "Equip";
                    nodeE.ImageIndex = 3;
                    nodeE.SelectedImageIndex = 3;
                    parentNode = nodeEG;
                    parentNode.Nodes.Add(nodeE);
                    parentNode.Expand();
                }
                foreach (Equipment equ in egg)
                    specList.Find(s => s == lastNode.nodeSpec).Find(es => es == lastNode.nodeES).Find(eg => eg == lastNode.nodeEG).Add(equ);
            }
        }

        private void catalogButton_Click(object sender, EventArgs e) // заполнение справочников
        {
            catalogForm cf = new catalogForm();
            cf.ShowDialog();
        }

        private void просмотрЗакупочнойВедомостиToolStripMenuItem_Click(object sender, EventArgs e) // просмотр закупочной ведомости
        {
            sheetForm sf = new sheetForm(lastNode.nodeSpec);
            sf.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateTree();
        }

        private void groupButton_Click(object sender, EventArgs e)
        {
            if (allowEndGrouping)
            {
                if (MessageBox.Show("Отменить группировку?", "Мастер спецификаций", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    specTree.CheckBoxes = false;
                    allowEndGrouping = false;
                    groupingMode = -1;
                }
                else if (isAllowGrouping())
                {
                    if (groupingMode == 0)
                    {
                        List<TreeNode> nodes = findCheckedNodes(specTree.Nodes);
                        if (nodes.Count > 0)
                        {
                            SpecTreeNode firstNode = (SpecTreeNode)nodes[0];
                            TreeNode parentNode = firstNode.Parent;
                            int id = Convert.ToInt32(mySQLSelect("SELECT set_id FROM equipment WHERE equipment_id = " + firstNode.nodeEq.equipId)[0]);
                            for (int i = 1; i < nodes.Count; i++)
                            {
                                SpecTreeNode node = (SpecTreeNode)nodes[i];
                                node.Parent.Nodes.Remove(node);
                                parentNode.Nodes.Add(node);
                                mySQLExecute("UPDATE equipment SET set_id = " + id + " WHERE equipment_id = " + node.nodeEq.equipId);
                            }
                        }
                    }
                    else if (groupingMode == 1)
                    {
                        List<TreeNode> nodes = findCheckedNodes(specTree.Nodes);
                        if (nodes.Count > 0)
                        {
                            SpecTreeNode firstNode = (SpecTreeNode)nodes[0];
                            TreeNode parentNode = firstNode.Parent;
                            int id = Convert.ToInt32(mySQLSelect("SELECT group_id FROM equipment WHERE equipment_id = " + firstNode.nodeEq.equipId)[0]);
                            for (int i = 1; i < nodes.Count; i++)
                            {
                                SpecTreeNode node = (SpecTreeNode)nodes[i];
                                node.Parent.Nodes.Remove(node);
                                parentNode.Nodes.Add(node);
                                mySQLExecute("UPDATE equipment SET group_id = " + id + " WHERE equipment_id = " + node.nodeEq.equipId);
                            }
                        }
                    }
                    else if (groupingMode == 2)
                    {
                        List<TreeNode> nodes = findCheckedNodes(specTree.Nodes);
                        if (nodes.Count > 0)
                        {
                            SpecTreeNode firstNode = (SpecTreeNode)nodes[0];
                            SpecTreeNode parentNode = (SpecTreeNode)firstNode.Parent;
                            entityForm ef = new entityForm("");
                            ef.ShowDialog();
                            SpecTreeNode entityNode = new SpecTreeNode(entityName, null, null, null, null);
                            entityNode.Tag = "Entity";
                            entityNode.ImageIndex = 4;
                            entityNode.SelectedImageIndex = 4;
                            for (int i = 0; i < nodes.Count; i++)
                            {
                                SpecTreeNode node = (SpecTreeNode)nodes[i];
                                parentNode.Nodes.Remove(node);
                                entityNode.Nodes.Add(node);
                            }
                            parentNode.Nodes.Add(entityNode);
                        }
                    }
                    specTree.CheckBoxes = false;
                    allowEndGrouping = false;
                    groupingMode = -1;
                }
                else
                    MessageBox.Show("Ошибка выбора изделий для группировки!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                groupingForm gf = new groupingForm();
                gf.ShowDialog();
            }
        }

        public void handleGrouping(int mode)
        {
            specTree.CheckBoxes = true;
            groupingMode = mode;
        }

        public bool isAllowGrouping()
        {
            List<TreeNode> nodes = findCheckedNodes(specTree.Nodes);
            if (nodes.Count > 0)
            {
                int count1 = 1, count2 = 1, count3 = 1, count4 = 1;
                SpecTreeNode prevNode = (SpecTreeNode)nodes[0];
                for (int i = 1; i < nodes.Count; i++)
                {
                    SpecTreeNode currNode = (SpecTreeNode)nodes[i];
                    if (currNode.Level == prevNode.Level && currNode.nodeSpec == prevNode.nodeSpec && currNode.Tag == prevNode.Tag && currNode.Level == 2 && currNode.Tag.ToString() == "Equip" && currNode.Parent.Text == prevNode.Parent.Text && groupingMode == 0)
                        count1++;
                    else if (currNode.Level == prevNode.Level && currNode.nodeSpec == prevNode.nodeSpec && currNode.nodeES == prevNode.nodeES && currNode.Tag == prevNode.Tag && currNode.Level == 3 && currNode.Tag.ToString() == "Equip" && currNode.Parent.Text == prevNode.Parent.Text && groupingMode == 1)
                        count2++;
                    else if (currNode.Level == prevNode.Level && currNode.nodeSpec == prevNode.nodeSpec && currNode.nodeES == prevNode.nodeES && currNode.Tag == prevNode.Tag && currNode.Level == 3 && currNode.Tag.ToString() == "Equip" && currNode.Parent.Text == prevNode.Parent.Text && groupingMode == 2)
                        count4++;
                    if (currNode.Parent == prevNode.Parent)
                        count3++;
                    prevNode = currNode;
                }
                if (count1 == nodes.Count || count2 == nodes.Count)
                    if (count3 == nodes.Count) // если все ветви имеют общего родителя
                        return false;
                    else
                        return true;
                else if (count3 == nodes.Count && count4 == nodes.Count) // группировка в сущности
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void specTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level == 2 && e.Node.Tag.ToString() == "Equip" && groupingMode == 0)
                allowEndGrouping = true;
            else if (e.Node.Level == 3 && e.Node.Tag.ToString() == "Equip" && (groupingMode == 1 || groupingMode == 2))
                allowEndGrouping = true;
            else
                e.Cancel = true;
        }

        private void переименоватьToolStripMenuItem2_Click(object sender, EventArgs e) // переименовать сущность
        {
            entityForm ef = new entityForm(entityName);
            ef.ShowDialog();
            lastNode.Text = entityName;
            forceInfoUpdate(lastNode);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e) // удалить сущность
        {
            lastNode.Parent.Nodes.Remove(lastNode);
        }

        private void groupModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (groupModeCheckBox.Checked)
                groupModeTimer.Enabled = true;
            else
                groupModeTimer.Enabled = false;
        }
    }

    public class User // пользователь
    {
        public int userId = 0, userLevel = 0;
        public string userName = "";

        public User(int id, string name, int level)
        {
            userId = id;
            userName = name;
            userLevel = level;
        }
    }

    public class userGroup: List<User> // группа пользователей
    {
        public int groupId = 0;

        public userGroup(int id)
        {
            groupId = id;
        }
    }

    public class Entity // сущность (контроллер, системный блок, ...)
    {
        public int entityId = 0;
        public string entityName = "";

        public Entity(int id, string name)
        {
            entityId = id;
            entityName = name;
        }
    }

    public class Equipment // покупное изделие
    {
        public int equipId = 0, equipCode = 0, equipCount = 0, equipPrice = 0;
        public string equipSeller = "", equipName = "", equipProducer = "";
        public Currency equipCurrency = Currency.Ruble;
        public deliveryType equipDelType = deliveryType.EXW;
        public Entity equipEntity = null;
        public User equipCreatedBy = null;

        public Equipment(int id, string seller, int code, string name, int count, int price, string producer, int currency, int delivery_type, Entity entity, User creator)
        {
            equipId = id;
            equipSeller = seller;
            equipCode = code;
            equipName = name;
            equipCount = count;
            equipPrice = price;
            equipProducer = producer;
            equipCurrency = (Currency)currency;
            equipDelType = (deliveryType)delivery_type;
            equipEntity = entity;
            equipCreatedBy = creator;
        }
    }

    public class equipmentGroup : List<Equipment> // комплектная единица
    {
        public int groupId = 0;
        public string groupName = "";

        public equipmentGroup(int id, string name)
        {
            groupId = id;
            groupName = name;
        }
    }

    public class equipmentSet : List<equipmentGroup> // ктс
    {
        public int setId = 0;
        public string setName = "";
        public List<Equipment> setEquip = new List<Equipment>();

        public equipmentSet(int id, string name)
        {
            setId = id;
            setName = name;
        }
    }

    public class Specification: List<equipmentSet> // спецификация
    {
        public int specId = 0;
        public string specName = "";

        public Specification(int id, string name)
        {
            specId = id;
            specName = name;
        }
    }

    public class SpecTreeNode : TreeNode
    {
        public Specification nodeSpec = null;
        public equipmentSet nodeES = null;
        public Equipment nodeEq = null;
        public equipmentGroup nodeEG = null;

        public SpecTreeNode(string text, Specification spec, equipmentSet set, Equipment equipment, equipmentGroup group)
        {
            this.Text = text;
            nodeSpec = spec;
            nodeES = set;
            nodeEq = equipment;
            nodeEG = group;
        }
    }
}
