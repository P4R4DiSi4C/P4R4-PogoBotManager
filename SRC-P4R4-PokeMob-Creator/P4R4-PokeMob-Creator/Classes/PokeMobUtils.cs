using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using P4R4_PokeMob_Creator.Classes;
using System.Diagnostics;

namespace P4R4_PokeMob_Creator
{
    /// <summary>
    /// Main class of this project
    /// </summary>
    public class PokeMobUtils
    {
        /// <summary>
        /// Property to store the MainForm here
        /// </summary>
        public MainForm MainForm { private get; set; }

        /// <summary>
        /// Property to store the accounts class
        /// </summary>
        public Accounts Accounts { private get; set; }

        /// <summary>
        /// Property to store the proxies class
        /// </summary>
        public Proxies Proxies { private get; set; }

        /// <summary>
        /// Store the Folders class
        /// </summary>
        private Folders _folders;

        /// <summary>
        /// Boolean to see if the user wants to start the bots when creation is finished
        /// </summary>
        public bool ToStart { get; set; }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="folders">Get the folder class</param>
        public PokeMobUtils(Folders folders)
        {
            //Store the folders class
            _folders = folders;
            
            //Set a default value for the boolean
            ToStart = false;
        }

        /// <summary>
        /// Method to start the creation when the button is clicked
        /// </summary>
        public void startCreation()
        {
            //Instanciate the logs class to append logs to the richTxtBox
            Logger logger = new Logger(MainForm);

            //Clear the logs
            logger.ClearLogs();

            //Add verifying paths to log
            logger.AppendLog("Verifying paths...");

            //Call the method to check if all the path are filled
            if (!_folders.verifyPaths())
            {
                //Error for path/s missing
                MessageBox.Show("Path/s missing !");

                //Clear the logs
                logger.ClearLogs();

                //Exits the function
                return;
            }

            //Add verifications to richtextboxes to log
            logger.AppendLog("Verifying empties accounts/proxies lists...");

            //Check if the richtextboxes aren't empty
            if (MainForm._accsRichTxtBox.Text == "" || MainForm._proxiesRichTxtBox.Text == "")
            {
                //Error for empty accounts/proxies list
                MessageBox.Show("Empty accounts/proxies list !");

                //Clear the logs
                logger.ClearLogs();

                //Exits the function
                return;
            }

            //Add verifications to manually deleted accs/proxies to log
            logger.AppendLog("Checking for new manually added/deleted accounts/proxies...");

            //Check if the user deleted or added MANUALLY new accounts or proxies
            Accounts.verifyNewLoadedAccs(MainForm._accsRichTxtBox.Lines, false);
            Proxies.verifyNewLoadedProxies(MainForm._proxiesRichTxtBox.Lines, false);

            //Add check for sufficient accs and proxies to logs
            logger.AppendLog("Checking for sufficient accounts and proxies...");

            //Check if the verified accounts are sufficient to match the number of folders we need to create
            //Same thing for the proxies
            if ((Accounts._verifiedAccounts.Count < MainForm.RequiredAccs()) || (Proxies._verifiedProxies.Count() < MainForm.RequiredAccs()))
            {
                //Error for insufficient accounts
                MessageBox.Show("Please, ensure you loaded/added sufficient accounts/proxies.");

                //Clear the logs
                logger.ClearLogs();

                //Exits the function
                return;
            }

            //Add creation of folders to logs
            logger.AppendLog("Creating folders...");

            //Call the method to create the number of needed folders
            _folders.CreateFolders(MainForm.RequiredAccs(), logger);

            //Add parsing combolists to logs
            logger.AppendLog("Parsing account:password list...");

            //Parse the combolist(the list of verified accs:pw)
            string[,] accsPw = Accounts.parseCombolist();

            //Add filtering and clearing to the logs
            logger.AppendLog("Clearing and filtering proxies list...");

            //Filter and clear the proxieslist
            string[,] proxiesList = Proxies.clearAndFilterProxiesList();

            //Add creation of auth and config files to the logs
            logger.AppendLog("Creating auth.json and config.json files for each folder...");

            //Call the method to do the auth.json file for each bot folder with each of the accounts
            makeAuthAndRndCfg(accsPw, proxiesList);

            //Start the bots if the user specified it
            if (ToStart)
                StartBots();

            //Clear the array with the names of the created folders for each bot
            _folders.nameFolders.Clear();

            //Add creation done to logs
            logger.AppendLog("Done !");

            //Messagebox to alert the user of successfully creation
            MessageBox.Show("Successfully created: " + MainForm.RequiredAccs() + " folders !");
        }

