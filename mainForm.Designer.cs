namespace Specification
{
    partial class mainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.specOpen = new System.Windows.Forms.Button();
            this.specTree = new System.Windows.Forms.TreeView();
            this.specMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specDeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрЗакупочнойВедомостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eqSetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПокупноеИзделиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьПокупноеИзделиеToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьКТСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eqSetDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.закрепитьКТСЗаПользователемToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlyMineCheckBox = new System.Windows.Forms.CheckBox();
            this.equipMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.копироватьИзделиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПокупноеИзделиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoBox = new System.Windows.Forms.ListBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.groupButton = new System.Windows.Forms.Button();
            this.addSpecMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новаяСпецификацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eqGroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПокупноеИзделиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьКомплЕдиницуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogButton = new System.Windows.Forms.Button();
            this.groupModeTimer = new System.Windows.Forms.Timer(this.components);
            this.entityMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.переименоватьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupModeCheckBox = new System.Windows.Forms.CheckBox();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specMenu.SuspendLayout();
            this.eqSetMenu.SuspendLayout();
            this.equipMenu.SuspendLayout();
            this.addSpecMenu.SuspendLayout();
            this.eqGroupMenu.SuspendLayout();
            this.entityMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // specOpen
            // 
            this.specOpen.Enabled = false;
            this.specOpen.Location = new System.Drawing.Point(12, 56);
            this.specOpen.Name = "specOpen";
            this.specOpen.Size = new System.Drawing.Size(302, 38);
            this.specOpen.TabIndex = 1;
            this.specOpen.Text = "Открыть базу спецификаций";
            this.specOpen.UseVisualStyleBackColor = true;
            this.specOpen.Click += new System.EventHandler(this.specOpen_Click);
            // 
            // specTree
            // 
            this.specTree.Location = new System.Drawing.Point(12, 100);
            this.specTree.Name = "specTree";
            this.specTree.Size = new System.Drawing.Size(302, 250);
            this.specTree.TabIndex = 2;
            this.specTree.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.specTree_BeforeCheck);
            this.specTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.specTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // specMenu
            // 
            this.specMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.переименоватьToolStripMenuItem,
            this.specDeleteMenu,
            this.просмотрЗакупочнойВедомостиToolStripMenuItem});
            this.specMenu.Name = "menu";
            this.specMenu.Size = new System.Drawing.Size(362, 146);
            // 
            // добавитьКомплектнуюЕдиницуToolStripMenuItem
            // 
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem.Image = global::Specification.Properties.Resources.add_button_2_512;
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem.Name = "добавитьКомплектнуюЕдиницуToolStripMenuItem";
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem.Text = "Добавить комплекс технических средств";
            this.добавитьКомплектнуюЕдиницуToolStripMenuItem.Click += new System.EventHandler(this.добавитьКомплектнуюЕдиницуToolStripMenuItem_Click);
            // 
            // переименоватьToolStripMenuItem
            // 
            this.переименоватьToolStripMenuItem.Image = global::Specification.Properties.Resources.img_167289;
            this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
            this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.переименоватьToolStripMenuItem.Text = "Переименовать";
            this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
            // 
            // specDeleteMenu
            // 
            this.specDeleteMenu.Image = global::Specification.Properties.Resources.delete_512;
            this.specDeleteMenu.Name = "specDeleteMenu";
            this.specDeleteMenu.Size = new System.Drawing.Size(361, 24);
            this.specDeleteMenu.Text = "Удалить";
            this.specDeleteMenu.Click += new System.EventHandler(this.specDeleteMenu_Click);
            // 
            // просмотрЗакупочнойВедомостиToolStripMenuItem
            // 
            this.просмотрЗакупочнойВедомостиToolStripMenuItem.Image = global::Specification.Properties.Resources.specification;
            this.просмотрЗакупочнойВедомостиToolStripMenuItem.Name = "просмотрЗакупочнойВедомостиToolStripMenuItem";
            this.просмотрЗакупочнойВедомостиToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.просмотрЗакупочнойВедомостиToolStripMenuItem.Text = "Просмотр закупочной ведомости";
            this.просмотрЗакупочнойВедомостиToolStripMenuItem.Click += new System.EventHandler(this.просмотрЗакупочнойВедомостиToolStripMenuItem_Click);
            // 
            // eqSetMenu
            // 
            this.eqSetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПокупноеИзделиеToolStripMenuItem,
            this.добавитьПокупноеИзделиеToolStripMenuItem2,
            this.копироватьКТСToolStripMenuItem,
            this.переименоватьToolStripMenuItem1,
            this.eqSetDelete,
            this.закрепитьКТСЗаПользователемToolStripMenuItem});
            this.eqSetMenu.Name = "eqSetMenu";
            this.eqSetMenu.Size = new System.Drawing.Size(311, 148);
            // 
            // добавитьПокупноеИзделиеToolStripMenuItem
            // 
            this.добавитьПокупноеИзделиеToolStripMenuItem.Image = global::Specification.Properties.Resources.add_button_2_512;
            this.добавитьПокупноеИзделиеToolStripMenuItem.Name = "добавитьПокупноеИзделиеToolStripMenuItem";
            this.добавитьПокупноеИзделиеToolStripMenuItem.Size = new System.Drawing.Size(310, 24);
            this.добавитьПокупноеИзделиеToolStripMenuItem.Text = "Добавить комплектную единицу";
            this.добавитьПокупноеИзделиеToolStripMenuItem.Click += new System.EventHandler(this.добавитьПокупноеИзделиеToolStripMenuItem_Click);
            // 
            // добавитьПокупноеИзделиеToolStripMenuItem2
            // 
            this.добавитьПокупноеИзделиеToolStripMenuItem2.Image = global::Specification.Properties.Resources.add_button_2_512;
            this.добавитьПокупноеИзделиеToolStripMenuItem2.Name = "добавитьПокупноеИзделиеToolStripMenuItem2";
            this.добавитьПокупноеИзделиеToolStripMenuItem2.Size = new System.Drawing.Size(310, 24);
            this.добавитьПокупноеИзделиеToolStripMenuItem2.Text = "Добавить покупное изделие";
            this.добавитьПокупноеИзделиеToolStripMenuItem2.Click += new System.EventHandler(this.добавитьПокупноеИзделиеToolStripMenuItem2_Click);
            // 
            // копироватьКТСToolStripMenuItem
            // 
            this.копироватьКТСToolStripMenuItem.Image = global::Specification.Properties.Resources.copies;
            this.копироватьКТСToolStripMenuItem.Name = "копироватьКТСToolStripMenuItem";
            this.копироватьКТСToolStripMenuItem.Size = new System.Drawing.Size(310, 24);
            this.копироватьКТСToolStripMenuItem.Text = "Копировать КТС";
            this.копироватьКТСToolStripMenuItem.Click += new System.EventHandler(this.копироватьКТСToolStripMenuItem_Click);
            // 
            // переименоватьToolStripMenuItem1
            // 
            this.переименоватьToolStripMenuItem1.Image = global::Specification.Properties.Resources.img_167289;
            this.переименоватьToolStripMenuItem1.Name = "переименоватьToolStripMenuItem1";
            this.переименоватьToolStripMenuItem1.Size = new System.Drawing.Size(310, 24);
            this.переименоватьToolStripMenuItem1.Text = "Переименовать";
            this.переименоватьToolStripMenuItem1.Click += new System.EventHandler(this.переименоватьToolStripMenuItem1_Click);
            // 
            // eqSetDelete
            // 
            this.eqSetDelete.Image = global::Specification.Properties.Resources.delete_512;
            this.eqSetDelete.Name = "eqSetDelete";
            this.eqSetDelete.Size = new System.Drawing.Size(310, 24);
            this.eqSetDelete.Text = "Удалить";
            this.eqSetDelete.Click += new System.EventHandler(this.eqSetDelete_Click);
            // 
            // закрепитьКТСЗаПользователемToolStripMenuItem
            // 
            this.закрепитьКТСЗаПользователемToolStripMenuItem.Image = global::Specification.Properties.Resources._65609;
            this.закрепитьКТСЗаПользователемToolStripMenuItem.Name = "закрепитьКТСЗаПользователемToolStripMenuItem";
            this.закрепитьКТСЗаПользователемToolStripMenuItem.Size = new System.Drawing.Size(310, 24);
            this.закрепитьКТСЗаПользователемToolStripMenuItem.Text = "Закрепить КТС за пользователем";
            this.закрепитьКТСЗаПользователемToolStripMenuItem.Click += new System.EventHandler(this.закрепитьКТСЗаПользователемToolStripMenuItem_Click);
            // 
            // onlyMineCheckBox
            // 
            this.onlyMineCheckBox.AutoSize = true;
            this.onlyMineCheckBox.Enabled = false;
            this.onlyMineCheckBox.Location = new System.Drawing.Point(450, 56);
            this.onlyMineCheckBox.Name = "onlyMineCheckBox";
            this.onlyMineCheckBox.Size = new System.Drawing.Size(161, 21);
            this.onlyMineCheckBox.TabIndex = 3;
            this.onlyMineCheckBox.Text = "Режим \"только мое\"";
            this.onlyMineCheckBox.UseVisualStyleBackColor = true;
            this.onlyMineCheckBox.CheckedChanged += new System.EventHandler(this.onlyMineCheckBox_CheckedChanged);
            // 
            // equipMenu
            // 
            this.equipMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.копироватьИзделиеToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьПокупноеИзделиеToolStripMenuItem});
            this.equipMenu.Name = "equipMenu";
            this.equipMenu.Size = new System.Drawing.Size(181, 76);
            // 
            // копироватьИзделиеToolStripMenuItem
            // 
            this.копироватьИзделиеToolStripMenuItem.Image = global::Specification.Properties.Resources.copies;
            this.копироватьИзделиеToolStripMenuItem.Name = "копироватьИзделиеToolStripMenuItem";
            this.копироватьИзделиеToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.копироватьИзделиеToolStripMenuItem.Text = "Копировать";
            this.копироватьИзделиеToolStripMenuItem.Click += new System.EventHandler(this.копироватьИзделиеToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Image = global::Specification.Properties.Resources.img_167289;
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // удалитьПокупноеИзделиеToolStripMenuItem
            // 
            this.удалитьПокупноеИзделиеToolStripMenuItem.Image = global::Specification.Properties.Resources.delete_512;
            this.удалитьПокупноеИзделиеToolStripMenuItem.Name = "удалитьПокупноеИзделиеToolStripMenuItem";
            this.удалитьПокупноеИзделиеToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.удалитьПокупноеИзделиеToolStripMenuItem.Text = "Удалить";
            this.удалитьПокупноеИзделиеToolStripMenuItem.Click += new System.EventHandler(this.удалитьПокупноеИзделиеToolStripMenuItem_Click);
            // 
            // infoBox
            // 
            this.infoBox.BackColor = System.Drawing.SystemColors.Control;
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBox.FormattingEnabled = true;
            this.infoBox.ItemHeight = 16;
            this.infoBox.Location = new System.Drawing.Point(320, 100);
            this.infoBox.Name = "infoBox";
            this.infoBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.infoBox.Size = new System.Drawing.Size(296, 240);
            this.infoBox.TabIndex = 4;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(12, 12);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(117, 38);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Авторизация";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Location = new System.Drawing.Point(135, 23);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(178, 17);
            this.welcomeLabel.TabIndex = 6;
            this.welcomeLabel.Text = "Добро пожаловать, гость!";
            // 
            // groupButton
            // 
            this.groupButton.Enabled = false;
            this.groupButton.Location = new System.Drawing.Point(320, 56);
            this.groupButton.Name = "groupButton";
            this.groupButton.Size = new System.Drawing.Size(114, 38);
            this.groupButton.TabIndex = 7;
            this.groupButton.Text = "Группировка";
            this.groupButton.UseVisualStyleBackColor = true;
            this.groupButton.Click += new System.EventHandler(this.groupButton_Click);
            // 
            // addSpecMenu
            // 
            this.addSpecMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяСпецификацияToolStripMenuItem});
            this.addSpecMenu.Name = "addSpecMenu";
            this.addSpecMenu.Size = new System.Drawing.Size(229, 28);
            // 
            // новаяСпецификацияToolStripMenuItem
            // 
            this.новаяСпецификацияToolStripMenuItem.Image = global::Specification.Properties.Resources.add_button_2_512;
            this.новаяСпецификацияToolStripMenuItem.Name = "новаяСпецификацияToolStripMenuItem";
            this.новаяСпецификацияToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.новаяСпецификацияToolStripMenuItem.Text = "Новая спецификация";
            this.новаяСпецификацияToolStripMenuItem.Click += new System.EventHandler(this.новаяСпецификацияToolStripMenuItem_Click);
            // 
            // eqGroupMenu
            // 
            this.eqGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПокупноеИзделиеToolStripMenuItem1,
            this.копироватьКомплЕдиницуToolStripMenuItem,
            this.редактироватьToolStripMenuItem1,
            this.удалитьToolStripMenuItem,
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem});
            this.eqGroupMenu.Name = "eqGroupMenu";
            this.eqGroupMenu.Size = new System.Drawing.Size(395, 124);
            // 
            // добавитьПокупноеИзделиеToolStripMenuItem1
            // 
            this.добавитьПокупноеИзделиеToolStripMenuItem1.Image = global::Specification.Properties.Resources.add_button_2_512;
            this.добавитьПокупноеИзделиеToolStripMenuItem1.Name = "добавитьПокупноеИзделиеToolStripMenuItem1";
            this.добавитьПокупноеИзделиеToolStripMenuItem1.Size = new System.Drawing.Size(394, 24);
            this.добавитьПокупноеИзделиеToolStripMenuItem1.Text = "Добавить покупное изделие";
            this.добавитьПокупноеИзделиеToolStripMenuItem1.Click += new System.EventHandler(this.добавитьПокупноеИзделиеToolStripMenuItem1_Click);
            // 
            // копироватьКомплЕдиницуToolStripMenuItem
            // 
            this.копироватьКомплЕдиницуToolStripMenuItem.Image = global::Specification.Properties.Resources.copies;
            this.копироватьКомплЕдиницуToolStripMenuItem.Name = "копироватьКомплЕдиницуToolStripMenuItem";
            this.копироватьКомплЕдиницуToolStripMenuItem.Size = new System.Drawing.Size(394, 24);
            this.копироватьКомплЕдиницуToolStripMenuItem.Text = "Копировать компл. единицу";
            this.копироватьКомплЕдиницуToolStripMenuItem.Click += new System.EventHandler(this.копироватьКомплЕдиницуToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem1
            // 
            this.редактироватьToolStripMenuItem1.Image = global::Specification.Properties.Resources.img_167289;
            this.редактироватьToolStripMenuItem1.Name = "редактироватьToolStripMenuItem1";
            this.редактироватьToolStripMenuItem1.Size = new System.Drawing.Size(394, 24);
            this.редактироватьToolStripMenuItem1.Text = "Переименовать";
            this.редактироватьToolStripMenuItem1.Click += new System.EventHandler(this.редактироватьToolStripMenuItem1_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Image = global::Specification.Properties.Resources.delete_512;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(394, 24);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem
            // 
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem.Image = global::Specification.Properties.Resources._65609;
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem.Name = "закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem";
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem.Size = new System.Drawing.Size(394, 24);
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem.Text = "Закрепить компл. единицу за пользователем";
            this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem.Click += new System.EventHandler(this.закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem_Click);
            // 
            // catalogButton
            // 
            this.catalogButton.Enabled = false;
            this.catalogButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.catalogButton.Location = new System.Drawing.Point(440, 12);
            this.catalogButton.Name = "catalogButton";
            this.catalogButton.Size = new System.Drawing.Size(176, 38);
            this.catalogButton.TabIndex = 8;
            this.catalogButton.Text = "Редактор справочников";
            this.catalogButton.UseVisualStyleBackColor = true;
            this.catalogButton.Click += new System.EventHandler(this.catalogButton_Click);
            // 
            // groupModeTimer
            // 
            this.groupModeTimer.Interval = 30000;
            this.groupModeTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // entityMenu
            // 
            this.entityMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.переименоватьToolStripMenuItem2,
            this.удалитьToolStripMenuItem1});
            this.entityMenu.Name = "entityMenu";
            this.entityMenu.Size = new System.Drawing.Size(191, 52);
            // 
            // переименоватьToolStripMenuItem2
            // 
            this.переименоватьToolStripMenuItem2.Image = global::Specification.Properties.Resources.img_167289;
            this.переименоватьToolStripMenuItem2.Name = "переименоватьToolStripMenuItem2";
            this.переименоватьToolStripMenuItem2.Size = new System.Drawing.Size(190, 24);
            this.переименоватьToolStripMenuItem2.Text = "Переименовать";
            this.переименоватьToolStripMenuItem2.Click += new System.EventHandler(this.переименоватьToolStripMenuItem2_Click);
            // 
            // удалитьToolStripMenuItem1
            // 
            this.удалитьToolStripMenuItem1.Image = global::Specification.Properties.Resources.delete_512;
            this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
            this.удалитьToolStripMenuItem1.Size = new System.Drawing.Size(190, 24);
            this.удалитьToolStripMenuItem1.Text = "Удалить";
            this.удалитьToolStripMenuItem1.Click += new System.EventHandler(this.удалитьToolStripMenuItem1_Click);
            // 
            // groupModeCheckBox
            // 
            this.groupModeCheckBox.AutoSize = true;
            this.groupModeCheckBox.Enabled = false;
            this.groupModeCheckBox.Location = new System.Drawing.Point(450, 75);
            this.groupModeCheckBox.Name = "groupModeCheckBox";
            this.groupModeCheckBox.Size = new System.Drawing.Size(146, 21);
            this.groupModeCheckBox.TabIndex = 9;
            this.groupModeCheckBox.Text = "Групповой режим";
            this.groupModeCheckBox.UseVisualStyleBackColor = true;
            this.groupModeCheckBox.CheckedChanged += new System.EventHandler(this.groupModeCheckBox_CheckedChanged);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Image = global::Specification.Properties.Resources.copies;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(361, 24);
            this.копироватьToolStripMenuItem.Text = "Копировать спецификацию";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 363);
            this.Controls.Add(this.groupModeCheckBox);
            this.Controls.Add(this.catalogButton);
            this.Controls.Add(this.groupButton);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.onlyMineCheckBox);
            this.Controls.Add(this.specTree);
            this.Controls.Add(this.specOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мастер спецификаций";
            this.specMenu.ResumeLayout(false);
            this.eqSetMenu.ResumeLayout(false);
            this.equipMenu.ResumeLayout(false);
            this.addSpecMenu.ResumeLayout(false);
            this.eqGroupMenu.ResumeLayout(false);
            this.entityMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button specOpen;
        private System.Windows.Forms.TreeView specTree;
        private System.Windows.Forms.ContextMenuStrip specMenu;
        private System.Windows.Forms.ToolStripMenuItem specDeleteMenu;
        private System.Windows.Forms.ContextMenuStrip eqSetMenu;
        private System.Windows.Forms.ToolStripMenuItem eqSetDelete;
        private System.Windows.Forms.ToolStripMenuItem добавитьКомплектнуюЕдиницуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьПокупноеИзделиеToolStripMenuItem;
        private System.Windows.Forms.CheckBox onlyMineCheckBox;
        private System.Windows.Forms.ContextMenuStrip equipMenu;
        private System.Windows.Forms.ToolStripMenuItem удалитьПокупноеИзделиеToolStripMenuItem;
        private System.Windows.Forms.ListBox infoBox;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.ContextMenuStrip addSpecMenu;
        private System.Windows.Forms.ToolStripMenuItem новаяСпецификацияToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip eqGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem добавитьПокупноеИзделиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрепитьКТСЗаПользователемToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрепитьКомплектнуюЕдиницуЗаПользователемToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьПокупноеИзделиеToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem копироватьКТСToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьИзделиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьКомплЕдиницуToolStripMenuItem;
        private System.Windows.Forms.Button catalogButton;
        private System.Windows.Forms.ToolStripMenuItem просмотрЗакупочнойВедомостиToolStripMenuItem;
        private System.Windows.Forms.Timer groupModeTimer;
        private System.Windows.Forms.ContextMenuStrip entityMenu;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem1;
        private System.Windows.Forms.CheckBox groupModeCheckBox;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
    }
}

