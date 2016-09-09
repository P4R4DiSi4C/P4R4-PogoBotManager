//*******ROBOT ICON MADE BY: iconsmind.com from the Noun Project*********************//
using P4R4_PokeMob_Creator.Classes;
using System;
using System.IO;
using System.Windows.Forms;



namespace P4R4_PokeMob_Creator
{
    public partial class MainForm : MaterialSkin.Controls.MaterialForm
    {
        /// <summary>
        /// Store the "main" class here
        /// </summary>
        private PokeMobUtils _pokeMobUtils;

        /// <summary>
        /// Store the Folders class
        /// </summary>
        private Folders _folders;

        /// <summary>
        /// Store the accounts class
        /// </summary>
        private Accounts _accounts;

        /// <summary>
        /// Store the proxies class
        /// </summary>
        private Proxies _proxies;

        /// <summary>
        /// Property for the accs richtextbox
        /// </summary>
        internal RichTextBox _accsRichTxtBox { get { return accsRichTxtBox; } }

        /// <summary>
        /// Property for the proxies richtextbox
        /// </summary>
        internal RichTextBox _proxiesRichTxtBox { get { return proxiesRichTxtBox; } }

        /// <summary>
        /// Store the nb of needed accounts
        /// </summary>
        private int _neededAccounts = 1;

        /// <summary>
        /// Initialize main form and link the required classes
        /// </summary>
        /// <param name="pokeMobUtils">Get the "main" class</param>
        /// <param name="folders">Get the folders class</param>
        /// <param name="accounts">Get the accounts class</param>
        /// <param name="proxies">Get the proxies class</param>
        public MainForm(PokeMobUtils pokeMobUtils,Folders folders,Accounts accounts,Proxies proxies)
        {
            InitializeComponent();

            //Link each class
            _pokeMobUtils = pokeMobUtils;
            _pokeMobUtils.MainForm = this;
            _folders = folders;
            _folders.MainForm = this;
            _accounts = accounts;
            _accounts.MainForm = this;
            _proxies = proxies;
            _proxies.MainForm = this;
        }

        /// <summary>
        /// Code that will be executed when the create folders button is used
        /// </summary>
        public void createFoldersBtn_Click(object sender, EventArgs e)
        {
            //Call the method to start the creation
            _pokeMobUtils.startCreation();
        }

        /// <summary>
        /// Open a browser dialog to allow the user to choose his bot folder
        /// </summary>
        private void botFolderBrowseBtn_Click(object sender, EventArgs e)
        {
            //Initiate a new browserdialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //Set the description
            fbd.Description = "Select your PokeMobBot folder:";

            //Check if the user pressed ok
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _folders.CheckBotFolder(fbd.SelectedPath);
            }
        }

