namespace armsim
{
    partial class ArmSimForm : Observer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checksumLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTraceOnoffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registersDataGridView = new System.Windows.Forms.DataGridView();
            this.registerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registerValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memoryDataGridView = new System.Windows.Forms.DataGridView();
            this.addressMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hexContentMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disassemblyTerminalTabControl = new System.Windows.Forms.TabControl();
            this.disassemblyTabPage = new System.Windows.Forms.TabPage();
            this.disTextBox = new System.Windows.Forms.TextBox();
            this.disassemblyTextBox = new System.Windows.Forms.TextBox();
            this.terminalTabPage = new System.Windows.Forms.TabPage();
            this.terminlaTextBox = new System.Windows.Forms.TextBox();
            this.stackFlagTabControl = new System.Windows.Forms.TabControl();
            this.flagsTabPage = new System.Windows.Forms.TabPage();
            this.flagsDataGridView = new System.Windows.Forms.DataGridView();
            this.flagNameTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagValueTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stackTabPage = new System.Windows.Forms.TabPage();
            this.stackDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.noRowsShowTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startingMemoryTextBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.fileOpenedLabel = new System.Windows.Forms.Label();
            this.simStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryDataGridView)).BeginInit();
            this.disassemblyTerminalTabControl.SuspendLayout();
            this.disassemblyTabPage.SuspendLayout();
            this.terminalTabPage.SuspendLayout();
            this.stackFlagTabControl.SuspendLayout();
            this.flagsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagsDataGridView)).BeginInit();
            this.stackTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.42793F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.44522F));
            this.tableLayoutPanel1.Controls.Add(this.checksumLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.registersDataGridView, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.memoryDataGridView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.disassemblyTerminalTabControl, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.stackFlagTabControl, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.fileOpenedLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.simStatus, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.823923F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.96281F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.980026F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.85583F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.37741F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 470);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // checksumLabel
            // 
            this.checksumLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checksumLabel.AutoSize = true;
            this.checksumLabel.Location = new System.Drawing.Point(326, 432);
            this.checksumLabel.Name = "checksumLabel";
            this.checksumLabel.Size = new System.Drawing.Size(158, 13);
            this.checksumLabel.TabIndex = 4;
            this.checksumLabel.Text = "Checksum: no files openned yet";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label6, 2);
            this.label6.Location = new System.Drawing.Point(613, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Registers";
            // 
            // menuStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.menuStrip1, 4);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.singleStepToolStripMenuItem,
            this.breakExecutionToolStripMenuItem,
            this.toggleTraceOnoffToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "&Debug";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Enabled = false;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // singleStepToolStripMenuItem
            // 
            this.singleStepToolStripMenuItem.Enabled = false;
            this.singleStepToolStripMenuItem.Name = "singleStepToolStripMenuItem";
            this.singleStepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.singleStepToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.singleStepToolStripMenuItem.Text = "Single step";
            this.singleStepToolStripMenuItem.Click += new System.EventHandler(this.singleStepToolStripMenuItem_Click);
            // 
            // breakExecutionToolStripMenuItem
            // 
            this.breakExecutionToolStripMenuItem.Enabled = false;
            this.breakExecutionToolStripMenuItem.Name = "breakExecutionToolStripMenuItem";
            this.breakExecutionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.breakExecutionToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.breakExecutionToolStripMenuItem.Text = "Break execution";
            this.breakExecutionToolStripMenuItem.Click += new System.EventHandler(this.breakExecutionToolStripMenuItem_Click);
            // 
            // toggleTraceOnoffToolStripMenuItem
            // 
            this.toggleTraceOnoffToolStripMenuItem.Enabled = false;
            this.toggleTraceOnoffToolStripMenuItem.Name = "toggleTraceOnoffToolStripMenuItem";
            this.toggleTraceOnoffToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.toggleTraceOnoffToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.toggleTraceOnoffToolStripMenuItem.Text = "Turn OFF trace log";
            this.toggleTraceOnoffToolStripMenuItem.Click += new System.EventHandler(this.toggleTraceOnoffToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Enabled = false;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // registersDataGridView
            // 
            this.registersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.registersDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.registersDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.registersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.registersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.registerName,
            this.registerValue});
            this.tableLayoutPanel1.SetColumnSpan(this.registersDataGridView, 2);
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.registersDataGridView.DefaultCellStyle = dataGridViewCellStyle38;
            this.registersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registersDataGridView.Location = new System.Drawing.Point(613, 79);
            this.registersDataGridView.Name = "registersDataGridView";
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.registersDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.registersDataGridView.RowHeadersVisible = false;
            this.registersDataGridView.RowHeadersWidth = 15;
            this.registersDataGridView.Size = new System.Drawing.Size(184, 178);
            this.registersDataGridView.TabIndex = 20;
            // 
            // registerName
            // 
            this.registerName.HeaderText = "Name";
            this.registerName.Name = "registerName";
            // 
            // registerValue
            // 
            this.registerValue.HeaderText = "Value";
            this.registerValue.Name = "registerValue";
            // 
            // memoryDataGridView
            // 
            this.memoryDataGridView.AllowUserToAddRows = false;
            this.memoryDataGridView.AllowUserToDeleteRows = false;
            this.memoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.memoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.memoryDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.memoryDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memoryDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.memoryDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.memoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle40;
            this.memoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.addressMemory,
            this.hexContentMemory,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column1});
            this.tableLayoutPanel1.SetColumnSpan(this.memoryDataGridView, 2);
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.memoryDataGridView.DefaultCellStyle = dataGridViewCellStyle41;
            this.memoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryDataGridView.Location = new System.Drawing.Point(3, 79);
            this.memoryDataGridView.Name = "memoryDataGridView";
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.memoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle42;
            this.memoryDataGridView.RowHeadersWidth = 15;
            this.memoryDataGridView.Size = new System.Drawing.Size(604, 178);
            this.memoryDataGridView.TabIndex = 24;
            this.memoryDataGridView.TabStop = false;
            // 
            // addressMemory
            // 
            this.addressMemory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.addressMemory.FillWeight = 120.3811F;
            this.addressMemory.HeaderText = "Address";
            this.addressMemory.Name = "addressMemory";
            this.addressMemory.Width = 85;
            // 
            // hexContentMemory
            // 
            this.hexContentMemory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.hexContentMemory.FillWeight = 192.2285F;
            this.hexContentMemory.HeaderText = "Hex Dump";
            this.hexContentMemory.Name = "hexContentMemory";
            this.hexContentMemory.Width = 85;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.FillWeight = 43.87368F;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.Width = 85;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.FillWeight = 74.87123F;
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.Width = 85;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.FillWeight = 5.227407F;
            this.Column4.HeaderText = "";
            this.Column4.Name = "Column4";
            this.Column4.Width = 85;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 8.935833F;
            this.Column1.HeaderText = "Content";
            this.Column1.Name = "Column1";
            // 
            // disassemblyTerminalTabControl
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.disassemblyTerminalTabControl, 2);
            this.disassemblyTerminalTabControl.Controls.Add(this.disassemblyTabPage);
            this.disassemblyTerminalTabControl.Controls.Add(this.terminalTabPage);
            this.disassemblyTerminalTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disassemblyTerminalTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disassemblyTerminalTabControl.Location = new System.Drawing.Point(3, 270);
            this.disassemblyTerminalTabControl.Name = "disassemblyTerminalTabControl";
            this.disassemblyTerminalTabControl.SelectedIndex = 0;
            this.disassemblyTerminalTabControl.Size = new System.Drawing.Size(604, 157);
            this.disassemblyTerminalTabControl.TabIndex = 25;
            // 
            // disassemblyTabPage
            // 
            this.disassemblyTabPage.Controls.Add(this.disTextBox);
            this.disassemblyTabPage.Controls.Add(this.disassemblyTextBox);
            this.disassemblyTabPage.Location = new System.Drawing.Point(4, 24);
            this.disassemblyTabPage.Name = "disassemblyTabPage";
            this.disassemblyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.disassemblyTabPage.Size = new System.Drawing.Size(596, 129);
            this.disassemblyTabPage.TabIndex = 0;
            this.disassemblyTabPage.Text = "Disassembled Instructions";
            this.disassemblyTabPage.UseVisualStyleBackColor = true;
            // 
            // disTextBox
            // 
            this.disTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disTextBox.Location = new System.Drawing.Point(3, 3);
            this.disTextBox.Multiline = true;
            this.disTextBox.Name = "disTextBox";
            this.disTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.disTextBox.Size = new System.Drawing.Size(590, 123);
            this.disTextBox.TabIndex = 1;
            this.disTextBox.Text = "Load a file and press F5 to run and see the disassembled instructions.";
            // 
            // disassemblyTextBox
            // 
            this.disassemblyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disassemblyTextBox.Location = new System.Drawing.Point(3, 3);
            this.disassemblyTextBox.Multiline = true;
            this.disassemblyTextBox.Name = "disassemblyTextBox";
            this.disassemblyTextBox.Size = new System.Drawing.Size(590, 123);
            this.disassemblyTextBox.TabIndex = 0;
            // 
            // terminalTabPage
            // 
            this.terminalTabPage.Controls.Add(this.terminlaTextBox);
            this.terminalTabPage.Location = new System.Drawing.Point(4, 24);
            this.terminalTabPage.Name = "terminalTabPage";
            this.terminalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.terminalTabPage.Size = new System.Drawing.Size(596, 129);
            this.terminalTabPage.TabIndex = 1;
            this.terminalTabPage.Text = "Terminal";
            this.terminalTabPage.UseVisualStyleBackColor = true;
            // 
            // terminlaTextBox
            // 
            this.terminlaTextBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.terminlaTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminlaTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terminlaTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.terminlaTextBox.Location = new System.Drawing.Point(3, 3);
            this.terminlaTextBox.Multiline = true;
            this.terminlaTextBox.Name = "terminlaTextBox";
            this.terminlaTextBox.Size = new System.Drawing.Size(590, 123);
            this.terminlaTextBox.TabIndex = 0;
            this.terminlaTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.terminlaTextBox_KeyPress);
            // 
            // stackFlagTabControl
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.stackFlagTabControl, 2);
            this.stackFlagTabControl.Controls.Add(this.flagsTabPage);
            this.stackFlagTabControl.Controls.Add(this.stackTabPage);
            this.stackFlagTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackFlagTabControl.Location = new System.Drawing.Point(613, 270);
            this.stackFlagTabControl.Name = "stackFlagTabControl";
            this.stackFlagTabControl.SelectedIndex = 0;
            this.stackFlagTabControl.Size = new System.Drawing.Size(184, 157);
            this.stackFlagTabControl.TabIndex = 26;
            // 
            // flagsTabPage
            // 
            this.flagsTabPage.Controls.Add(this.flagsDataGridView);
            this.flagsTabPage.Location = new System.Drawing.Point(4, 22);
            this.flagsTabPage.Name = "flagsTabPage";
            this.flagsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.flagsTabPage.Size = new System.Drawing.Size(176, 131);
            this.flagsTabPage.TabIndex = 1;
            this.flagsTabPage.Text = "Flags";
            this.flagsTabPage.UseVisualStyleBackColor = true;
            // 
            // flagsDataGridView
            // 
            this.flagsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.flagsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.flagsDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flagsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.flagsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flagNameTab,
            this.flagValueTab});
            this.flagsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.flagsDataGridView.Name = "flagsDataGridView";
            this.flagsDataGridView.RowHeadersVisible = false;
            this.flagsDataGridView.RowHeadersWidth = 15;
            this.flagsDataGridView.Size = new System.Drawing.Size(170, 125);
            this.flagsDataGridView.TabIndex = 0;
            // 
            // flagNameTab
            // 
            this.flagNameTab.HeaderText = "Name";
            this.flagNameTab.Name = "flagNameTab";
            // 
            // flagValueTab
            // 
            this.flagValueTab.HeaderText = "Value";
            this.flagValueTab.Name = "flagValueTab";
            // 
            // stackTabPage
            // 
            this.stackTabPage.Controls.Add(this.stackDataGridView);
            this.stackTabPage.Controls.Add(this.dataGridView1);
            this.stackTabPage.Location = new System.Drawing.Point(4, 22);
            this.stackTabPage.Name = "stackTabPage";
            this.stackTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.stackTabPage.Size = new System.Drawing.Size(176, 131);
            this.stackTabPage.TabIndex = 0;
            this.stackTabPage.Text = "Stack";
            this.stackTabPage.UseVisualStyleBackColor = true;
            // 
            // stackDataGridView
            // 
            this.stackDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.stackDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.stackDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stackDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stackDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.stackDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackDataGridView.Location = new System.Drawing.Point(3, 3);
            this.stackDataGridView.Name = "stackDataGridView";
            this.stackDataGridView.RowHeadersVisible = false;
            this.stackDataGridView.RowHeadersWidth = 15;
            this.stackDataGridView.Size = new System.Drawing.Size(170, 125);
            this.stackDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Address";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle45.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridView1.Size = new System.Drawing.Size(170, 125);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.04546F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.63636F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.60227F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.noRowsShowTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.startingMemoryTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.goButton, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(326, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(281, 43);
            this.tableLayoutPanel2.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Memory adress";
            // 
            // noRowsShowTextBox
            // 
            this.noRowsShowTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.noRowsShowTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.noRowsShowTextBox.Location = new System.Drawing.Point(120, 19);
            this.noRowsShowTextBox.Name = "noRowsShowTextBox";
            this.noRowsShowTextBox.Size = new System.Drawing.Size(102, 20);
            this.noRowsShowTextBox.TabIndex = 2;
            this.noRowsShowTextBox.Text = "15";
            this.noRowsShowTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of rows";
            // 
            // startingMemoryTextBox
            // 
            this.startingMemoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.startingMemoryTextBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.startingMemoryTextBox.Location = new System.Drawing.Point(3, 19);
            this.startingMemoryTextBox.Name = "startingMemoryTextBox";
            this.startingMemoryTextBox.Size = new System.Drawing.Size(111, 20);
            this.startingMemoryTextBox.TabIndex = 1;
            this.startingMemoryTextBox.Text = "4128";
            this.startingMemoryTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // goButton
            // 
            this.goButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.goButton.AutoSize = true;
            this.goButton.Location = new System.Drawing.Point(228, 19);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(38, 21);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "&Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // fileOpenedLabel
            // 
            this.fileOpenedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fileOpenedLabel.AutoSize = true;
            this.fileOpenedLabel.Location = new System.Drawing.Point(3, 432);
            this.fileOpenedLabel.Name = "fileOpenedLabel";
            this.fileOpenedLabel.Size = new System.Drawing.Size(92, 13);
            this.fileOpenedLabel.TabIndex = 5;
            this.fileOpenedLabel.Text = "File opened: none";
            // 
            // simStatus
            // 
            this.simStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.simStatus.AutoSize = true;
            this.simStatus.Location = new System.Drawing.Point(3, 452);
            this.simStatus.Name = "simStatus";
            this.simStatus.Size = new System.Drawing.Size(182, 13);
            this.simStatus.TabIndex = 34;
            this.simStatus.Text = "Simulator status: no files openned yet";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(317, 43);
            this.tableLayoutPanel3.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Memory";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Select an Executable";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ArmSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ArmSimForm";
            this.Text = "ARM7TDMI Simulator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryDataGridView)).EndInit();
            this.disassemblyTerminalTabControl.ResumeLayout(false);
            this.disassemblyTabPage.ResumeLayout(false);
            this.disassemblyTabPage.PerformLayout();
            this.terminalTabPage.ResumeLayout(false);
            this.terminalTabPage.PerformLayout();
            this.stackFlagTabControl.ResumeLayout(false);
            this.flagsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flagsDataGridView)).EndInit();
            this.stackTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stackDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label checksumLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakExecutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleTraceOnoffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label fileOpenedLabel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridView registersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn registerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn registerValue;
        private System.Windows.Forms.DataGridView memoryDataGridView;
        private System.Windows.Forms.TabControl stackFlagTabControl;
        private System.Windows.Forms.TabPage stackTabPage;
        private System.Windows.Forms.TabPage flagsTabPage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView flagsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagNameTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagValueTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox startingMemoryTextBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label simStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox noRowsShowTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn hexContentMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView stackDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TabControl disassemblyTerminalTabControl;
        private System.Windows.Forms.TabPage disassemblyTabPage;
        private System.Windows.Forms.TextBox disTextBox;
        private System.Windows.Forms.TextBox disassemblyTextBox;
        private System.Windows.Forms.TabPage terminalTabPage;
        private System.Windows.Forms.TextBox terminlaTextBox;
    }
}