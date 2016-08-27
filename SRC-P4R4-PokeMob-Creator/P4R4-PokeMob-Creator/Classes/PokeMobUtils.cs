using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using P4R4_PokeMob_Creator.Classes;

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
        /// Store the Folders class
        /// </summary>
        private Folders _folders;

        //Regex to check if user entered right acc:pw
        private Regex _ptcAccReg;
        private Regex _googleAccReg;
        //Regexes for the proxies
        private Regex _proxyReg;
        private Regex _proxyUserPwReg;

        //Int to save the accs needed with a default value of -> 1
        public int NeededAccounts { private get; set; } = 1;

        //Initialize a new list to save each account
        private List<string> _verifiedAccounts;

        //Initialize a new list to save each proxy
        private List<string> _verifiedProxies;


        /// <summary>
        /// Default constructor of the class
        /// </summary>
        public PokeMobUtils(Folders folders)
        {
            //Set the regexes
            _ptcAccReg = new Regex(@"^[a-zA-Z0-9_çéàüèöä+]{6,16}:+(.*){6,15}$");
            _googleAccReg = new Regex(@"(\W|^)[\w.+\-]*@gmail\.com:+(.*){8,37}$");
            _proxyReg = new Regex(@"^((([^:]+):([^@]+))@)?((\d{1,3}\.){3}\d{1,3})(:(\d{1,5}))$");
            _proxyUserPwReg = new Regex(@"^((([^:]+):([^@]+))@)?((\d{1,3}\.){3}\d{1,3})(:(\d{1,5})):+[a-zA-Z0-9_-]{5,30}:+(.*){5,30}$");

            //Set the new list for each lists
            _verifiedAccounts = new List<string>();
            _verifiedProxies = new List<string>();

            //Store the folders class
            _folders = folders;
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
            verifyNewLoadedProxAcc(MainForm._accsRichTxtBox.Lines, false, true);
            verifyNewLoadedProxAcc(MainForm._proxiesRichTxtBox.Lines, false, false);

            //Add check for sufficient accs and proxies to logs
            logger.AppendLog("Checking for sufficient accounts and proxies...");

            //Check if the verified accounts are sufficient to match the number of folders we need to create
            //Same thing for the proxies
            if ((_verifiedAccounts.Count() < NeededAccounts) || (_verifiedProxies.Count() < NeededAccounts))
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
            _folders.CreateFolders(MainForm.GetNbFolders(), logger);

            //Add parsing combolists to logs
            logger.AppendLog("Parsing account:password list...");

            //Parse the combolist(the list of verified accs:pw)
            string[,] accsPw = parseCombolist();

            //Add filtering and clearing to the logs
            logger.AppendLog("Clearing and filtering proxies list...");

            //Filter and clear the proxieslist
            string[,] proxiesList = clearAndFilterProxiesList();

            //Add creation of auth and config files to the logs
            logger.AppendLog("Creating auth.json and config.json files for each folder...");

            //Call the method to do the auth.json file for each bot folder with each of the accounts
            makeAuthAndRndCfg(accsPw, proxiesList);

            //Clear the array with the names of the created folders for each bot
            _folders.nameFolders.Clear();

            //Add creation done to logs
            logger.AppendLog("Done !");

            //Messagebox to alert the user of successfully creation
            MessageBox.Show("Successfully created: " + NeededAccounts + " folders !");
        }

        /// <summary>
        /// Method to check if the new manually added/loaded proxies/accs are valid and add/display them in the list/richtextbox.
        /// </summary>
        /// <param name="newProxies">Get an array of string</param>
        /// <param name="autoLoaded">Boolean to check if added manually or not</param>
        public void verifyNewLoadedProxAcc(string[] newProxAcc, bool autoLoaded, bool isAccs)
        {
            //Count to check the nb of the lines that doesn't match the regex
            int notFormatMatchCount = 0;

            //Counter for the new added accs/proxies
            int addedProxAcc = 0;

            //String to store the message to display in the richtextbox
            string loadedAccProx = "";

            //Richtextbox to assing the corresponding richtextbox later(if it's the accs richtxtbox or the proxies one)
            RichTextBox richTxtBox;

            //List to assing the corresponding list later(if it's the accsList or the proxies ones)
            List<string> listToUse;

            //Bool to store the corresponding regex later(accs regex or proxies regex)
            bool regToCheck;

            //General counter for the loop
            int i = 0;

            /////////*********FAIRE TABLEAU ****************//

            //Check if we're checking accs
            if(isAccs)
            {
                //Assing the accs regex
                regToCheck = _googleAccReg.IsMatch(newProxAcc[i]) || _ptcAccReg.IsMatch(newProxAcc[i]);

                //Assing the accs richtxtbox
                richTxtBox = MainForm._accsRichTxtBox;

                //Assign the list to use
                listToUse = _verifiedAccounts;

                //Check if the accounts are loaded or manually added to assign the corresponding message
                loadedAccProx = "New"+ (autoLoaded?"loaded" : "manually added") + "accounts: ";
            }
            else
            {
                //Assign the proxies regex
                regToCheck = _proxyReg.IsMatch(newProxAcc[i]) || _proxyUserPwReg.IsMatch(newProxAcc[i]);

                //Assign the proxies richtxtbox
                richTxtBox = MainForm._proxiesRichTxtBox;

                //Assign the list to use
                listToUse = _verifiedProxies;

                //Check if the proxies are loaded or manually added to assign the corresponding message
                loadedAccProx = "New" + (autoLoaded ? "loaded" : "manually added") + "proxies: ";
            }


            //Check each line in the array of strings
            for(i = 0; i < newProxAcc.Length;i++)
            {
                //Check if it matches is regex and if it's not empty
                if(!regToCheck || newProxAcc[i] == "")
                {
                    //Increment the counter of not matched format accounts
                    notFormatMatchCount++;
                }
                else
                {
                    //Check if's manually added
                    if (!autoLoaded)
                    {
                        //Check if the user deleted manually some proxies or accounts
                        checkManuallyDeleted(newProxAcc, isAccs);
                    }

                    //Check if  it is a duplicate
                    if (!checkIfAlreadyVerified(newProxAcc[i], listToUse))
                    {
                        //Add it to the list
                        listToUse.Add(newProxAcc[i]);

                        //Increment the counter of added proxies/accounts
                        addedProxAcc++;
                    }
                }
            }
            //Clear the corresponding richtextbox
            richTxtBox.Clear();

            //Display the final list of proxies/accounts
            foreach (string str in listToUse)
            {
                richTxtBox.AppendText(str + "\n");
            }

            //Check if we added more than 0 accounts/proxies
            if (addedProxAcc > 0)
            {
                //Display the message with the new added accs/proxies nb
                MessageBox.Show(loadedAccProx + addedProxAcc);
            }
        }

        /// <summary>
        /// Method to check if the acc or proxy is a duplicate
        /// </summary>
        /// <param name="str">Get the string</param>
        /// /// <param name="isAccs">If true we check the accs list,else we check the proxies list</param>
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
        /// <param name="richTxtBoxAcc">Get the acc:pw string</param>
        /// <param name="isAcces">Check if we are checking accounts or proxies</param>
        /// <returns>Return a boolean if the acc is a duplicate or not</returns>
        public void checkManuallyDeleted(string[] richTxtBoxAcc,bool isAccs)
        {
            //List to store the corresponding list later
            List<string> listToCheck;

            //Check if we're checking accounts
            if (isAccs)
            {
                //Assign the corresponding list
                listToCheck = _verifiedAccounts;
            }
            else
            {
                //Assign the corresponding list
                listToCheck = _verifiedProxies;
            }

            //General counter to remove from the list of acc/proxies without touching the i counter
            int counter = 0;

            //Store the size of the list we're checking
            //When we remove an item in the list they're moved in the list, so the size of the list changes and the loop doesn't see that, so we've to store it before the loop
            int listSize = listToCheck.Count();

            //Loop through the already verified accs/proxies list
            for (int i = 0; i < listSize; i++)
            {
                //Check if the acc in the list isn't present in the richtextbox
                if (!Array.Exists(richTxtBoxAcc, x => x == listToCheck[counter]))
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
        /// Method used to split the acc:pw list in an array
        /// </summary>
        public string[,] parseCombolist()
        {
            //Clear the richtextbox
            MainForm._accsRichTxtBox.Clear();

            //Array to save each acc with it's password
            string[,] accsPw = new string[NeededAccounts, 2];

            //Loop that will loop the neededAcconts var times
            for (int i = 0; i < NeededAccounts; i++)
            {
                //Instances a tempArray for the splitted strings
                string[] tempArray = _verifiedAccounts[0].Split(':');

                //Assign the acc password with the correct index
                accsPw[i, 0] = tempArray[0];
                accsPw[i, 1] = tempArray[1];

                //Remove it from the list(so it will only be used once)
                _verifiedAccounts.RemoveAt(0);
            }

            //Display the new verifiedAccounts list without the used accs
            foreach (string str in _verifiedAccounts)
            {
                MainForm._accsRichTxtBox.AppendText(str + "\n");
            }

            return accsPw;
        }

        /// <summary>
        /// Method to filter out the proxies with user:pw and withouth, and keep only the ones that we'll not use for further use.
        /// </summary>
        /// <returns>Return a list with the final proxies to use</returns>
        public string[,] clearAndFilterProxiesList()
        {
            //Clear the richTxtBox
            MainForm._proxiesRichTxtBox.Clear();

            //Array to save each acc with it's password
            string[,] proxiesWithUserPw = new string[NeededAccounts, 3];

            //Declare an array to temporary place the proxies that we'll use
            string[] tempProxiesList = new string[NeededAccounts];

            //Get the range of proxies that we'll use, copy them to the proxiesList, then delete them from the verified proxies list
            _verifiedProxies.CopyTo(0, tempProxiesList, 0, tempProxiesList.Length);
            _verifiedProxies.RemoveRange(0, tempProxiesList.Length);

            //Counter to don't touch the "i" counter and avoid being out of index
            int counter = 0;

            //Loop that will loop the neededAcconts var times
            for (int i = 0; i < NeededAccounts; i++)
            {
                //Check if the proxy has a user and pw
                if (_proxyUserPwReg.IsMatch(tempProxiesList[i]))
                {

                    //Instances a tempArray for the splitted strings
                    string[] tempArray = tempProxiesList[i].Split(':');

                    //Assign the proxy,account and password with the correct indexes
                    proxiesWithUserPw[counter, 0] = tempArray[0] + ":" + tempArray[1];
                    proxiesWithUserPw[counter, 1] = tempArray[2];
                    proxiesWithUserPw[counter, 2] = tempArray[3];
                    counter++;
                }
                else
                {
                    //Assign the proxy and port + null to username and password(default values)
                    proxiesWithUserPw[counter, 0] = tempProxiesList[i];
                    proxiesWithUserPw[counter, 1] = null;
                    proxiesWithUserPw[counter, 2] = null;
                    counter++;
                }
            }

            //Write the proxies that we didn't use to the textbox
            foreach (string str in _verifiedProxies)
            {
                MainForm._proxiesRichTxtBox.Text += str + "\n";
            }

            return proxiesWithUserPw;
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
    }
}
