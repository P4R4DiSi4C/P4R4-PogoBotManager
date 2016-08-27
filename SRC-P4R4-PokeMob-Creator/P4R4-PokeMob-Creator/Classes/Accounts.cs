using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace P4R4_PokeMob_Creator.Classes
{
    public class Accounts
    {
        /// <summary>
        /// Property to store the MainForm here
        /// </summary>
        public MainForm MainForm { private get; set; }

        /// <summary>
        /// Property to store the pokemobutils class
        /// </summary>
        private PokeMobUtils _pokeMobUtils;

        //Regex to check if user entered right acc:pw or gmail:pw
        private Regex _ptcAccReg;
        private Regex _googleAccReg;

        /// <summary>
        /// Property for the verified accounts list
        /// </summary>
        internal List<string> _verifiedAccounts { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pokeMobUtils">Get the pokemobutils class</param>
        public Accounts(PokeMobUtils pokeMobUtils)
        {
            //Set the regexes
            _ptcAccReg = new Regex(@"^[a-zA-Z0-9_çéàüèöä+]{6,16}:+(.*){6,15}$");
            _googleAccReg = new Regex(@"(\W|^)[\w.+\-]*@gmail\.com:+(.*){8,37}$");

            //Init the list
            _verifiedAccounts = new List<string>();

            //Links the class
            _pokeMobUtils = pokeMobUtils;
            _pokeMobUtils.Accounts = this;
        }

        /// <summary>
        /// Method to check if the new manually added/loaded accs are valid and add/display them in the list/richtextbox.
        /// </summary>
        /// <param name="newAccs">Get an array of strings with the accs</param>
        /// <param name="autoLoaded">Boolean to check if added manually or not</param>
        public void verifyNewLoadedAccs(string[] newAccs, bool autoLoaded)
        {
            //Count to check the nb of the lines that doesn't match the regex
            int notFormatMatchCount = 0;

            //Counter for the new added accs/proxies
            int addedAccs = 0;

            //String to store the message to display in the richtextbox
            string loadedAccProx = "New " + (autoLoaded ? "loaded" : "manually added") + " accounts: ";

            //General counter for the loop
            int i = 0;

            //Bool to store the corresponding regex
            bool regToCheck = _googleAccReg.IsMatch(newAccs[i]) || _ptcAccReg.IsMatch(newAccs[i]);


            //Check each line in the array of strings
            for (i = 0; i < newAccs.Length; i++)
            {
                //Check if it matches is regex and if it's not empty
                if (!regToCheck || newAccs[i] == "")
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
                        _pokeMobUtils.checkManuallyDeleted(newAccs, _verifiedAccounts);
                    }

                    //Check if  it is a duplicate
                    if (!_pokeMobUtils.checkIfAlreadyVerified(newAccs[i], _verifiedAccounts))
                    {
                        //Add it to the list
                        _verifiedAccounts.Add(newAccs[i]);

                        //Increment the counter of added proxies/accounts
                        addedAccs++;
                    }
                }
            }
            //Clear the corresponding richtextbox
            MainForm._accsRichTxtBox.Clear();

            //Display the final list of proxies/accounts
            foreach (string str in _verifiedAccounts)
            {
                MainForm._accsRichTxtBox.AppendText(str + "\n");
            }

            //Check if we added more than 0 accounts/proxies
            if (addedAccs > 0)
            {
                //Display the message with the new added accs/proxies nb
                MessageBox.Show(loadedAccProx + addedAccs);
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
            string[,] accsPw = new string[MainForm.RequiredAccs(), 2];

            //Loop that will loop the neededAcconts var times
            for (int i = 0; i < MainForm.RequiredAccs(); i++)
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
    }
}
