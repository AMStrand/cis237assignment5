//Author: Alyssa Strand
//CIS 237
//Assignment 5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the WineItemCollection class
            WineItemCollection wineItemCollection = new WineItemCollection();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        userInterface.DisplayImportSuccess();
                        break;

                    case 2:
                        //Print Entire List Of Items
                        string[] allItems = wineItemCollection.GetPrintStringsForAllItems();
                        if (allItems.Length > 0)
                        {
                            //Display all of the items
                            userInterface.DisplayAllItems(allItems);
                        }
                        else
                        {
                            //Display error message for all items
                            userInterface.DisplayAllItemsError();
                        }
                        break;

                    case 3:
                        //Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        string itemInformation = wineItemCollection.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 4:
                            // Add A New Item To The List
                        string[] newItemInformation = userInterface.GetItemInformation();
                            // Convert the price to a decimal:
                        decimal price = Convert.ToDecimal(newItemInformation[3]);
                            // Set a boolean to hold whether the item is active:
                        bool active = false;
                            // If the input was Y for yes, set active to true:
                        if (newItemInformation[4] == "Y")
                        {
                            active = true;
                        }
                        // Send info to WineItemCollection to add a new item:
                        if (wineItemCollection.AddNewItem(newItemInformation[0], newItemInformation[1], newItemInformation[2], price, active))
                        {
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;
                    case 5:
                        // Update an item in the list:
                        string idUpdate = userInterface.GetUpdateId();
                        if (wineItemCollection.UpdateBeverageItem(idUpdate))
                        {
                            userInterface.DisplayItemUpdateSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemUpdateError();
                        }
                        break;
                    case 6:
                        // Delete an item from the list:
                        string idDelete = userInterface.GetDeleteId();
                        if (wineItemCollection.DeleteBeverageItem(idDelete))
                        {
                            userInterface.DisplayItemDeleteSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemDeleteError();
                        }
                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

        }
    }
}
