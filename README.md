# Contact database with EdgeDB

https://github.com/Rofaydaaa/ConatactDB-EdgeDB/assets/125312170/eafbbe28-51d2-4552-bd90-494336369339

### Authentication and Authorization: 


https://github.com/Rofaydaaa/ContactDB-EdgeDB/assets/125312170/f49b35f6-0170-4e2a-b05a-a5d0e7a2a254


https://github.com/Rofaydaaa/ContactDB-EdgeDB/assets/125312170/65c8aaac-ac59-4f53-9fa6-954a760169af



The project aims to create a contact database system using ```EdgeDB 3.0```, ```ASP.NET Core Razor Pages```, and ```Ajax``` <small>(Ajax allows us to perform real-time searches without the need to reload the entire web page).</small>

# Functionality:
- Two users Roles (Admin and Normal)
#### All users can:
- List: The system will display a list of all entered contacts with relevant information.
- Search Filter: The list of contacts will be equipped with a search filter, allowing users to search for specific contacts based on their first name and last name.
#### Only Admin can:
- Create: Add new contacts by filling out the form with the required details and saving the information to EdgeDB.
- Update: Edit existing contact information.
- Delete: Remove contacts from the list if they are no longer required.

# Try out this sample:
- Execute the following command 
```
edgedb project init --server-instance contact-app.
```
- Move one directory up.
- Execute the following command
```
dotnet watch
```
- Open your browser to http://localhost:XXXX.

# How to clean up
Run this command line
```
edgedb instance destroy -I contact-app --force .
```