        /// <summary>
        /// Open a browser dialog to allow the user to choose the directory to place the new folders
        /// </summary>
        private void folderToPlaceBtn_Click(object sender, EventArgs e)
        {
            //Initiate a new browserdialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //Set the description
            fbd.Description = "Select a folder to place each bots folder:";

            //Check if the user pressed ok
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _folders.CheckFolderToPlace(fbd.SelectedPath);
            }
        }

        /// <summary>
        /// Open a browser file dialog to allow the user to select his config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cfgFileBrowse_Click(object sender, EventArgs e)
        {
            //Initiate a new browserdialog
            OpenFileDialog ofd = new OpenFileDialog();

            //Set the description
            ofd.Filter = "JSON file (*.json)|*.json";
            ofd.FilterIndex = 1;


            //Check if the user confirmed
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _folders.CheckCfgFile(ofd.FileName);
            }
        }

        /// <summary>
        /// Open a filedialog so the user can load a combobox with acc:password
        /// </summary>
        private void browseAccsBtn_Click(object sender, EventArgs e)
        {
            //Load a new openfiledialog
            OpenFileDialog ofd = new OpenFileDialog();

            //Set the description
            ofd.Filter = "Normal text file (*.txt)|*.txt";
            ofd.FilterIndex = 1;

            //Check if the user clicked ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Check if the user deleted some accounts before loading new ones
                _pokeMobUtils.checkManuallyDeleted(accsRichTxtBox.Lines, _accounts._verifiedAccounts);

                //Check the loaded accs from the file
                _accounts.verifyNewLoadedAccs(File.ReadAllLines(ofd.FileName), true);
            }
        }

        /// <summary>
        /// Open a filedialog so the user can load a file with proxies
        /// </summary>
        private void browseProxiesBtn_Click(object sender, EventArgs e)
        {
            //Load a new openfiledialog
            OpenFileDialog ofd = new OpenFileDialog();

            //Set the description
            ofd.Filter = "Normal text file (*.txt)|*.txt";
            ofd.FilterIndex = 1;

            //Check if the user clicked ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Check if the user deleted some proxies before loading new ones
                _pokeMobUtils.checkManuallyDeleted(proxiesRichTxtBox.Lines, _proxies._verifiedProxies);

                //Check the loaded accs from the file
                _proxies.verifyNewLoadedProxies(File.ReadAllLines(ofd.FileName), true);
            }
        }

        /// <summary>
        /// Update the nb of accounts needed
        /// </summary>
        private void nbFoldersNum_ValueChanged(object sender, EventArgs e)
        {
            //Doesn't let the user put 0 or lower than 0
            if (nbFoldersNum.Value <= 0)
            {
                //Set it to 1 by default
                nbFoldersNum.Value = 1;
            }

            //Update the accounts needed
            accountsNeededLab.Text = "Accounts needed: " + Convert.ToString(Convert.ToInt32(nbFoldersNum.Value));

            //Update the proxies needed
            proxiesNeededLab.Text = "Proxies needed: " + Convert.ToString(Convert.ToInt32(nbFoldersNum.Value));

            //Update the nb of accounts needed var
            _neededAccounts = Convert.ToInt32(nbFoldersNum.Value);
        }

        /// <summary>
        /// Method when custom config is checked
        /// </summary>
        private void customConfigChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(customConfigChkBox.Checked)
            {
                //Uncheck the other chkbox
                defaultConfigChkBox.Checked = false;

                //Enable the btn
                cfgFileBrowse.Enabled = true;

                //Set customconfig to true
                _folders.CustomConfig = true;

                //Simulate a browse btn click
                cfgFileBrowse_Click(sender,e);
            }
        }

        /// <summary>
        /// Method when default config is checked
        /// </summary>
        private void defaultConfigChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //Check if it has been checked
            if(defaultConfigChkBox.Checked)
            {
                //Uncheck the other chkbox
                customConfigChkBox.Checked = false;

                //Set the config file path to nothing
                _folders.ConfigFilePath = "";

                //Set the txtbox with filepath to empty
                cfgFilePathTxt.Text = "";

                //Disable the button
                cfgFileBrowse.Enabled = false;

                //Set customconfig to false
                _folders.CustomConfig = false;
            }
        }

        /// <summary>
        /// Method to check the checkbox if the user wants to start the bots when creation is finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startProcessesChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //If checked we set true to the boolean
            if (startProcessesChkBox.Checked)
            {
                _pokeMobUtils.ToStart = true;
            }
            else
            {
                //Else we set false to the boolean
                _pokeMobUtils.ToStart = false;
            }
        }

        /// <summary>
        /// Method trigered when a new log is entered
        /// </summary>
        private void creationLogsRichTxtBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            creationLogsRichTxtBox.SelectionStart = creationLogsRichTxtBox.Text.Length;
            // scroll it automatically
            creationLogsRichTxtBox.ScrollToCaret();
        }


        #region PUBLIC ACCESSOR METHODS

        /// <summary>
        /// Method to set a text to the cfgfilepath
        /// </summary>
        /// <param name="text">Get the text to input</param>
        public void SetCfgFilePathText(string text)
        {
            cfgFilePathTxt.Text = text;
        }

        /// <summary>
        /// Method to set a text to the foldertoplacetxt box
        /// </summary>
        /// <param name="text">Get the text to input</param>
        public void SetFolderToPlaceTxt(string text)
        {
            folderToPlace.Text = text;
        }

        /// <summary>
        /// Method to return the nb of folder to create
        /// </summary>
        /// <returns></returns>
        public int RequiredAccs()
        {
            return _neededAccounts;
        }

        /// <summary>
        /// Method to set the text of the botfolder textbox
        /// </summary>
        /// <param name="text">Get the text to input</param>
        public void SetBotFolderTxt(string text)
        {
            botFolderTxt.Text = text;
        }

        /// <summary>
        /// Method to check if customconfig is checked or not
        /// </summary>
        /// <returns></returns>
        public bool IsCustomConfig()
        {
            if (customConfigChkBox.Checked)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to append or clear logs
        /// </summary>
        /// <param name="textToAppend">Gets the textToAppend</param>
        /// <param name="toClear">Check if it's to clear the logs</param>
        public void CreationLogsActions(string textToAppend, bool toClear)
        {
            //Check if we clear or append text
            if (toClear)
            {
                creationLogsRichTxtBox.Clear();
            }
            else
            {
                MethodInvoker action = delegate
                {
                    creationLogsRichTxtBox.AppendText(textToAppend);
                };
                creationLogsRichTxtBox.BeginInvoke(action);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Stats stats = new Stats();
            stats.ListAllApplications();
        }


    }
}
