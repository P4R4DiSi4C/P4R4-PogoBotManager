using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace P4R4_PogoBotsManager
{
    public class mainClass
    {
        //Var for the form
        private mainForm mainForm;

        /// <summary>
        /// Get and set the main form
        /// </summary>
        public mainForm MainForm
        {
            set { mainForm = value; }
        }

        //CONSTS
        public const string BOT_FOLDER_NAME = "\\PokeMobBot";
        public const string CONFIG_FOLDER_NAME = "\\config\\config.json";
        public const string BOT_EXE_NAME = "PokeMobBot.exe";
        public const string AUTH_FOLDER_NAME = "\\config\\auth.json";

        //Folder where bot is
        public string botFolder;

        //Int to save the accs needed
        public int neededAccounts = 1;

        //Folder to place each bot folder
        public string dirToPlaceFolders;

        //Filepath to the config.json
        public string configFilePath = "";

        //Regex to check if user entered right acc:pw
        Regex ptcAccReg = new Regex(@"^[a-zA-Z0-9_çéàüèöä+]{6,16}:+(.*){6,15}$");
        Regex googleAccReg = new Regex(@"(\W|^)[\w.+\-]*@gmail\.com:+(.*){8,37}$");

        //Regex for the proxy
        Regex proxyReg = new Regex(@"^((([^:]+):([^@]+))@)?((\d{1,3}\.){3}\d{1,3})(:(\d{1,5}))$");

        //Initialize a new list to save each account
        public List<string> verifiedAccounts = new List<string>();

        //Initialize a new list to save each proxy
        public List<string> verifiedProxies = new List<string>();

        //Initialize a new list to save each account
        public List<string> nameFolders = new List<string>();

        //Create constants and Array of booleans for further verifications
        public const int BOT_FOLDER_PATH = 0;
        public const int DIR_TO_PLACE_FOLDERS = 1;
        public bool customConfig = false;
        public bool[] pathBooleans = new bool[2];

        /// <summary>
        /// Method to start the creation when the button is clicked
        /// </summary>
        public void startCreation()
        {
            //Call the method to check if all the path are filled
            if (verifyPaths())
            {
                //Check if the richtextbox isn't empty
                if (mainForm.accsRichTxtBox.Text != "")
                {
                    //Check if we got any manual entered account to check and add to the list
                    verifyNewLoadedAccs(mainForm.accsRichTxtBox.Lines, false);

                    //Check if we got any manual entered account to check and add to the list
                    verifyNewLoadedProxies(mainForm.proxiesRichTxtBox.Lines, false);

                    //Check if the verified accounts are sufficient to match the number of folders we need to create
                    //Same thing for the proxies
                    if ((verifiedAccounts.Count() > 0 && verifiedAccounts.Count() >= neededAccounts) && (verifiedProxies.Count() > 0 && verifiedProxies.Count() >= neededAccounts))
                    {
                        //Call the method to create the number of needed folders
                        createFolders(Convert.ToInt32(mainForm.nbFoldersNum.Value));

                        //Parse the combolist(the list of verified accs:pw)
                        string[,] accsPw = parseCombolist();

                        //Call the method to do the auth.json file for each bot folder with each of the accounts
                        makeAuthAndRndCfg(accsPw,clearedProxiesList());

                        //Clear the array with the names of the created folders for each bot
                        nameFolders.Clear();
                    }
                    else
                    {
                        //Error for insufficient accounts
                        MessageBox.Show("Please, ensure you loaded/added sufficient accounts/proxies.");
                    }
                }
                else
                {
                    //Error for empty accounts list
                    MessageBox.Show("Empty accounts list !");
                }
            }
            else
            {
                //Error for path/s missing
                MessageBox.Show("Path/s missing !");
            }
        }

        /// <summary>
        /// Method to create the folders for the bots
        /// </summary>
        /// <param name="numberOfFolders">Get the number of folders to create</param>
        public void createFolders(int numberOfFolders)
        {
            //Loop to create the folders required
            for (int i = 1; i <= numberOfFolders; i++)
            {
                //Nb that will be added to the folder name if the folder already exists without broking the counter(i)
                //So if bot1 already exists we will increment this nb until we find a name of folder that is free to use.
                int folderNb = i;

                //Check if folder exists
                while (Directory.Exists(dirToPlaceFolders + BOT_FOLDER_NAME + folderNb))
                {
                    //Increment the counter
                    folderNb++;
                }
                //Create the folder
                Directory.CreateDirectory(dirToPlaceFolders + BOT_FOLDER_NAME + folderNb);

                //Add the name of the folder to the list
                nameFolders.Add(BOT_FOLDER_NAME + folderNb);

                //Copy folder structure from bot folder
                foreach (string sourceSubFolder in Directory.GetDirectories(botFolder, "*", SearchOption.AllDirectories))
                {
                    //Create the BotX directory
                    Directory.CreateDirectory(sourceSubFolder.Replace(botFolder, dirToPlaceFolders + BOT_FOLDER_NAME + folderNb));
                }

                //Copy bot subfolder and files
                foreach (string sourceFile in Directory.GetFiles(botFolder, "*", SearchOption.AllDirectories))
                {
                    string destinationFile = sourceFile.Replace(botFolder, dirToPlaceFolders + BOT_FOLDER_NAME + folderNb);
                    File.Copy(sourceFile, destinationFile, true);
                }

                //Rename each exe by adding the counter number to the name
                DirectoryInfo d = new DirectoryInfo(dirToPlaceFolders + BOT_FOLDER_NAME + folderNb);
                FileInfo[] infos = d.GetFiles(BOT_EXE_NAME);
                foreach (FileInfo f in infos)
                {
                    // Do the renaming here
                    File.Move(f.FullName, f.Directory.FullName + BOT_FOLDER_NAME + folderNb + f.Extension);
                }

                //Check if the user has chosen a custom config
                if (customConfig)
                {
                    //Copy the config file to each folder
                    File.Copy(configFilePath, dirToPlaceFolders + BOT_FOLDER_NAME + folderNb + CONFIG_FOLDER_NAME);
                }
                else
                {
                    //Copy the config.json file in resources if it doesn't exists -> custom config ?
                    File.WriteAllBytes(dirToPlaceFolders + BOT_FOLDER_NAME + folderNb + CONFIG_FOLDER_NAME, Properties.Resources.config);
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
            foreach (bool path in pathBooleans)
            {
                //If one path is missing we display a messagebox and return false
                if (path == false || (mainForm.customConfigChkBox.Checked && configFilePath == string.Empty))
                {
                    //Return false
                    return false;
                }
            }

            //Check if it's a customconfig and if the filepath is blank
            if (customConfig && configFilePath == "")
            {
                return false;
            }

            //Return true
            return true;
        }

        /// <summary>
        /// Method to check if the new manually added/loaded accs are valid and add/display them in the list/richtextbox.
        /// </summary>
        /// <param name="newAccs">Get an array of string</param>
        /// <param name="autoLoaded">Boolean to check if added manually or not</param>
        public void verifyNewLoadedAccs(string[] newAccs, bool autoLoaded)
        {
            //Count to check the nb of the lines that doesn't match the format acc:pw
            int notFormatMatchCount = 0;

            //Added accounts var
            int addedAccounts = 0;

            //Loop to go through each line and add to the list
            foreach (string line in newAccs)
            {
                //Check if the line match the format acc:pw
                if (googleAccReg.IsMatch(line) || ptcAccReg.IsMatch(line))
                {
                    if (!autoLoaded)
                    {
                        checkManuallyDeleted(newAccs,true);
                    }

                    //Check if acc is a duplicate
                    if (!checkIfAlreadyVerified(line,true))
                    {
                        //Add the acc to the list
                        verifiedAccounts.Add(line);

                        //Increment the addedAccounts var
                        addedAccounts++;
                    }
                }
                else
                {
                    //Increment the counter of not matched format accounts
                    notFormatMatchCount++;
                }
            }

            //Clear the richtextbox
            mainForm.accsRichTxtBox.Text = "";

            //Display the verified accs
            foreach (string str in verifiedAccounts)
            {
                mainForm.accsRichTxtBox.Text += str + "\n";
            }

            //If the accs were loaded with a file we display this message
            if (autoLoaded)
            {
                //Display the message with the new added accs nb
                MessageBox.Show("New loaded accounts: " + addedAccounts);
            }
            else
            {
                //If the accs were added manually and were verified we display this message
                if (addedAccounts > 0)
                {
                    //Display the message with the new added accs nb
                    MessageBox.Show("New manually added accounts: " + addedAccounts);
                }
            }
        }

        /// <summary>
        /// Method to check if the new manually added/loaded proxies are valid and add/display them in the list/richtextbox.
        /// </summary>
        /// <param name="newProxies">Get an array of string</param>
        /// <param name="autoLoaded">Boolean to check if added manually or not</param>
        public void verifyNewLoadedProxies(string[] newProxies, bool autoLoaded)
        {
            //Count to check the nb of the lines that doesn't match the format acc:pw
            int notFormatMatchCount = 0;

            //Added accounts var
            int addedProxies = 0;

            //Loop to go through each line and add to the list
            foreach (string line in newProxies)
            {
                //Check if the line match the format acc:pw
                if (proxyReg.IsMatch(line))
                {
                    if (!autoLoaded)
                    {
                        checkManuallyDeleted(newProxies,false);
                    }

                    //Check if acc is a duplicate
                    if (!checkIfAlreadyVerified(line,false))
                    {
                        //Add the acc to the list
                        verifiedProxies.Add(line);

                        //Increment the addedAccounts var
                        addedProxies++;
                    }
                }
                else
                {
                    //Increment the counter of not matched format accounts
                    notFormatMatchCount++;
                }
            }

            //Clear the richtextbox
            mainForm.proxiesRichTxtBox.Text = "";

            //Display the verified accs
            foreach (string str in verifiedProxies)
            {
                mainForm.proxiesRichTxtBox.Text += str + "\n";
            }

            //If the accs were loaded with a file we display this message
            if (autoLoaded)
            {
                //Display the message with the new added accs nb
                MessageBox.Show("New loaded proxies: " + addedProxies);
            }
            else
            {
                //If the accs were added manually and were verified we display this message
                if (addedProxies > 0)
                {
                    //Display the message with the new added accs nb
                    MessageBox.Show("New manually added proxies: " + addedProxies);
                }
            }
        }

        /// <summary>
        /// Method to check if the acc or proxy is a duplicate
        /// </summary>
        /// <param name="str">Get the string</param>
        /// /// <param name="isAccs">If true we check the accs list,else we check the proxies list</param>
        /// <returns>Return a boolean if the acc or proxy is a duplicate or not</returns>
        public bool checkIfAlreadyVerified(string str,bool isAccs)
        {
            List<string> listToCheck;
            if(isAccs)
            {
                listToCheck = verifiedAccounts;
            }
            else
            {
                listToCheck = verifiedProxies;
            }

            //Loop through the already verified accs
            for (int i = 0; i < listToCheck.Count(); i++)
            {
                //Check if its a duplicate
                if (str == listToCheck[i])
                {
                    //Return true if it's duplicate
                    return true;
                }
            }
            //return false if it's not a duplicate
            return false;
        }

        /// <summary>
        /// Method to check if any acc or proxy in the richtextbox has been manually deleted. If true we need to delete it from the verified accs list.
        /// </summary>
        /// <param name="richTxtBoxAcc">Get the acc:pw string</param>
        /// <param name="isAcces">Check if we are checking accounts or proxies</param>
        /// <returns>Return a boolean if the acc is a duplicate or not</returns>
        private void checkManuallyDeleted(string[] richTxtBoxAcc,bool isAccs)
        {
            List<string> listToCheck;

            if (isAccs)
            {
                listToCheck = verifiedAccounts;
            }
            else
            {
                listToCheck = verifiedProxies;
            }

            //Loop through the already verified accs
            for (int i = 0; i < listToCheck.Count(); i++)
            {
                //Check if the acc in the verifiedAccs array isn't present in the richtextbox
                if (!Array.Exists(richTxtBoxAcc, x => x == listToCheck[i]))
                {
                    //remove it from the list
                    listToCheck.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Method used to split the acc:pw list in an array
        /// </summary>
        public string[,] parseCombolist()
        {
            //Clear the richtextbox
            mainForm.accsRichTxtBox.Text = "";

            //Array to save each acc with it's password
            string[,] accsPw = new string[neededAccounts, 2];

            //Loop that will loop the neededAcconts var times
            for (int i = 0; i < neededAccounts; i++)
            {
                //Instances a tempArray for the splitted strings
                string[] tempArray = verifiedAccounts[0].Split(':');

                //Assign the acc password with the correct index
                accsPw[i, 0] = tempArray[0];
                accsPw[i, 1] = tempArray[1];

                //Remove it from the list(so it will only be used once)
                verifiedAccounts.RemoveAt(0);
            }

            //Display the new verifiedAccounts list without the used accs
            foreach (string str in verifiedAccounts)
            {
                mainForm.accsRichTxtBox.Text += str + "\n";
            }

            return accsPw;
        }

        /// <summary>
        /// Method to clear the proxies that we will use and just keep the ones that we didn't use
        /// </summary>
        /// <returns>Return a list with the final proxies to use</returns>
        public string[] clearedProxiesList()
        {
            //Declare an array
            string[] proxiesList = new string[neededAccounts];

            //Loop the nb of needed accounts var and add a proxy to the array. Delete the original proxy from his list
            for(int i = 0; i < neededAccounts;i++)
            {
                proxiesList[i] = verifiedProxies[0];
                verifiedProxies.RemoveAt(0);
            }

            return proxiesList;
        }

        /// <summary>
        /// Method used to make the auth.json file for each botfolder with each of the verified accs
        /// </summary>
        /// <param name="accsPw">Get the array with the accs and passwords</param>
        public void makeAuthAndRndCfg(string[,] accsPw,string[] proxiesList)
        {
            //Clear the proxies richtextbox
            mainForm.proxiesRichTxtBox.Text = "";

            //Loop the number of entries in nameFolders list(Array with the name of the created folders)
            for (int i = 0; i < nameFolders.Count(); i++)
            {
                //********MAKE AUTH FILES***************//
                //Copy the auth.json file to each of the bot folders
                File.WriteAllBytes(dirToPlaceFolders + nameFolders[i] + AUTH_FOLDER_NAME, Properties.Resources.auth);

                //Save the auth.json file in a string
                string json = File.ReadAllText(dirToPlaceFolders + nameFolders[i] + AUTH_FOLDER_NAME);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                //Check wether is a google account or a PTC acc
                if (accsPw[i, 0].Contains("@gmail.com"))
                {
                    //Set authtype, username and password in the auth.json
                    jsonObj["AuthType"] = "google";
                    jsonObj["GoogleUsername"] = "" + accsPw[i, 0] + "";
                    jsonObj["GooglePassword"] = "" + accsPw[i, 1] + "";
                }
                else
                {
                    //Set the authtype, username and password in the auth.json
                    jsonObj["AuthType"] = "ptc";
                    jsonObj["PtcUsername"] = "" + accsPw[i, 0] + "";
                    jsonObj["PtcPassword"] = "" + accsPw[i, 1] + "";
                }

                //Set the proxies
                jsonObj["UseProxy"] = true;
                jsonObj["ProxyUri"] = "" + proxiesList[i] + "";


                //Convert back to json
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

                //Copy the file to a bot folder
                File.WriteAllText(dirToPlaceFolders + nameFolders[i] + AUTH_FOLDER_NAME, output);

                //******MODIFY CONFIG FILES WITH RANDOM DEVICE FINGERPRINT*******//
                //Generate a random device
                DeviceSettings device = new DeviceSettings();

                //Save the auth.json file in a string
                string jsonCfg = File.ReadAllText(dirToPlaceFolders + nameFolders[i] + CONFIG_FOLDER_NAME);
                dynamic jsonObjCfg = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonCfg);

                //Set the username and password in the auth.json
                jsonObjCfg["DeviceSettings"]["DeviceId"] = device.DeviceId;
                jsonObjCfg["DeviceSettings"]["AndroidBoardName"] = device.AndroidBoardName;
                jsonObjCfg["DeviceSettings"]["AndroidBootLoader"] = device.AndroidBootLoader;
                jsonObjCfg["DeviceSettings"]["DeviceBrand"] = device.DeviceBrand;
                jsonObjCfg["DeviceSettings"]["DeviceModel"] = device.DeviceModel;
                jsonObjCfg["DeviceSettings"]["DeviceModelIdentifier"] = device.DeviceModelIdentifier;
                jsonObjCfg["DeviceSettings"]["DeviceModelBoot"] = device.DeviceModelBoot;
                jsonObjCfg["DeviceSettings"]["HardwareManufacturer"] = device.HardwareManufacturer;
                jsonObjCfg["DeviceSettings"]["HardWareModel"] = device.HardWareModel;
                jsonObjCfg["DeviceSettings"]["FirmwareBrand"] = device.FirmwareBrand;
                jsonObjCfg["DeviceSettings"]["FirmwareTags"] = device.FirmwareTags;
                jsonObjCfg["DeviceSettings"]["FirmwareType"] = device.FirmwareType;
                jsonObjCfg["DeviceSettings"]["FirmwareFingerprint"] = device.FirmwareFingerprint;

                //Convert back to json
                string outputCfg = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObjCfg, Newtonsoft.Json.Formatting.Indented);

                //Copy the file to a bot folder
                File.WriteAllText(dirToPlaceFolders + nameFolders[i] + CONFIG_FOLDER_NAME, outputCfg);
            }

            //Write the proxies that we didn't use to the textbox
            foreach (string str in verifiedProxies)
            {
                mainForm.proxiesRichTxtBox.Text += str + "\n";
            }
        }
    }
}
