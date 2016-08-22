using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator.Classes
{
    public class Folders
    {
        /// <summary>
        /// Constants
        /// </summary>
        public const string BOT_FOLDER_NAME = "\\PokeMobBot";
        public const string CONFIG_FOLDER_NAME = "\\config\\config.json";
        public const string BOT_EXE_NAME = "PokeMobBot.exe";
        public const string AUTH_FOLDER_NAME = "\\config\\auth.json";
        private const int DIR_TO_PLACE_FOLDERS = 1;
        private const int BOT_FOLDER_PATH = 0;

        /// <summary>
        /// Property to store the MainForm here
        /// </summary>
        public MainForm MainForm { get; set; }

        //Initialize a new list to save each name of created folder
        public List<string> nameFolders;

        //Folder where bot is
        public string BotFolder { private get; set; }

        //Folder to place each bot folder
        public string DirToPlaceFolders { get; set; }

        //Filepath to the config.json
        public string ConfigFilePath { get; set; } = "";

        //Create Array of booleans for further verifications
        public bool[] PathBooleans { get; set; } = new bool[2];

        //Create a boolean to check if it's a custom config
        public bool CustomConfig { get; set; } = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Folders()
        {
            nameFolders = new List<string>();
        }

        /// <summary>
        /// Method to check the selected botfolder
        /// </summary>
        /// <param name="path">Get the path</param>
        public void CheckBotFolder(string path)
        {
            //Check if Bot.exe isn't present -> Invalid bot folder
            if (!File.Exists(path + "\\" + BOT_EXE_NAME))
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
                    if (File.Exists(path + CONFIG_FOLDER_NAME) || File.Exists(path + AUTH_FOLDER_NAME))
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
        /// Method to check the folder to place
        /// </summary>
        /// <param name="path">Get the path</param>
        public void CheckFolderToPlace(string path)
        {
            //Get the selectedpath and set it to the variable and the textbox
            DirToPlaceFolders = path;

            //Set the var with the path
            MainForm.folderToPlace.Text = path;

            //Switch the boolean to true
            PathBooleans[DIR_TO_PLACE_FOLDERS] = true;

            //Verify if all paths are set
            verifyPaths();
        }

        public void CheckCfgFile(string path)
        {
            //*********CHECK IF GOOD CFG FILE//
            //Open the json config file
            string jsonCfg = File.ReadAllText(path);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonCfg);

            //Check if its contains the DeviceSettings property
            if (jsonObj["DeviceSettings"] != null)
            {
                //Set the path to the textbox
                MainForm.cfgFilePathTxt.Text = path;

                //Set the var with the path
                ConfigFilePath = path;

                //Verify if all paths are set
                verifyPaths();
            }
            else
            {
                //Set the path to the textbox
                MainForm.cfgFilePathTxt.Text = "";

                //Set the var with the path
                ConfigFilePath = "";

                MessageBox.Show("Invalid PokeMobBot config file !");
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
                if (path == false || (MainForm.customConfigChkBox.Checked && ConfigFilePath == string.Empty))
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

        /// <summary>
        /// Method to create the folders for the bots
        /// </summary>
        /// <param name="numberOfFolders">Get the number of folders to create</param>
        public void CreateFolders(int numberOfFolders, Logger logClass)
        {
            //Loop to create the folders required
            for (int i = 1; i <= numberOfFolders; i++)
            {
                //Nb that will be added to the folder name if the folder already exists without broking the counter(i)
                //So if bot1 already exists we will increment this nb until we find a name of folder that is free to use.
                int folderNb = i;

                //Check if folder exists
                while (Directory.Exists(DirToPlaceFolders + BOT_FOLDER_NAME + folderNb))
                {
                    //Increment the counter
                    folderNb++;
                }
                //Create the folder
                Directory.CreateDirectory(DirToPlaceFolders + BOT_FOLDER_NAME + folderNb);

                //Add an entry in the log for the created folder
                logClass.AppendLog("Created: " + DirToPlaceFolders + BOT_FOLDER_NAME + folderNb);

                //Add the name of the folder to the list
                nameFolders.Add(BOT_FOLDER_NAME + folderNb);

                //Copy folder structure from bot folder
                foreach (string sourceSubFolder in Directory.GetDirectories(BotFolder, "*", SearchOption.AllDirectories))
                {
                    //Create the BotX directory
                    Directory.CreateDirectory(sourceSubFolder.Replace(BotFolder, DirToPlaceFolders + BOT_FOLDER_NAME + folderNb));
                }

                //Copy bot subfolder and files
                foreach (string sourceFile in Directory.GetFiles(BotFolder, "*", SearchOption.AllDirectories))
                {
                    string destinationFile = sourceFile.Replace(BotFolder, DirToPlaceFolders + BOT_FOLDER_NAME + folderNb);
                    File.Copy(sourceFile, destinationFile, true);
                }

                //Rename each exe by adding the counter number to the name
                DirectoryInfo d = new DirectoryInfo(DirToPlaceFolders + BOT_FOLDER_NAME + folderNb);
                FileInfo[] infos = d.GetFiles(BOT_EXE_NAME);
                foreach (FileInfo f in infos)
                {
                    // Do the renaming here
                    File.Move(f.FullName, f.Directory.FullName + BOT_FOLDER_NAME + folderNb + f.Extension);
                }

                //Check if the user has chosen a custom config
                if (CustomConfig)
                {
                    //Copy the config file to each folder
                    File.Copy(ConfigFilePath, DirToPlaceFolders + BOT_FOLDER_NAME + folderNb + CONFIG_FOLDER_NAME);
                }
                else
                {
                    //Copy the config.json file in resources if it doesn't exists -> custom config ?
                    File.WriteAllBytes(DirToPlaceFolders + BOT_FOLDER_NAME + folderNb + CONFIG_FOLDER_NAME, Properties.Resources.config);
                }
            }
        }
    }
}