        /// <summary>
        /// Method to check if the acc or proxy is a duplicate
        /// </summary>
        /// <param name="str">Get the string</param>
        /// /// <param name="listToCheck">Gets the list to check. proxies/accounts list</param>
        /// <returns>Return a boolean if the acc or proxy is a duplicate or not</returns>
        public bool checkIfAlreadyVerified(string str, List<string> listToCheck)
        {
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
        /// <param name="richTxtBox">Get the richtextbox to check the string</param>
        /// <param name="listToCheck">Get the list to check. Proxies/accounts</param>
        /// <returns>Return a boolean if the acc is a duplicate or not</returns>
        public void checkManuallyDeleted(string[] richTxtBox,List<string> listToCheck)
        {
            //General counter to remove from the list of acc/proxies without touching the i counter
            int counter = 0;

            //Store the size of the list we're checking
            //When we remove an item in the list they're moved in the list, so the size of the list changes and the loop doesn't see that, so we've to store it before the loop
            int listSize = listToCheck.Count();

            //Loop through the already verified accs/proxies list
            for (int i = 0; i < listSize; i++)
            {
                //Check if the acc in the list isn't present in the richtextbox
                if (!Array.Exists(richTxtBox, x => x == listToCheck[counter]))
                {
                    //Remove it from the list
                    listToCheck.RemoveAt(counter);
                }
                else
                {
                    //Increment the counter
                    counter++;
                }
            }
        }

        /// <summary>
        /// Method used to make the auth.json file for each botfolder with each of the verified accs
        /// </summary>
        /// <param name="accsPw">Get the array with the accs and passwords</param>
        public void makeAuthAndRndCfg(string[,] accsPw,string[,] proxiesList)
        {
            //Loop the number of entries in nameFolders list(Array with the name of the created folders)
            for (int i = 0; i < _folders.nameFolders.Count(); i++)
            {
                //********MAKE AUTH FILES***************//
                //Copy the auth.json file to each of the bot folders
                File.WriteAllBytes(_folders.DirToPlaceFolders + _folders.nameFolders[i] + Folders.AUTH_FOLDER_NAME, Properties.Resources.auth);

                //Save the auth.json file in a string
                string json = File.ReadAllText(_folders.DirToPlaceFolders + _folders.nameFolders[i] + Folders.AUTH_FOLDER_NAME);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

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
                jsonObj["ProxyUri"] = "" + proxiesList[i,0] + "";

                //Check if the proxies doesn't have user/pass, if they don't we'll keep the default value, if they do we set the user and pass.
                if (proxiesList[i, 1] != null && proxiesList[i, 2] != null)
                {
                    jsonObj["ProxyLogin"] = "" + proxiesList[i, 1] + "";
                    jsonObj["ProxyPass"] = "" + proxiesList[i, 2] + "";
                }


                //Convert back to json
                string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

                //Copy the file to a bot folder
                File.WriteAllText(_folders.DirToPlaceFolders + _folders.nameFolders[i] + Folders.AUTH_FOLDER_NAME, output);

                //******MODIFY CONFIG FILES WITH RANDOM DEVICE FINGERPRINT*******//
                //Generate a random device
                DeviceSettings device = new DeviceSettings();

                //Save the auth.json file in a string
                string jsonCfg = File.ReadAllText(_folders.DirToPlaceFolders + _folders.nameFolders[i] + Folders.CONFIG_FOLDER_NAME);
                dynamic jsonObjCfg = JsonConvert.DeserializeObject(jsonCfg);

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
                string outputCfg = JsonConvert.SerializeObject(jsonObjCfg, Newtonsoft.Json.Formatting.Indented);

                //Copy the file to a bot folder
                File.WriteAllText(_folders.DirToPlaceFolders + _folders.nameFolders[i] + Folders.CONFIG_FOLDER_NAME, outputCfg);
            }
        }

        /// <summary>
        /// Method to start each bots
        /// </summary>
        public void StartBots()
        {
            //Loop through each created folder and run the exe
            for(int i = 0; i < _folders.nameFolders.Count;i++)
            {
                Process.Start(_folders.DirToPlaceFolders + _folders.nameFolders[i] + _folders.nameFolders[i] + ".exe");
            }
        }
    }
}
