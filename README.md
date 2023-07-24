# Contact database with EdgeDB

https://github.com/Rofaydaaa/ConatactDB-EdgeDB/assets/125312170/eafbbe28-51d2-4552-bd90-494336369339

The project aims to create a contact database system using EdgeDB 3.0, ASP.NET Core Razor Pages, and Ajax to allow us to perform real-time searches without the need to reload the entire web page. 

# Functionality:
- Create: Users can add new contacts by filling out the form with the required details and saving the information to EdgeDB.
- List: The system will display a list of all entered contacts with relevant information.
- Search Filter: The list of contacts will be equipped with a search filter, allowing users to search for specific contacts based on their first name and last name.
- Update: Users can edit existing contact information.
- Delete: Users can remove contacts from the list if they are no longer required.

# Try out this sample:
- Go to dbschema folder.
- Execute the following command 
```
edgedb project init --server-instance contact-app.
```
- Move one directory up.
- Execute the following command dotnet watch.
- Open your browser to http://localhost:XXXX.

# How to clean up
Run this command line
```
edgedb instance destroy -I contact-app --force .
```
