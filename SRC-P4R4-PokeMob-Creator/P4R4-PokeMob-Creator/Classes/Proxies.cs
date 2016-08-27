using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator.Classes
{
    public class Proxies
    {
        /// <summary>
        /// Property to store the MainForm
        /// </summary>
        public MainForm MainForm { private get; set; }

        /// <summary>
        /// Property to store the pokemobutils class
        /// </summary>
        private PokeMobUtils _pokeMobUtils;

        //Regexes for the proxies
        private Regex _proxyReg;
        private Regex _proxyUserPwReg;

        /// <summary>
        /// List to store the verified proxies
        /// </summary>
        internal List<string> _verifiedProxies { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pokeMobUtils">Get the pokemobutils class</param>
        public Proxies(PokeMobUtils pokeMobUtils)
        {
            //Set the regexes
            _proxyReg = new Regex(@"^((([^:]+):([^@]+))@)?((\d{1,3}\.){3}\d{1,3})(:(\d{1,5}))$");
            _proxyUserPwReg = new Regex(@"^((([^:]+):([^@]+))@)?((\d{1,3}\.){3}\d{1,3})(:(\d{1,5})):+[a-zA-Z0-9_-]{5,30}:+(.*){5,30}$");

            //Set the new list
            _verifiedProxies = new List<string>();

            //Links the class
            _pokeMobUtils = pokeMobUtils;
            _pokeMobUtils.Proxies = this;
        }

        /// <summary>
        /// Method to filter out the proxies with user:pw and without, and keep only the ones that we'll not be using, for further use.
        /// </summary>
        /// <returns>Return a list with the final proxies to use</returns>
        public string[,] clearAndFilterProxiesList()
        {
            //Clear the richTxtBox
            MainForm._proxiesRichTxtBox.Clear();

            //Array to save each acc with it's password
            string[,] proxiesWithUserPw = new string[MainForm.RequiredAccs(), 3];

            //Declare an array to temporary place the proxies that we'll use
            string[] tempProxiesList = new string[MainForm.RequiredAccs()];

            //Get the range of proxies that we'll use, copy them to the proxiesList, then delete them from the verified proxies list
            _verifiedProxies.CopyTo(0, tempProxiesList, 0, tempProxiesList.Length);
            _verifiedProxies.RemoveRange(0, tempProxiesList.Length);

            //Counter to don't touch the "i" counter and avoid being out of index
            int counter = 0;

            //Loop that will loop the neededAcconts var times
            for (int i = 0; i < MainForm.RequiredAccs(); i++)
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
        /// Method to check if the new manually added/loaded proxies/accs are valid and add/display them in the list/richtextbox.
        /// </summary>
        /// <param name="newProxies">Get an array of string</param>
        /// <param name="autoLoaded">Boolean to check if added manually or not</param>
        public void verifyNewLoadedProxies(string[] newProxies, bool autoLoaded)
        {
            //Count to check the nb of the lines that doesn't match the regex
            int notFormatMatchCount = 0;

            //Counter for the new added proxies
            int addedProxies = 0;

            //String to store the message to display in the richtextbox
            string loadedAccProx = "New " + (autoLoaded ? "loaded" : "manually added") + " proxies: ";

            //General counter for the loop
            int i = 0;

            //Bool to store the corresponding regex
            bool regToCheck = _proxyReg.IsMatch(newProxies[i]) || _proxyUserPwReg.IsMatch(newProxies[i]);

            //Check each line in the array of strings
            for (i = 0; i < newProxies.Length; i++)
            {
                //Check if it matches is regex and if it's not empty
                if (!regToCheck || newProxies[i] == "")
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
                        _pokeMobUtils.checkManuallyDeleted(newProxies, _verifiedProxies);
                    }

                    //Check if  it is a duplicate
                    if (!_pokeMobUtils.checkIfAlreadyVerified(newProxies[i], _verifiedProxies))
                    {
                        //Add it to the list
                        _verifiedProxies.Add(newProxies[i]);

                        //Increment the counter of added proxies/accounts
                        addedProxies++;
                    }
                }
            }
            //Clear the corresponding richtextbox
            MainForm._proxiesRichTxtBox.Clear();

            //Display the final list of proxies
            foreach (string str in _verifiedProxies)
            {
                MainForm._proxiesRichTxtBox.AppendText(str + "\n");
            }

            //Check if we added more than 0 accounts/proxies
            if (addedProxies > 0)
            {
                //Display the message with the new added accs/proxies nb
                MessageBox.Show(loadedAccProx + addedProxies);
            }
        }
    }
}
