using MaterialSkin;
using P4R4_PogoBotsManager.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace P4R4_PogoBotsManager
{
    public partial class mainForm : MaterialSkin.Controls.MaterialForm
    {
        //Var for the main class
        private mainClass mainClass;

        /// <summary>
        /// Initialize main form and link the mainclass
        /// </summary>
        /// <param name="mainClass">Get the class</param>
        public mainForm(mainClass mainClass)
        {
            //Link the mainClass to this form
            this.mainClass = mainClass;

            //Link this form to the mainclass
            mainClass.MainForm = this;

            InitializeComponent();
        }

        /// <summary>
        /// Code that will be executed when the create folders button is used
        /// </summary>
        public void createFoldersBtn_Click(object sender, EventArgs e)
        {
            //Call the method to start the creation
            mainClass.startCreation();
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
                //Check if Bot.exe isn't present -> Invalid bot folder
                if (!File.Exists(fbd.SelectedPath + "\\" + mainClass.BOT_EXE_NAME))
                {
                    //Error invalid bot folder
                    MessageBox.Show("Not a valid PokeMobBot folder !");

                    //Set the botFolder var to empty string
                    mainClass.botFolder = string.Empty;

                    //Set the textbox text to empty
                    botFolderTxt.Text = "";
                }
                else
                {
                    //Check if folder Config is present
                    if (!Directory.Exists(fbd.SelectedPath + "\\config"))
                    {
                        //Error for invalid bot folder
                        MessageBox.Show("Not a valid PokeMobBot folder. Missing: Config folder");

                        //Set the botFolder var to empty string
                        mainClass.botFolder = string.Empty;

                        //Set the textbox text to empty
                        botFolderTxt.Text = "";
                    }
                    else
                    {
                        //Check if the bot already has config and auth file
                        if (File.Exists(fbd.SelectedPath + mainClass.CONFIG_FOLDER_NAME) || File.Exists(fbd.SelectedPath + mainClass.AUTH_FOLDER_NAME))
                        {
                            //Error if auth.json and config.json are already present
                            MessageBox.Show("Not a valid PokeMobBot folder. Remove: auth.json OR/AND config.json");

                            //Set the botFolder var to empty string
                            mainClass.botFolder = string.Empty;

                            //Set the textbox text to empty
                            botFolderTxt.Text = "";
                        }
                        else
                        {
                            //Get the selectedpath and set it to the variable and the textbox
                            mainClass.botFolder = fbd.SelectedPath;
                            botFolderTxt.Text = fbd.SelectedPath;

                            //Switch the boolean to true
                            mainClass.pathBooleans[mainClass.BOT_FOLDER_PATH] = true;

                            //Verify if all paths are set
                            mainClass.verifyPaths();
                        }
                    }
                }
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
                //*************NOT NEEDED SINCE THE USER HAS TO CHOOSE AN EXISTANT FOLDER OR CREATE IT FROM THE FolderBrowserDialog***************//
                //*************KEEP THIS CODE IF NEEDED LATER***********//

                ////Check if the directory exists, if not we create it
                //if(!Directory.Exists(fbd.SelectedPath))
                //{
                //    //Create the directory
                //    Directory.CreateDirectory(fbd.SelectedPath);
                //}

                //Get the selectedpath and set it to the variable and the textbox
                mainClass.dirToPlaceFolders = fbd.SelectedPath;

                //Set the var with the path
                folderToPlace.Text = fbd.SelectedPath;

                //Switch the boolean to true
                mainClass.pathBooleans[mainClass.DIR_TO_PLACE_FOLDERS] = true;

                //Verify if all paths are set
                mainClass.verifyPaths();
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
                //*********CHECK IF GOOD CFG FILE//
                //Open the json config file
                string jsonCfg = File.ReadAllText(ofd.FileName);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonCfg);

                //Check if its contains the DeviceSettings property
                if(jsonObj["DeviceSettings"] != null)
                {
                    //Set the path to the textbox
                    cfgFilePathTxt.Text = ofd.FileName;

                    //Set the var with the path
                    mainClass.configFilePath = ofd.FileName;

                    //Verify if all paths are set
                    mainClass.verifyPaths();
                }
                else
                {
                    //Set the path to the textbox
                    cfgFilePathTxt.Text = "";

                    //Set the var with the path
                    mainClass.configFilePath = "";

                    MessageBox.Show("Invalid PokeMobBot config file !");
                }
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
                //Check the loaded accs from the file
                mainClass.verifyNewLoadedAccs(File.ReadAllLines(ofd.FileName), true);
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
                //Check the loaded accs from the file
                mainClass.verifyNewLoadedProxies(File.ReadAllLines(ofd.FileName), true);
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
            mainClass.neededAccounts = Convert.ToInt32(nbFoldersNum.Value);
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
                mainClass.customConfig = true;

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
                mainClass.configFilePath = "";

                //Set the txtbox with filepath to empty
                cfgFilePathTxt.Text = "";

                //Disable the button
                cfgFileBrowse.Enabled = false;

                //Set customconfig to false
                mainClass.customConfig = false;
            }
        }
    }
}
