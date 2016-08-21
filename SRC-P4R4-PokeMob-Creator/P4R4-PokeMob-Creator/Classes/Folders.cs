using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator.Classes
{
    public class Folders
    {
        //Properties

        /// <summary>
        /// Property to store the MainForm here
        /// </summary>
        public MainForm MainForm { get; set; }

        //Initialize a new list to save each account
        private List<string> _nameFolders;

        //Folder where bot is
        public string BotFolder { private get; set; }

        //Folder to place each bot folder
        public string DirToPlaceFolders { private get; set; }

        //Filepath to the config.json
        public string ConfigFilePath { private get; set; } = "";

        //Create Array of booleans for further verifications
        public bool[] PathBooleans { get; set; } = new bool[2];

        //Create a boolean to check if it's a custom config
        public bool CustomConfig { get; set; } = false;

        //Consts
        private const int DIR_TO_PLACE_FOLDERS = 1;
        private const int BOT_FOLDER_PATH = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Folders()
        {
            _nameFolders = new List<string>();
        }

        /// <summary>
        /// Method to check the selected botfolder
        /// </summary>
        /// <param name="path"></param>
        public void CheckBotFolder(string path)
        {
            //Check if Bot.exe isn't present -> Invalid bot folder
            if (!File.Exists(path + "\\" + PokeMobUtils.BOT_EXE_NAME))
            {
                //Error invalid bot folder
                MessageBox.Show("Not a valid PokeMobBot folder !");

                //Set the botFolder var to empty string
                BotFolder = string.Empty;

                //Set the textbox text to empty
                MainForm.botFolderTxt.Text = "";
            }
            else
            {
                //Check if folder Config is present
                if (!Directory.Exists(path + "\\config"))
                {
                    //Error for invalid bot folder
                    MessageBox.Show("Not a valid PokeMobBot folder. Missing: Config folder");

                    //Set the botFolder var to empty string
                    BotFolder = string.Empty;

                    //Set the textbox text to empty
                    MainForm.botFolderTxt.Text = "";
                }
                else
                {
                    //Check if the bot already has config and auth file
                    if (File.Exists(path + PokeMobUtils.CONFIG_FOLDER_NAME) || File.Exists(path + PokeMobUtils.AUTH_FOLDER_NAME))
                    {
                        //Error if auth.json and config.json are already present
                        MessageBox.Show("Not a valid PokeMobBot folder. Remove: auth.json OR/AND config.json");

                        //Set the botFolder var to empty string
                        BotFolder = string.Empty;

                        //Set the textbox text to empty
                        MainForm.botFolderTxt.Text = "";
                    }
                    else
                    {
                        //Get the selectedpath and set it to the variable and the textbox
                        BotFolder = path;
                        MainForm.botFolderTxt.Text = path;

                        //Switch the boolean to true
                        PathBooleans[BOT_FOLDER_PATH] = true;

                        //Verify if all paths are set
                        verifyPaths();
                    }
                }
            }
        }

        /// <summary>
        /// Method used to make the verifications we need before creating folders and files
        /// </summary>
        /// <returns>Return a boolean if all paths are set or not</returns>
        public bool verifyPaths()
        {
            //Loop through each boolean in the array of the path to check if we got all the needed paths
            foreach (bool path in PathBooleans)
            {
                //If one path is missing we display a messagebox and return false
                if (path == false || (MainForm.customConfigChkBox.Checked && configFilePath == string.Empty))
                {
                    //Return false
                    return false;
                }
            }

            //Check if it's a customconfig and if the filepath is blank
            if (CustomConfig && ConfigFilePath == "")
            {
                return false;
            }

            //Return true
            return true;
        }

    }
}
