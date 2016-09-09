namespace P4R4_PokeMob_Creator
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.foldersPage = new System.Windows.Forms.TabPage();
            this.customConfigChkBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.defaultConfigChkBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.folderToCreateTitleLab = new System.Windows.Forms.Label();
            this.foldersTitleLab = new System.Windows.Forms.Label();
            this.foldersDivider = new MaterialSkin.Controls.MaterialDivider();
            this.cfgFileBrowse = new MaterialSkin.Controls.MaterialRaisedButton();
            this.folderToPlaceBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.botFolderBrowseBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.cfgFilePathTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.folderToPlace = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.nbFoldersNum = new System.Windows.Forms.NumericUpDown();
            this.botFolderTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.dirForFoldersLab = new MaterialSkin.Controls.MaterialLabel();
            this.necroFolderLab = new MaterialSkin.Controls.MaterialLabel();
            this.nbFoldersLab = new MaterialSkin.Controls.MaterialLabel();
            this.accsPage = new System.Windows.Forms.TabPage();
            this.browseAccsBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.accsTitleLab = new System.Windows.Forms.Label();
            this.accFormatLab = new System.Windows.Forms.Label();
            this.accountsNeededLab = new System.Windows.Forms.Label();
            this.accsRichTxtBox = new System.Windows.Forms.RichTextBox();
            this.proxiesTabPage = new System.Windows.Forms.TabPage();
            this.formatProxyLab = new System.Windows.Forms.Label();
            this.browseProxiesBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.proxiesNeededLab = new System.Windows.Forms.Label();
            this.proxiesRichTxtBox = new System.Windows.Forms.RichTextBox();
            this.loadProxiesPageTitle = new System.Windows.Forms.Label();
            this.creationTabPage = new System.Windows.Forms.TabPage();
            this.startProcessesChkBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.creationLogsRichTxtBox = new System.Windows.Forms.RichTextBox();
            this.creationPageTitle = new System.Windows.Forms.Label();
            this.createFoldersBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.statsPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.statsTitleLab = new System.Windows.Forms.Label();
            this.materialTabControl1.SuspendLayout();
            this.foldersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFoldersNum)).BeginInit();
            this.accsPage.SuspendLayout();
            this.proxiesTabPage.SuspendLayout();
            this.creationTabPage.SuspendLayout();
            this.statsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 62);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(752, 42);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.foldersPage);
            this.materialTabControl1.Controls.Add(this.accsPage);
            this.materialTabControl1.Controls.Add(this.proxiesTabPage);
            this.materialTabControl1.Controls.Add(this.creationTabPage);
            this.materialTabControl1.Controls.Add(this.statsPage);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 110);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(752, 394);
            this.materialTabControl1.TabIndex = 1;
            // 
            // foldersPage
            // 
            this.foldersPage.BackColor = System.Drawing.Color.White;
            this.foldersPage.Controls.Add(this.customConfigChkBox);
            this.foldersPage.Controls.Add(this.defaultConfigChkBox);
            this.foldersPage.Controls.Add(this.folderToCreateTitleLab);
            this.foldersPage.Controls.Add(this.foldersTitleLab);
            this.foldersPage.Controls.Add(this.foldersDivider);
            this.foldersPage.Controls.Add(this.cfgFileBrowse);
            this.foldersPage.Controls.Add(this.folderToPlaceBtn);
            this.foldersPage.Controls.Add(this.botFolderBrowseBtn);
            this.foldersPage.Controls.Add(this.cfgFilePathTxt);
            this.foldersPage.Controls.Add(this.folderToPlace);
            this.foldersPage.Controls.Add(this.nbFoldersNum);
            this.foldersPage.Controls.Add(this.botFolderTxt);
            this.foldersPage.Controls.Add(this.dirForFoldersLab);
            this.foldersPage.Controls.Add(this.necroFolderLab);
            this.foldersPage.Controls.Add(this.nbFoldersLab);
            this.foldersPage.Location = new System.Drawing.Point(4, 22);
            this.foldersPage.Name = "foldersPage";
            this.foldersPage.Padding = new System.Windows.Forms.Padding(3);
            this.foldersPage.Size = new System.Drawing.Size(744, 368);
            this.foldersPage.TabIndex = 0;
            this.foldersPage.Text = "Folders";
            // 
            // customConfigChkBox
            // 
            this.customConfigChkBox.AutoSize = true;
            this.customConfigChkBox.Depth = 0;
            this.customConfigChkBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.customConfigChkBox.Location = new System.Drawing.Point(13, 173);
            this.customConfigChkBox.Margin = new System.Windows.Forms.Padding(0);
            this.customConfigChkBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.customConfigChkBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.customConfigChkBox.Name = "customConfigChkBox";
            this.customConfigChkBox.Ripple = true;
            this.customConfigChkBox.Size = new System.Drawing.Size(203, 30);
            this.customConfigChkBox.TabIndex = 21;
            this.customConfigChkBox.Text = "Custom PokeMobBot config";
            this.customConfigChkBox.UseVisualStyleBackColor = true;
            this.customConfigChkBox.CheckedChanged += new System.EventHandler(this.customConfigChkBox_CheckedChanged);
            // 
            // defaultConfigChkBox
            // 
            this.defaultConfigChkBox.AutoSize = true;
            this.defaultConfigChkBox.Checked = true;
            this.defaultConfigChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultConfigChkBox.Depth = 0;
            this.defaultConfigChkBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.defaultConfigChkBox.Location = new System.Drawing.Point(13, 138);
            this.defaultConfigChkBox.Margin = new System.Windows.Forms.Padding(0);
            this.defaultConfigChkBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.defaultConfigChkBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.defaultConfigChkBox.Name = "defaultConfigChkBox";
            this.defaultConfigChkBox.Ripple = true;
            this.defaultConfigChkBox.Size = new System.Drawing.Size(117, 30);
            this.defaultConfigChkBox.TabIndex = 20;
            this.defaultConfigChkBox.Text = "Default config";
            this.defaultConfigChkBox.UseVisualStyleBackColor = true;
            this.defaultConfigChkBox.CheckedChanged += new System.EventHandler(this.defaultConfigChkBox_CheckedChanged);
            // 
            // folderToCreateTitleLab
            // 
            this.folderToCreateTitleLab.AutoSize = true;
            this.folderToCreateTitleLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderToCreateTitleLab.ForeColor = System.Drawing.Color.Black;
            this.folderToCreateTitleLab.Location = new System.Drawing.Point(8, 258);
            this.folderToCreateTitleLab.Name = "folderToCreateTitleLab";
            this.folderToCreateTitleLab.Size = new System.Drawing.Size(338, 29);
            this.folderToCreateTitleLab.TabIndex = 19;
            this.folderToCreateTitleLab.Text = "Set the nb. of folders to create:";
            // 
            // foldersTitleLab
            // 
            this.foldersTitleLab.AutoSize = true;
            this.foldersTitleLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foldersTitleLab.ForeColor = System.Drawing.Color.Black;
            this.foldersTitleLab.Location = new System.Drawing.Point(8, 3);
            this.foldersTitleLab.Name = "foldersTitleLab";
            this.foldersTitleLab.Size = new System.Drawing.Size(303, 29);
            this.foldersTitleLab.TabIndex = 18;
            this.foldersTitleLab.Text = "Select the required folders:";
            // 
            // foldersDivider
            // 
            this.foldersDivider.BackColor = System.Drawing.Color.DarkCyan;
            this.foldersDivider.Depth = 0;
            this.foldersDivider.Location = new System.Drawing.Point(13, 222);
            this.foldersDivider.MouseState = MaterialSkin.MouseState.HOVER;
            this.foldersDivider.Name = "foldersDivider";
            this.foldersDivider.Size = new System.Drawing.Size(674, 10);
            this.foldersDivider.TabIndex = 14;
            // 
            // cfgFileBrowse
            // 
            this.cfgFileBrowse.Depth = 0;
            this.cfgFileBrowse.Enabled = false;
            this.cfgFileBrowse.Location = new System.Drawing.Point(661, 173);
            this.cfgFileBrowse.MouseState = MaterialSkin.MouseState.HOVER;
            this.cfgFileBrowse.Name = "cfgFileBrowse";
            this.cfgFileBrowse.Primary = true;
            this.cfgFileBrowse.Size = new System.Drawing.Size(25, 23);
            this.cfgFileBrowse.TabIndex = 12;
            this.cfgFileBrowse.Text = "...";
            this.cfgFileBrowse.UseVisualStyleBackColor = true;
            this.cfgFileBrowse.Click += new System.EventHandler(this.cfgFileBrowse_Click);
            // 
            // folderToPlaceBtn
            // 
            this.folderToPlaceBtn.Depth = 0;
            this.folderToPlaceBtn.Location = new System.Drawing.Point(661, 90);
            this.folderToPlaceBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.folderToPlaceBtn.Name = "folderToPlaceBtn";
            this.folderToPlaceBtn.Primary = true;
            this.folderToPlaceBtn.Size = new System.Drawing.Size(25, 23);
            this.folderToPlaceBtn.TabIndex = 11;
            this.folderToPlaceBtn.Text = "...";
            this.folderToPlaceBtn.UseVisualStyleBackColor = true;
            this.folderToPlaceBtn.Click += new System.EventHandler(this.folderToPlaceBtn_Click);
            // 
            // botFolderBrowseBtn
            // 
            this.botFolderBrowseBtn.Depth = 0;
            this.botFolderBrowseBtn.Location = new System.Drawing.Point(661, 49);
            this.botFolderBrowseBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.botFolderBrowseBtn.Name = "botFolderBrowseBtn";
            this.botFolderBrowseBtn.Primary = true;
            this.botFolderBrowseBtn.Size = new System.Drawing.Size(25, 23);
            this.botFolderBrowseBtn.TabIndex = 10;
            this.botFolderBrowseBtn.Text = "...";
            this.botFolderBrowseBtn.UseVisualStyleBackColor = true;
            this.botFolderBrowseBtn.Click += new System.EventHandler(this.botFolderBrowseBtn_Click);
            // 
            // cfgFilePathTxt
            // 
            this.cfgFilePathTxt.Depth = 0;
            this.cfgFilePathTxt.Enabled = false;
            this.cfgFilePathTxt.Hint = "";
            this.cfgFilePathTxt.Location = new System.Drawing.Point(230, 173);
            this.cfgFilePathTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.cfgFilePathTxt.Name = "cfgFilePathTxt";
            this.cfgFilePathTxt.PasswordChar = '\0';
            this.cfgFilePathTxt.SelectedText = "";
            this.cfgFilePathTxt.SelectionLength = 0;
            this.cfgFilePathTxt.SelectionStart = 0;
            this.cfgFilePathTxt.Size = new System.Drawing.Size(415, 23);
            this.cfgFilePathTxt.TabIndex = 6;
            this.cfgFilePathTxt.UseSystemPasswordChar = false;
            // 
            // folderToPlace
            // 
            this.folderToPlace.Depth = 0;
            this.folderToPlace.Enabled = false;
            this.folderToPlace.Hint = "";
            this.folderToPlace.Location = new System.Drawing.Point(192, 90);
            this.folderToPlace.MouseState = MaterialSkin.MouseState.HOVER;
            this.folderToPlace.Name = "folderToPlace";
            this.folderToPlace.PasswordChar = '\0';
            this.folderToPlace.SelectedText = "";
            this.folderToPlace.SelectionLength = 0;
            this.folderToPlace.SelectionStart = 0;
            this.folderToPlace.Size = new System.Drawing.Size(453, 23);
            this.folderToPlace.TabIndex = 5;
            this.folderToPlace.UseSystemPasswordChar = false;
            // 
            // nbFoldersNum
            // 
            this.nbFoldersNum.Location = new System.Drawing.Point(192, 312);
            this.nbFoldersNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbFoldersNum.Name = "nbFoldersNum";
            this.nbFoldersNum.Size = new System.Drawing.Size(36, 20);
            this.nbFoldersNum.TabIndex = 4;
            this.nbFoldersNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbFoldersNum.ValueChanged += new System.EventHandler(this.nbFoldersNum_ValueChanged);
            // 
            // botFolderTxt
            // 
            this.botFolderTxt.Depth = 0;
            this.botFolderTxt.Enabled = false;
            this.botFolderTxt.Hint = "";
            this.botFolderTxt.Location = new System.Drawing.Point(192, 49);
            this.botFolderTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.botFolderTxt.Name = "botFolderTxt";
            this.botFolderTxt.PasswordChar = '\0';
            this.botFolderTxt.SelectedText = "";
            this.botFolderTxt.SelectionLength = 0;
            this.botFolderTxt.SelectionStart = 0;
            this.botFolderTxt.Size = new System.Drawing.Size(453, 23);
            this.botFolderTxt.TabIndex = 2;
            this.botFolderTxt.UseSystemPasswordChar = false;
            // 
            // dirForFoldersLab
            // 
            this.dirForFoldersLab.AutoSize = true;
            this.dirForFoldersLab.Depth = 0;
            this.dirForFoldersLab.Font = new System.Drawing.Font("Roboto", 11F);
            this.dirForFoldersLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dirForFoldersLab.Location = new System.Drawing.Point(9, 90);
            this.dirForFoldersLab.MouseState = MaterialSkin.MouseState.HOVER;
            this.dirForFoldersLab.Name = "dirForFoldersLab";
            this.dirForFoldersLab.Size = new System.Drawing.Size(141, 19);
            this.dirForFoldersLab.TabIndex = 2;
            this.dirForFoldersLab.Text = "Dir to place folders:";
            // 
            // necroFolderLab
            // 
            this.necroFolderLab.AutoSize = true;
            this.necroFolderLab.Depth = 0;
            this.necroFolderLab.Font = new System.Drawing.Font("Roboto", 11F);
            this.necroFolderLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.necroFolderLab.Location = new System.Drawing.Point(9, 49);
            this.necroFolderLab.MouseState = MaterialSkin.MouseState.HOVER;
            this.necroFolderLab.Name = "necroFolderLab";
            this.necroFolderLab.Size = new System.Drawing.Size(143, 19);
            this.necroFolderLab.TabIndex = 1;
            this.necroFolderLab.Text = "PokeMobBot folder:";
            // 
            // nbFoldersLab
            // 
            this.nbFoldersLab.AutoSize = true;
            this.nbFoldersLab.Depth = 0;
            this.nbFoldersLab.Font = new System.Drawing.Font("Roboto", 11F);
            this.nbFoldersLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nbFoldersLab.Location = new System.Drawing.Point(9, 313);
            this.nbFoldersLab.MouseState = MaterialSkin.MouseState.HOVER;
            this.nbFoldersLab.Name = "nbFoldersLab";
            this.nbFoldersLab.Size = new System.Drawing.Size(169, 19);
            this.nbFoldersLab.TabIndex = 0;
            this.nbFoldersLab.Text = "Nb. of folders to create:";
            // 
            // accsPage
            // 
            this.accsPage.BackColor = System.Drawing.Color.White;
            this.accsPage.Controls.Add(this.browseAccsBtn);
            this.accsPage.Controls.Add(this.accsTitleLab);
            this.accsPage.Controls.Add(this.accFormatLab);
            this.accsPage.Controls.Add(this.accountsNeededLab);
            this.accsPage.Controls.Add(this.accsRichTxtBox);
            this.accsPage.Location = new System.Drawing.Point(4, 22);
            this.accsPage.Name = "accsPage";
            this.accsPage.Padding = new System.Windows.Forms.Padding(3);
            this.accsPage.Size = new System.Drawing.Size(744, 368);
            this.accsPage.TabIndex = 1;
            this.accsPage.Text = "Accounts";
            // 
            // browseAccsBtn
            // 
            this.browseAccsBtn.Depth = 0;
            this.browseAccsBtn.Location = new System.Drawing.Point(557, 85);
            this.browseAccsBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.browseAccsBtn.Name = "browseAccsBtn";
            this.browseAccsBtn.Primary = true;
            this.browseAccsBtn.Size = new System.Drawing.Size(137, 35);
            this.browseAccsBtn.TabIndex = 20;
            this.browseAccsBtn.Text = "LOAD ACCOUNTS";
            this.browseAccsBtn.UseVisualStyleBackColor = true;
            this.browseAccsBtn.Click += new System.EventHandler(this.browseAccsBtn_Click);
            // 
            // accsTitleLab
            // 
            this.accsTitleLab.AutoSize = true;
            this.accsTitleLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accsTitleLab.ForeColor = System.Drawing.Color.Black;
            this.accsTitleLab.Location = new System.Drawing.Point(3, 8);
            this.accsTitleLab.Name = "accsTitleLab";
            this.accsTitleLab.Size = new System.Drawing.Size(216, 29);
            this.accsTitleLab.TabIndex = 17;
            this.accsTitleLab.Text = "Load/add accounts";
            // 
            // accFormatLab
            // 
            this.accFormatLab.AutoSize = true;
            this.accFormatLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accFormatLab.ForeColor = System.Drawing.Color.Red;
            this.accFormatLab.Location = new System.Drawing.Point(4, 46);
            this.accFormatLab.Name = "accFormatLab";
            this.accFormatLab.Size = new System.Drawing.Size(201, 36);
            this.accFormatLab.TabIndex = 16;
            this.accFormatLab.Text = "Format is: account:password\r\nGoogle/PTC accounts\r\n";
            // 
            // accountsNeededLab
            // 
            this.accountsNeededLab.AutoSize = true;
            this.accountsNeededLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountsNeededLab.ForeColor = System.Drawing.Color.Red;
            this.accountsNeededLab.Location = new System.Drawing.Point(553, 337);
            this.accountsNeededLab.Name = "accountsNeededLab";
            this.accountsNeededLab.Size = new System.Drawing.Size(138, 18);
            this.accountsNeededLab.TabIndex = 2;
            this.accountsNeededLab.Text = "Accounts needed: 1";
            // 
            // accsRichTxtBox
            // 
            this.accsRichTxtBox.Location = new System.Drawing.Point(8, 85);
            this.accsRichTxtBox.Name = "accsRichTxtBox";
            this.accsRichTxtBox.Size = new System.Drawing.Size(504, 271);
            this.accsRichTxtBox.TabIndex = 0;
            this.accsRichTxtBox.Text = "";
            // 
            // proxiesTabPage
            // 
            this.proxiesTabPage.Controls.Add(this.formatProxyLab);
            this.proxiesTabPage.Controls.Add(this.browseProxiesBtn);
            this.proxiesTabPage.Controls.Add(this.proxiesNeededLab);
            this.proxiesTabPage.Controls.Add(this.proxiesRichTxtBox);
            this.proxiesTabPage.Controls.Add(this.loadProxiesPageTitle);
            this.proxiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.proxiesTabPage.Name = "proxiesTabPage";
            this.proxiesTabPage.Size = new System.Drawing.Size(744, 368);
            this.proxiesTabPage.TabIndex = 2;
            this.proxiesTabPage.Text = "Proxies";
            this.proxiesTabPage.UseVisualStyleBackColor = true;
            // 
            // formatProxyLab
            // 
            this.formatProxyLab.AutoSize = true;
            this.formatProxyLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formatProxyLab.ForeColor = System.Drawing.Color.Red;
            this.formatProxyLab.Location = new System.Drawing.Point(4, 45);
            this.formatProxyLab.Name = "formatProxyLab";
            this.formatProxyLab.Size = new System.Drawing.Size(177, 36);
            this.formatProxyLab.TabIndex = 26;
            this.formatProxyLab.Text = "Format is: ip:port:user:pw\r\n(user:pw are not required)";
            // 
            // browseProxiesBtn
            // 
            this.browseProxiesBtn.Depth = 0;
            this.browseProxiesBtn.Location = new System.Drawing.Point(559, 84);
            this.browseProxiesBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.browseProxiesBtn.Name = "browseProxiesBtn";
            this.browseProxiesBtn.Primary = true;
            this.browseProxiesBtn.Size = new System.Drawing.Size(137, 35);
            this.browseProxiesBtn.TabIndex = 25;
            this.browseProxiesBtn.Text = "LOAD PROXIES";
            this.browseProxiesBtn.UseVisualStyleBackColor = true;
            this.browseProxiesBtn.Click += new System.EventHandler(this.browseProxiesBtn_Click);
            // 
            // proxiesNeededLab
            // 
            this.proxiesNeededLab.AutoSize = true;
            this.proxiesNeededLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proxiesNeededLab.ForeColor = System.Drawing.Color.Red;
            this.proxiesNeededLab.Location = new System.Drawing.Point(555, 337);
            this.proxiesNeededLab.Name = "proxiesNeededLab";
            this.proxiesNeededLab.Size = new System.Drawing.Size(126, 18);
            this.proxiesNeededLab.TabIndex = 22;
            this.proxiesNeededLab.Text = "Proxies needed: 1";
            // 
            // proxiesRichTxtBox
            // 
            this.proxiesRichTxtBox.Location = new System.Drawing.Point(8, 84);
            this.proxiesRichTxtBox.Name = "proxiesRichTxtBox";
            this.proxiesRichTxtBox.Size = new System.Drawing.Size(504, 272);
            this.proxiesRichTxtBox.TabIndex = 21;
            this.proxiesRichTxtBox.Text = "";
            // 
            // loadProxiesPageTitle
            // 
            this.loadProxiesPageTitle.AutoSize = true;
            this.loadProxiesPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadProxiesPageTitle.ForeColor = System.Drawing.Color.Black;
            this.loadProxiesPageTitle.Location = new System.Drawing.Point(3, 8);
            this.loadProxiesPageTitle.Name = "loadProxiesPageTitle";
            this.loadProxiesPageTitle.Size = new System.Drawing.Size(200, 29);
            this.loadProxiesPageTitle.TabIndex = 18;
            this.loadProxiesPageTitle.Text = "Load/add proxies";
            // 
            // creationTabPage
            // 
            this.creationTabPage.BackColor = System.Drawing.Color.White;
            this.creationTabPage.Controls.Add(this.startProcessesChkBox);
            this.creationTabPage.Controls.Add(this.creationLogsRichTxtBox);
            this.creationTabPage.Controls.Add(this.creationPageTitle);
            this.creationTabPage.Controls.Add(this.createFoldersBtn);
            this.creationTabPage.Location = new System.Drawing.Point(4, 22);
            this.creationTabPage.Name = "creationTabPage";
            this.creationTabPage.Size = new System.Drawing.Size(744, 368);
            this.creationTabPage.TabIndex = 3;
            this.creationTabPage.Text = "Creation";
            // 
            // startProcessesChkBox
            // 
            this.startProcessesChkBox.AutoSize = true;
            this.startProcessesChkBox.Depth = 0;
            this.startProcessesChkBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.startProcessesChkBox.Location = new System.Drawing.Point(513, 224);
            this.startProcessesChkBox.Margin = new System.Windows.Forms.Padding(0);
            this.startProcessesChkBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.startProcessesChkBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.startProcessesChkBox.Name = "startProcessesChkBox";
            this.startProcessesChkBox.Ripple = true;
            this.startProcessesChkBox.Size = new System.Drawing.Size(176, 30);
            this.startProcessesChkBox.TabIndex = 22;
            this.startProcessesChkBox.Text = "Start bots after creation";
            this.startProcessesChkBox.UseVisualStyleBackColor = true;
            this.startProcessesChkBox.CheckedChanged += new System.EventHandler(this.startProcessesChkBox_CheckedChanged);
            // 
            // creationLogsRichTxtBox
            // 
            this.creationLogsRichTxtBox.Enabled = false;
            this.creationLogsRichTxtBox.Location = new System.Drawing.Point(9, 41);
            this.creationLogsRichTxtBox.Name = "creationLogsRichTxtBox";
            this.creationLogsRichTxtBox.Size = new System.Drawing.Size(466, 306);
            this.creationLogsRichTxtBox.TabIndex = 21;
            this.creationLogsRichTxtBox.Text = "";
            this.creationLogsRichTxtBox.TextChanged += new System.EventHandler(this.creationLogsRichTxtBox_TextChanged);
            // 
            // creationPageTitle
            // 
            this.creationPageTitle.AutoSize = true;
            this.creationPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creationPageTitle.ForeColor = System.Drawing.Color.Black;
            this.creationPageTitle.Location = new System.Drawing.Point(3, 8);
            this.creationPageTitle.Name = "creationPageTitle";
            this.creationPageTitle.Size = new System.Drawing.Size(160, 29);
            this.creationPageTitle.TabIndex = 20;
            this.creationPageTitle.Text = "Start creation:";
            // 
            // createFoldersBtn
            // 
            this.createFoldersBtn.Depth = 0;
            this.createFoldersBtn.Location = new System.Drawing.Point(513, 182);
            this.createFoldersBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.createFoldersBtn.Name = "createFoldersBtn";
            this.createFoldersBtn.Primary = true;
            this.createFoldersBtn.Size = new System.Drawing.Size(181, 35);
            this.createFoldersBtn.TabIndex = 19;
            this.createFoldersBtn.Text = "START CREATION";
            this.createFoldersBtn.UseVisualStyleBackColor = true;
            this.createFoldersBtn.Click += new System.EventHandler(this.createFoldersBtn_Click);
            // 
            // statsPage
            // 
            this.statsPage.Controls.Add(this.button1);
            this.statsPage.Controls.Add(this.statsTitleLab);
            this.statsPage.Location = new System.Drawing.Point(4, 22);
            this.statsPage.Name = "statsPage";
            this.statsPage.Size = new System.Drawing.Size(744, 368);
            this.statsPage.TabIndex = 4;
            this.statsPage.Text = "Stats";
            this.statsPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statsTitleLab
            // 
            this.statsTitleLab.AutoSize = true;
            this.statsTitleLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsTitleLab.ForeColor = System.Drawing.Color.Black;
            this.statsTitleLab.Location = new System.Drawing.Point(8, 10);
            this.statsTitleLab.Name = "statsTitleLab";
            this.statsTitleLab.Size = new System.Drawing.Size(72, 29);
            this.statsTitleLab.TabIndex = 21;
            this.statsTitleLab.Text = "Stats:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 516);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P4R4-PokeMob-Creator";
            this.materialTabControl1.ResumeLayout(false);
            this.foldersPage.ResumeLayout(false);
            this.foldersPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbFoldersNum)).EndInit();
            this.accsPage.ResumeLayout(false);
            this.accsPage.PerformLayout();
            this.proxiesTabPage.ResumeLayout(false);
            this.proxiesTabPage.PerformLayout();
            this.creationTabPage.ResumeLayout(false);
            this.creationTabPage.PerformLayout();
            this.statsPage.ResumeLayout(false);
            this.statsPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage foldersPage;
        private System.Windows.Forms.TabPage accsPage;
        private MaterialSkin.Controls.MaterialSingleLineTextField cfgFilePathTxt;
        private MaterialSkin.Controls.MaterialSingleLineTextField folderToPlace;
        private System.Windows.Forms.NumericUpDown nbFoldersNum;
        private MaterialSkin.Controls.MaterialSingleLineTextField botFolderTxt;
        private MaterialSkin.Controls.MaterialLabel dirForFoldersLab;
        private MaterialSkin.Controls.MaterialLabel necroFolderLab;
        private MaterialSkin.Controls.MaterialLabel nbFoldersLab;
        private MaterialSkin.Controls.MaterialRaisedButton cfgFileBrowse;
        private MaterialSkin.Controls.MaterialRaisedButton folderToPlaceBtn;
        private MaterialSkin.Controls.MaterialRaisedButton botFolderBrowseBtn;
        private System.Windows.Forms.RichTextBox accsRichTxtBox;
        private System.Windows.Forms.Label accountsNeededLab;
        private MaterialSkin.Controls.MaterialDivider foldersDivider;
        private System.Windows.Forms.Label accFormatLab;
        private System.Windows.Forms.Label accsTitleLab;
        private System.Windows.Forms.Label folderToCreateTitleLab;
        private System.Windows.Forms.Label foldersTitleLab;
        private MaterialSkin.Controls.MaterialCheckBox customConfigChkBox;
        private MaterialSkin.Controls.MaterialCheckBox defaultConfigChkBox;
        private MaterialSkin.Controls.MaterialRaisedButton browseAccsBtn;
        private System.Windows.Forms.TabPage proxiesTabPage;
        private System.Windows.Forms.Label loadProxiesPageTitle;
        private MaterialSkin.Controls.MaterialRaisedButton createFoldersBtn;
        private MaterialSkin.Controls.MaterialRaisedButton browseProxiesBtn;
        private System.Windows.Forms.Label proxiesNeededLab;
        private System.Windows.Forms.RichTextBox proxiesRichTxtBox;
        private System.Windows.Forms.TabPage creationTabPage;
        private System.Windows.Forms.Label creationPageTitle;
        private System.Windows.Forms.Label formatProxyLab;
        private System.Windows.Forms.RichTextBox creationLogsRichTxtBox;
        private System.Windows.Forms.TabPage statsPage;
        private System.Windows.Forms.Label statsTitleLab;
        private System.Windows.Forms.Button button1;
        private MaterialSkin.Controls.MaterialCheckBox startProcessesChkBox;
    }
}

