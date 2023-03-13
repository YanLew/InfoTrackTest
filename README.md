# InfoTrackTest
InfoTrack Technical Test

## Backend Setup 
.Net Core 7 Api, 
Url: https://localhost:7293/swagger/index.html
1. Open InfoTrackTest.sln with Visual Studio
2. The current connection string is `Server=localhost\\SQLEXPRESS;Database=info-track-test;Trusted_Connection=True;TrustServerCertificate=True;`. You may update InfoTrackTest.Apis/appsettings.json config "ConnectionStrings:InfoTrackTestDb" for the db connection string / create an empty express database "info-track-test" 
3. Open the Package Manager Console in VS
4. Choose InfoTrackTest.Repositories as the default project
5. Execute comment `Update-Database`
6. Run project InfoTrackTest.Api

## Frontend Setup
Node version: 16.13.1, 
Url: https://localhost:3002/
1. Open Command Prompt and enter the InfoTrackTest.Web
2. Execute comment `yarn install`
3. Execute comment `yarn start`

## UI Design
### Search Page
- After entered the search keyword, the page would automatically search the result
![image](https://user-images.githubusercontent.com/76930062/224552174-865c122f-81c4-4f74-8e49-656d29a550dd.png)
![image](https://user-images.githubusercontent.com/76930062/224552153-3f01ba14-3622-4699-887e-134bfc117277.png)
![image](https://user-images.githubusercontent.com/76930062/224552195-258d0255-be94-4291-97f9-3e77700fa0f4.png)

### History Page
- A paginated table displaying 10 records per page
- All records would be display in the descending order by the search date
![image](https://user-images.githubusercontent.com/76930062/224552529-ce016f1c-2dc4-4835-827a-9cf306784867.png)

### History Dashboard Page
- A table displaying the daily search trend
- Grouping the search data by the search date, search keyword and search engine
![image](https://user-images.githubusercontent.com/76930062/224552665-35128252-a583-42f0-b4ba-222e9328b20b.png)
