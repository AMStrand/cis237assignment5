//Author: Alyssa Strand
//CIS 237
//Assignment 5
// This class handles the interactions with the database.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class WineItemCollection : IWineCollection
    {
            // Variable to hold the entities:
        BeverageAMahlerEntities beverageEntities;
            // Variable to hold the length:
        int wineItemsLength;
            // User interface object:
        UserInterface userInterface = new UserInterface();

        //Constuctor. 
        public WineItemCollection()
        {
                // Create a new instance of the entities class:
            beverageEntities = new BeverageAMahlerEntities();

            wineItemsLength = beverageEntities.Beverages.Count();
        }

            // Add a new item to the collection
        public bool AddNewItem(string id, string description, string pack, decimal price, bool isActive)
        {
                // Create a boolean variable to hold whether the add was successful:
            bool added = false;

                // Create a new Beverage and add the properties:
            Beverage beverageToAdd = new Beverage();
            beverageToAdd.id = id;
            beverageToAdd.name = description;
            beverageToAdd.pack = pack;
            beverageToAdd.price = price;
            beverageToAdd.active = isActive;

                // Create a variable to hold whether the id is already in the database:
            bool inDatabase = false;

                // Check the database for the id:
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                    // If the beverage id is the same as the input id
                if (beverage.id == id)
                {
                        // Set the bool variable to true:
                    inDatabase = true;
                }
            }
                // If the id is not already used, continue to add the beverage:
            if (!inDatabase)
            {
                // Try to add the record to the database:
                try
                {
                    // Add the beverage to the entities list:
                    beverageEntities.Beverages.Add(beverageToAdd);
                    // Save changes to the database:
                    beverageEntities.SaveChanges();
                    // Increment the length variable:
                    wineItemsLength++;
                    // Change bool variable to true:
                    added = true;
                }
                catch (Exception ex)
                {
                    // Remove the beverage from the entities list if it cannot be added successfully:
                    beverageEntities.Beverages.Remove(beverageToAdd);
                    // Output to user:
                    Console.WriteLine("The beverage cannot be added: " + ex.Message);
                    // Change boolean variable:
                    added = false;
                }
            }
                // Otherwise, output the error to the user:
            else
            {
                added = false;
            }
            return added;
        }
        
        //Get The Print String Array For All Items
        public string[] GetPrintStringsForAllItems()
        {
            //Create and array to hold all of the printed strings
            string[] allItemStrings = new string[wineItemsLength];
            //set a counter to be used
            int counter = 0;

            //If the wineItemsLength is greater than 0, create the array of strings
            if (wineItemsLength > 0)
            {
                //For each item in the collection
                foreach (Beverage beverage in beverageEntities.Beverages)
                {
                    {
                        //Add the results of calling ToString on the item to the string array.
                        allItemStrings[counter] = beverage.id + " " + beverage.name + " " + beverage.pack + " " + 
                            beverage.price.ToString("C2") + " Active: " + beverage.active;
                        counter++;
                    }
                }
            }
            //Return the array of item strings
            return allItemStrings;
        }

        //Find an item by it's Id
        public string FindById(string id)
        {
            //Declare return string for the possible found item
            string returnString = null;

            //For each WineItem in wineItems
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                //if the wineItem Id is the same as the search id
                if (beverage.id == id)
                {
                    //Set the return string to the result of the wineItem's ToString method
                    returnString = beverage.id + " " + beverage.name.Trim() + " " + beverage.pack.Trim() + " " +
                            beverage.price.ToString("C2") + " Active: " + beverage.active;
                }
            }
            //Return the returnString
            return returnString;
        }

        public bool UpdateBeverageItem(string id)
        {
                // Bool for whether it has been updated:
            bool updated = false;
                // String to hold the beverage info based on the id, using the FindById method:
            string beverageInfo = FindById(id);
                // Check if the beverage info string has contect (item was found):
            if (beverageInfo != null)
            {
                    // Save the pre-update beverage item in a variable:
                Beverage oldBeverage = beverageEntities.Beverages.Find(id);
                    // Create a new beverage to save the new properties in:
                Beverage beverageUpdate = new Beverage();
                    // Output verification to user:
                Console.WriteLine("The id you selected is for the following beverage item: ");
                Console.WriteLine(beverageInfo);
                    // Get property information:
                Console.WriteLine();
                Console.WriteLine("Enter the updated information for each field. (Id cannot be changed.)");
                Console.Write("Name: ");
                beverageUpdate.name = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Pack: ");
                beverageUpdate.pack = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Price: ");
                beverageUpdate.price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Active? (Y or N): ");
                beverageUpdate.active = Console.ReadLine() == "Y";

                try
                {
                        // Remove the old beverage:
                    beverageEntities.Beverages.Remove(oldBeverage);
                        // Set the updated beverage's id to the same id:
                    beverageUpdate.id = id;
                        // Add the beverage:
                    beverageEntities.Beverages.Add(beverageUpdate);
                        // Save changes:
                    beverageEntities.SaveChanges();
                        // Change the success boolean:
                    updated = true;
                }
                catch (Exception ex)
                {
                        // Output update error to user:
                    Console.WriteLine("There was an error: " + ex.Message);
                }
            }
            else
            {
                    // Output id error to user:
                Console.WriteLine("That id does not correspond to an existing beverage.");
            }
                // Return whether successful:
            return updated;
        }

        public bool DeleteBeverageItem(string id)
        {
                // Set boolean for success:
            bool deleted = false;
                // Find the info for the beverage item:
            string beverageInfo = FindById(id);
                // If the info is not empty:
            if (beverageInfo != null)
            {       // Set the beverage to delete:
                Beverage beverageToDelete = beverageEntities.Beverages.Find(id);
                    // Verify:
                Console.WriteLine("Are you sure you want to delete the following item? (Y or N)");
                Console.WriteLine(beverageInfo);
                if (Console.ReadLine() == "Y")
                {
                    try
                    {       // Remove the item:
                        beverageEntities.Beverages.Remove(beverageToDelete);
                            // Save the changes:
                        beverageEntities.SaveChanges();
                            // Change success variable to true:
                        deleted = true;
                    }
                    catch (Exception ex)
                    {       // Output to user deletion error:
                        Console.WriteLine("There was a problem deleting the item: " + ex.Message);
                    }
                }
            }
            else
            {       // Output to user id error:
                Console.WriteLine("There is no item with that id in the database.");
            }
                // Return success boolean:
            return deleted;
        }

    }
}
