# AvansertProgObli1
Rest api for an bookstore.

Getting started:
Start up an empty mysql database. To connect to it through the api go to: 
"BookStoreAPI/appsettings.json" and in the change the "DefaultConnection"-String with the credentials for your database.
When the database is connected go to SetupDB/MakeTables.sql and run the script to make the book table.
under the same folder you can run data.sql to populate the database with data.

The API has implemented 4 endpoints as follows: 

GET {{BookStoreAPI_HostAddress}}/Books/
Accept: application/json

Comment: This get endpoint can sort the returned list based on PublicationYear, Title, Author or get a book by ID.
###
POST {{BookStoreAPI_HostAddress}}/Books/
Accept: application/json
Content-Type: application/json

###
PUT {{BookStoreAPI_HostAddress}}/Books/{id}
Accept: application/json
Content-Type: application/json

###
DELETE {{BookStoreAPI_HostAddress}}/Books/{id}

Examples of using the api: 

![image](https://github.com/user-attachments/assets/1d1e3f2b-ce1e-4695-90d2-07b96c117ab8)
![image](https://github.com/user-attachments/assets/a929f9cf-1802-443a-952f-b2242a496d7b)
![image](https://github.com/user-attachments/assets/c6ae29c0-884f-4cb4-856c-96a9c3869600)
![image](https://github.com/user-attachments/assets/6b54cfa4-40f0-4245-bf9e-551b15fcd227)
